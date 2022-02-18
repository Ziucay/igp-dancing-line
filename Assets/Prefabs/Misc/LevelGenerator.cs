using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public int SegmentsAmount = 10;
    
    [Range(0.0f, 1.0f)]
    public float CoinSpawnProbability;
    
    public Vector3 InitialPosition = Vector3.zero;

    public GameObject End;
    public GameObject Coin;

    public LevelGeneratorScriptableObject InitialSegmentScriptableObject;
    private Object _initialSegmentScriptableObject;

    public LevelGeneratorScriptableObject[] LevelSegments;
    private Object[] _levelSegmentObjects;

    private void Awake()
    {
        InitializeSegmentPrefabs();

        List<int> segments = GenerateSegmentList();

        Vector3 endPosition = InstantiateSegments(segments);

        Instantiate(End, new Vector3(endPosition.x,endPosition.y - 0.01f, endPosition.z), Quaternion.identity);

    }

    private void InitializeSegmentPrefabs()
    {
        _initialSegmentScriptableObject = AssetDatabase.
            LoadAssetAtPath(InitialSegmentScriptableObject.prefabPath, typeof(GameObject));
        
        _levelSegmentObjects = new Object[LevelSegments.Length];
        for (int i = 0; i < LevelSegments.Length; ++i)
        {
            _levelSegmentObjects[i] = AssetDatabase.
                LoadAssetAtPath(LevelSegments[i].prefabPath, typeof(GameObject));
        }
    }

    private List<int> GenerateSegmentList()
    {
        List<int> segments = new List<int>();
        for (int i = 0; i < SegmentsAmount; ++i)
        {
            segments.Add(Random.Range(0,LevelSegments.Length));
        }

        return segments;
    }

    private Vector3 InstantiateSegments(List<int> segments)
    {
        Vector3 nextPosition = CreateInitialSegment();
        foreach (var segment in segments)
        {
            nextPosition = CreateNextSegment(segment, nextPosition);
        }

        return nextPosition;
    }

    private Vector3 CreateInitialSegment()
    {
        Vector3 newSegmentPosition = InitialPosition + InitialSegmentScriptableObject.SegmentPositionOffset;
        Vector3 newSegmentLinkPosition = InitialPosition + InitialSegmentScriptableObject.SegmentLinkPositionOffset;
        Instantiate(_initialSegmentScriptableObject,newSegmentPosition,Quaternion.identity);
        return newSegmentLinkPosition;
    }

    private Vector3 CreateNextSegment(int segment, Vector3 placePosition)
    {
        Vector3 newSegmentPosition = placePosition + LevelSegments[segment].SegmentPositionOffset;
        Vector3 newSegmentLinkPosition = placePosition + LevelSegments[segment].SegmentLinkPositionOffset;
        Instantiate(_levelSegmentObjects[segment], newSegmentPosition, Quaternion.identity);
        InstantiateCoins(segment, placePosition);
        return newSegmentLinkPosition;
    }

    private void InstantiateCoins(int segment, Vector3 placePosition)
    {
        Vector3[] CoinSpawnPoints = LevelSegments[segment].CoinSpawnPoints;

        foreach (var t in CoinSpawnPoints)
        {
            if (Random.value < CoinSpawnProbability)
            {
                Instantiate(Coin, placePosition + t + Vector3.up, Quaternion.identity);
            }
        }
    }
}
