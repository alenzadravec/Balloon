using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyBehaviour enemyBehaviour;
    [SerializeField] bool isRare = false;
    [Header("Rarity(0 - note rare at all, 1 - very rare)")]
    [SerializeField] [Range(0.01f,0.99f)]float rarity;

    [Header("Random offset (+/-) inserted value")]
    List<Transform> wayPoints;
    int wayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = enemyBehaviour.GetWaypoints();
        transform.position = wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move(); 
    }
    public void SetEnemyBehaviour(EnemyBehaviour enemyBehaviour)
    {
        this.enemyBehaviour = enemyBehaviour;
    }

    private void Move()
    {
        if (wayPointIndex <= wayPoints.Count - 1)
        {
            var targetPosition = wayPoints[wayPointIndex].transform.position;
            var movementThisFrame = enemyBehaviour.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool GetIsRare() 
    {
        return isRare;
    }
    public float GetRarity() 
    {
        return rarity;
    }
}
