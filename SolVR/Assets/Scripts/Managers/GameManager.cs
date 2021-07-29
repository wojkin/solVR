using Patterns;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public void ExitGame() 
        {
            Application.Quit();
        }

        public void LoadScene(Environment scriptableObject)
        {
            Addressables.LoadSceneAsync(scriptableObject.scene);
        }
    }
}
