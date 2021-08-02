using System;
using Managers;
using UnityEngine.SceneManagement;

namespace DeveloperTools.Commands
{
    /// <summary>
    /// A command for resetting the current scene.
    /// </summary>
    [Serializable]
    public class ResetCommand : Command
    {
        public ResetCommand()
        {
            commandPattern = "rst";
        }

        /// <summary>
        /// Reloads the currently loaded scene.
        /// </summary>
        public override void Execute()
        {
            CustomSceneManager.Instance.QueueReloadScene();
        }
    }
}