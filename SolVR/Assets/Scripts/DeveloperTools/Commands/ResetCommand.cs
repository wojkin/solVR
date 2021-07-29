using System;
using Managers;
using UnityEngine.SceneManagement;

namespace DeveloperTools.Commands
{
    [Serializable]
    public class ResetCommand : Command
    {
        public ResetCommand()
        {
            commandPattern = "rst";
        }

        public override void Execute()
        {
            CustomSceneManager.Instance.QueueLoadScene(SceneManager.GetActiveScene().name);
        }
    }
}