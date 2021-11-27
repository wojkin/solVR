using System.Collections.Generic;
using Patterns;
using Robots;
using ScriptableObjects;
using ScriptableObjects.Environments;
using Tasks;
using UnityEngine;

namespace Levels
{
    /// <summary>
    /// Class for passing level data between scenes.
    /// </summary>
    public class PersistentLevelData : Singleton<PersistentLevelData>
    {
        #region Serialized Fields

        /// <summary><see cref="BlockData"/> of blocks available in the level.</summary>
        [SerializeField] public List<BlockData> blockData;

        /// <summary>The next <see cref="Level"/> after this one.</summary>
        [SerializeField] public Level nextLevel;

        /// <summary>Robot available in the level.</summary>
        [SerializeField] private Robot robot;
        
        /// <summary>Task available in the level.</summary>
        [SerializeField] private Task task;

        #endregion

        #region Variables

        /// <summary>Delegate for data initialize.</summary>
        public delegate void LevelLoad();

        /// <summary>Event invoked to notify listeners that level is loaded.</summary>
        public event LevelLoad LevelLoaded;

        /// <summary><inheritdoc cref="robot"/></summary>
        public Robot Robot
        {
            get => robot;
            private set => robot = value;
        }
        
        /// <summary><inheritdoc cref="task"/></summary>
        public Task Task
        {
            get => task;
            private set => task = value;
        }

        #endregion

        #region Custom Methods

        /// <summary>
        /// Sets robot and task data and invokes <see cref="LevelLoaded"/> event.
        /// </summary>
        public void OnLevelLoad(Robot robot, Task task)
        {
            Robot = robot;
            Task = task;
            LevelLoaded?.Invoke();
        }
        
        /// <summary>
        /// Sets level data.
        /// </summary>
        /// <param name="levelData">Passed level to set the data.</param>
        public void SetLevelData(Level levelData)
        {
            blockData = levelData.blocks;
            nextLevel = levelData.nextLevel;
        }

        #endregion
    }
}