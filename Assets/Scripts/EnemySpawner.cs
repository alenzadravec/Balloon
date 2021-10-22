using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Enemy[] enemies;
    [SerializeField] float minSecBetweenSpawns;
    [SerializeField] float maxSecBetweenSpawns;
    Coroutine coroutine;

    IEnumerator SpawnEnemy() 
    {
        Enemy enemy = enemies[Random.Range(0, enemies.Length)];
        if (enemy.GetComponent<Enemy>().GetIsRare()) // If enemy is rare
        {
            Debug.Log("RAREEEEE!");
            float random = Random.value;//Value between 0 and 1
            if (random >= enemy.GetComponent<Enemy>().GetRarity()) // Chances of spawning enemy
            {
                Debug.Log(random +">" + enemy.GetComponent<Enemy>().GetRarity());
                Debug.Log(enemy);
                Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            }
            yield return new WaitForSeconds(Random.Range(minSecBetweenSpawns, maxSecBetweenSpawns));
            StartCoroutine(SpawnEnemy());

        }
        else // Just spawn if it's not rare
        {
            Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
            yield return new WaitForSeconds(Random.Range(minSecBetweenSpawns, maxSecBetweenSpawns));
            StartCoroutine(SpawnEnemy());
        }
    }

    public Coroutine SpawnEnemies() 
    {
        coroutine = StartCoroutine(SpawnEnemy());
        return coroutine;
    }

}
