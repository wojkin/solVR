using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Environment", menuName = "ScriptableObjects/Environment", order = 1)]
public class Environment : ScriptableObject
{
    public string environmentName;
    public AssetReference scene;
}
