using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelGeneratorScriptableObject", order = 1)]
public class LevelGeneratorScriptableObject : ScriptableObject
{
    public string prefabPath;
    public Vector3 SegmentPositionOffset;
    public Vector3 SegmentLinkPositionOffset;
    
    public Vector3[] CoinSpawnPoints;
}