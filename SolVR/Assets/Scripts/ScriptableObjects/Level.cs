using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 1)]
public class Level : ScriptableObject
{
    public string levelName;
    public AssetReference scene;
}
