using Patterns;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public void ExitGame() 
        {
            Application.Quit();
        }
    }
}
