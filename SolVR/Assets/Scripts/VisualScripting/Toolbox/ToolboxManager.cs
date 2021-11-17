using System;
using System.Collections.Generic;
using Levels;
using ScriptableObjects;
using UI.List;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace VisualScripting.Toolbox
{
    /// <summary>
    /// Manager for the toolbox responsible for initializing and displaying blocks.
    /// </summary>
    public class ToolboxManager : MonoBehaviour
    {
        #region Serialized Fields

        /// <summary>Parent transform for all new blocks.</summary>
        [SerializeField] private Transform blockParent;

        /// <summary>Positions at which the blocks can be displayed.</summary>
        [SerializeField] private List<Transform> displayTransforms;

        /// <summary>Descriptions of currently displayed blocks.</summary>
        [SerializeField] private UIList blockDescriptions;

        /// <summary>Button for shifting displayed blocks to the left.</summary>
        [SerializeField] private Button leftBtn;

        /// <summary>Button for shifting displayed blocks to the right.</summary>
        [SerializeField] private Button rightBtn;

        #endregion

        #region Variables

        /// <summary>Pool of all block instances, one for each block data.</summary>
        private readonly List<GameObject> _blockPool = new List<GameObject>();

        /// <summary>List of currently displayed blocks.</summary>
        private readonly List<GameObject> _displayedBlocks = new List<GameObject>();

        /// <summary>List of block data for blocks available in the toolbox.</summary>
        private List<BlockData> _blockData;

        /// <summary>Count of all blocks.</summary>
        private int _blockCount;

        /// <summary>Count of currently displayed blocks.</summary>
        private int _displayCount;

        /// <summary>ID of the first displayed block.</summary>
        private int _firstBlockId;

        /// <summary>Maximum size a block can have on any dimension.</summary>
        private float _maxBlockSize;

        /// <summary>Action assigned to the left button.</summary>
        private UnityAction _leftBtnAction;

        /// <summary>Action assigned to the right button.</summary>
        private UnityAction _rightBtnAction;

        #endregion

        #region Built-in Methods

        /// <summary>
        /// Initializes variables, blocks and buttons.
        /// </summary>
        private void Start()
        {
            _blockData = PersistentLevelData.Instance.blockData;
            _blockCount = _blockData.Count;
            _displayCount = displayTransforms.Count;
            _maxBlockSize = CalcSmallestDist();
            InitializeBlocks();
            Show();
            UpdateButtons();
        }

        /// <summary>
        /// Adds button listeners.
        /// </summary>
        private void OnEnable()
        {
            _rightBtnAction = () => Shift(false);
            rightBtn.onClick.AddListener(_rightBtnAction);
            _leftBtnAction = () => Shift(true);
            leftBtn.onClick.AddListener(_leftBtnAction);
        }

        /// <summary>
        /// Removes button listeners.
        /// </summary>
        private void OnDisable()
        {
            rightBtn.onClick.RemoveListener(_rightBtnAction);
            leftBtn.onClick.RemoveListener(_leftBtnAction);
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Enables or disables buttons based on the currently displayed blocks.
        /// </summary>
        private void UpdateButtons()
        {
            leftBtn.interactable = _firstBlockId != 0;
            rightBtn.interactable = _firstBlockId + _displayCount < _blockCount;
        }

        /// <summary>
        /// Initializes a block and adds it to the block pool.
        /// </summary>
        /// <remarks>
        /// If <paramref name="blockToReplace"/> is not set, the block will be added at the end of the block pool list.
        /// </remarks>
        /// <param name="data"><see cref="BlockData"/> of the block which will be initialized.</param>
        /// <param name="blockToReplace">Block in the pool to replace.</param>
        private void InitializeBlock(BlockData data, GameObject blockToReplace = null)
        {
            // instantiate the block toolbox prefab 
            var instantiatedBlock = Instantiate(data.PreviewPrefab, gameObject.transform);

            // set the scale so the block fits in the toolbox
            var bounds = CalcBlockBounds(data.PreviewPrefab);
            instantiatedBlock.transform.localScale = Vector3.one * (_maxBlockSize / bounds.size.magnitude);

            instantiatedBlock.SetActive(false); // disable the block gameobject

            MakePlaceable(instantiatedBlock, bounds, data);

            // replace a block in the pool if block to replace is set, otherwise add it to the end
            if (blockToReplace != null)
                _blockPool[_blockPool.FindIndex(ind => ind.Equals(blockToReplace))] = instantiatedBlock;
            else
                _blockPool.Add(instantiatedBlock);
        }

        /// <summary>
        /// Initializes all blocks.
        /// </summary>
        private void InitializeBlocks()
        {
            foreach (var block in _blockData)
                InitializeBlock(block);
        }

        /// <summary>
        /// Calculates the smallest distance between block display positions.
        /// </summary>
        /// <returns>Smallest distance between block display positions.</returns>
        private float CalcSmallestDist()
        {
            var smallestDist = Vector3.Distance(displayTransforms[0].position, displayTransforms[1].position);
            for (var i = 2; i < _displayCount; i++)
            {
                var dist = Vector3.Distance(displayTransforms[i].position, displayTransforms[i - 1].position);
                if (dist < smallestDist)
                    smallestDist = dist;
            }

            return smallestDist;
        }

        /// <summary>
        /// Calculate bounds encapsulating a block.
        /// </summary>
        /// <param name="block">Block for which bounds will be calculated.</param>
        /// <returns><see cref="Bounds"/> encapsulating the block.</returns>
        private Bounds CalcBlockBounds(GameObject block)
        {
            // create bounds object with center at the block position and zero size
            var combinedBounds = new Bounds(block.transform.position, Vector3.zero);

            // find all renderer components on the block
            var renderers = block.GetComponentsInChildren<Renderer>();

            // encapsulate each renderer's bounds in the bounds object
            foreach (var render in renderers)
                combinedBounds.Encapsulate(render.bounds);

            return combinedBounds;
        }

        /// <summary>
        /// Configures a block so it can be placed in the scene by the player.
        /// </summary>
        /// <param name="block">Block to be configured.</param>
        /// <param name="bounds"><see cref="Bounds"/> encapsulating the block.</param>
        /// <param name="data"><see cref="BlockData"/> for this block.</param>
        private void MakePlaceable(GameObject block, Bounds bounds, BlockData data)
        {
            // add a box collider to the block based on it's bounds
            var boxCollider = block.AddComponent<BoxCollider>();
            boxCollider.center = bounds.center;
            boxCollider.size = bounds.size;

            // add a placeable block component and configure it's fields
            var placeable = block.AddComponent<PlaceableBlock>();
            placeable.blockData = data;
            placeable.toMove = block.transform;
            placeable.parent = blockParent;

            placeable.BlockPlaced += PlaceHandler; // register a handler for block placed event
        }

        /// <summary>
        /// Displays a block at a preset position in the toolbox.
        /// </summary>
        /// <param name="posId">ID of the position in the toolbox.</param>
        /// <param name="blockId">ID of the block in the block pool.</param>
        private void DisplayAtPosition(int posId, int blockId)
        {
            var block = _blockPool[blockId];
            block.transform.position = displayTransforms[posId].position;
            block.SetActive(true);
            _displayedBlocks.Add(block);
        }

        /// <summary>
        /// Hides all the currently displayed blocks.
        /// </summary>
        private void Clear()
        {
            foreach (var block in _displayedBlocks)
                block.SetActive(false);

            _displayedBlocks.Clear();
        }

        /// <summary>
        /// Displays blocks in the toolbox.
        /// </summary>
        private void Show()
        {
            var positionId = 0;

            // calculates the index of the last block to be displayed
            var lastBlockId = Math.Min(_firstBlockId + _displayCount, _blockCount);

            // initialize a list for storing data of displayed blocks 
            var displayedBlockData = new List<Object>();

            // display each block and store it's data
            for (var i = _firstBlockId; i < lastBlockId; i++)
            {
                DisplayAtPosition(positionId, i);
                displayedBlockData.Add(_blockData[i]);
                positionId++;
            }

            // update block descriptions
            blockDescriptions.ChangeListElements(displayedBlockData);
        }

        /// <summary>
        /// Displays blocks which are next to the currently displayed blocks in the block pool and updates buttons.
        /// </summary>
        /// <param name="clockwise">Flag controlling block shift direction.</param>
        private void Shift(bool clockwise)
        {
            if (clockwise)
                _firstBlockId -= _displayCount;
            else
                _firstBlockId += _displayCount;

            Refresh();
            UpdateButtons();
        }

        /// <summary>
        /// Hides the currently displayed blocks and displays them again based on the current first block ID.
        /// </summary>
        private void Refresh()
        {
            Clear();
            Show();
        }

        /// <summary>
        /// Handler for the placed event of a block.
        /// </summary>
        /// <param name="data"><see cref="BlockData"/> of the block which was placed.</param>
        /// <param name="placedBlock">The block <see cref="GameObject"/> which was placed.</param>
        private void PlaceHandler(BlockData data, GameObject placedBlock)
        {
            InitializeBlock(data, placedBlock);
            Refresh();
        }

        #endregion
    }
}