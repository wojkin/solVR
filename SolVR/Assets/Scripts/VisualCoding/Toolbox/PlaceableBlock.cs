﻿using Controls.Interactions;
using ScriptableObjects;
using UnityEngine;

namespace VisualCoding.Toolbox
{
    /// <summary>
    /// A class representing a preview block which can be grabbed and placed in the scene.
    /// </summary>
    public class PlaceableBlock : Grabbable
    {
        #region Serialized Fields

        /// <summary>Data of the placed block.</summary>
        public BlockData blockData;

        #endregion

        #region Variables

        /// <summary>Delegate for a place handler.</summary>
        public delegate void PlaceHandler(BlockData blockData, GameObject placedBlock);

        /// <summary>Event invoked when a preview block is placed.</summary>
        public event PlaceHandler BlockPlaced;

        #endregion

        #region Custom Methods

        /// <summary><inheritdoc/></summary>
        public override void Grab()
        {
            transform.SetParent(null);
            base.Grab();
        }

        /// <summary><inheritdoc/></summary>
        public override void Release()
        {
            BlockPlaced?.Invoke(blockData, gameObject); // invoke the block placed event

            // instantiate a block and set it's position, rotation and scale
            var position = transform.position;
            var block = Instantiate(blockData.Prefab, position, Quaternion.identity);
            var lookPos = Camera.main.transform.position - position;
            lookPos.y = 0;
            block.transform.rotation = Quaternion.LookRotation(-lookPos);
            block.transform.localScale = transform.localScale;

            block.AddComponent<BlockExpander>(); // add a block expander to the block
            Destroy(gameObject); // destroy the toolbox block gameobject this script was attached to
        }

        #endregion
    }
}