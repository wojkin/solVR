using System;
using Managers;

namespace DeveloperTools.Commands
{
    /// <summary>
    /// A command for resetting the current scene.
    /// </summary>
    [Serializable]
    public class ResetCommand : Command
    {
        #region Custom Methods

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

        #endregion
    }
}