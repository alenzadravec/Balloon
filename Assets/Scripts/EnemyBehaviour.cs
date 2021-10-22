using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Behaviour Config")]
public class EnemyBehaviour : ScriptableObject
{
    [SerializeField] GameObject[] movingPathPrefab;
    [SerializeField] float moveSpeed;

    public List<Transform> GetWaypoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in movingPathPrefab[Random.Range(0,movingPathPrefab.Length)].transform) //Take one of elements from array
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }

    public float GetMoveSpeed() { return moveSpeed; }
}
