using System;
using UnityEngine;

namespace ScriptableObjects
{
    /// <summary>
    /// Stores data about a block as a <see cref="ScriptableObject"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "Block", menuName = "ScriptableObjects/Block", order = 2)]
    public class BlockData : ScriptableObject
    {
        #region Serialized Fields

        /// <summary>Name of the block displayed in the toolbox.</summary>
        [SerializeField] private String blockName;

        /// <summary>Prefab without interactive elements used in the toolbox.</summary>
        [SerializeField] private GameObject previewPrefab;

        /// <summary>Prefab of the block.</summary>
        [SerializeField] private GameObject prefab;

        #endregion

        #region Variables

        /// <summary><inheritdoc cref="blockName"/></summary>
        public string BlockName => blockName;

        /// <summary><inheritdoc cref="previewPrefab"/></summary>
        public GameObject PreviewPrefab => previewPrefab;

        /// <summary><inheritdoc cref="prefab"/></summary>
        public GameObject Prefab => prefab;

        #endregion
    }
}