using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balloon : MonoBehaviour
{
    [Header("Basic setup values")]
    [SerializeField] int health = 100;
    [SerializeField] float flyUpSpeed;
    [SerializeField] float damagedFlyUpSpeed;
    [SerializeField] Sprite undamagedSprite;
    [SerializeField] Sprite damagedSprite;
    [SerializeField] Sprite veryDamagedSprite;

    [Header("Wind variables")]
    [SerializeField] float windSpeedRange;
    [SerializeField] float minTimeBeforeChangingWind;
    [SerializeField] float maxTimeBeforeChangingWind;

    DamageDealer damageDealer;
    EnemySpawner enemySpawner;

    float xMin;
    float xMax;
    float yMax;
    float yMin;

    float windSpeed;
    float yAxisMovement;
    float timeCounter = 0;
    float duration;
    int maxHealth = 100;
    bool isDead=false;


    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.SpawnEnemies();
        duration = Random.Range(minTimeBeforeChangingWind, maxTimeBeforeChangingWind);
        windSpeed = Random.Range(-windSpeedRange, windSpeedRange);
        transform.Translate(Random.Range(0, windSpeed) * Time.deltaTime,0,0); // Change position as wind blows
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovementAndBoundaries();
        BlowWind();   
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
    }

    private void BasicMovementAndBoundaries()
    {

        if (transform.position.x <= xMin || transform.position.y <= yMin)
        {
            Die();
        }
        else if (transform.position.y >= yMax)
        {
            transform.Translate(0, -yAxisMovement, 0); // Upper bound
        }
        else if (transform.position.x >= xMax)
        {
            transform.position = new Vector2(xMax, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            yAxisMovement = flyUpSpeed * Time.deltaTime; //movement progress
            transform.Translate(0, yAxisMovement, 0);
        }
    }

    private void ChangeWind()
    {
        Debug.Log("Change!");
        windSpeed = Random.Range(-windSpeedRange, windSpeedRange);
        timeCounter = 0;
        duration = Random.Range(minTimeBeforeChangingWind, maxTimeBeforeChangingWind);
    }
    private void BlowWind() 
    {
       // Debug.Log("WindSpeed:"+windSpeed);
        timeCounter += Time.deltaTime;
      //  Debug.Log(timeCounter);

        if (timeCounter >= duration)
        {
            ChangeWind();
        }
        transform.Translate(windSpeed * Time.deltaTime, 0, 0); // Change position as wind blows
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Kolizija");
        //damageDealer = other.gameObject.GetComponent<DamageDealer>();
        //health += damageDealer.GetDamage();
        //if (health <= 0)
        //{
        //    Die();
        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player trigger");
        ManageHealth(other);
        ChangesDependingHealth(); // changing sprites depending on health
    }

    private void ManageHealth(Collider2D other)
    {
        damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health += damageDealer.GetDamage();

        if (health >= maxHealth) { health = maxHealth; }
        if (health <= 0)
        {
            Die();
        }
        if (!isDead)
        {
            Destroy(other.gameObject);
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Dead");
        flyUpSpeed = 0;
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        //Destroy(gameObject);
    }

    public bool IsDead() 
    {
        return isDead;
    }

    private void ChangesDependingHealth() 
    {
        if (health < 75 && health > 15)
        {
            GetComponent<SpriteRenderer>().sprite = damagedSprite;
        }
        else if (health < 15)
        {
            GetComponent<SpriteRenderer>().sprite = veryDamagedSprite;
            flyUpSpeed = damagedFlyUpSpeed;
        }
        else if(health>75)
        {
            GetComponent<SpriteRenderer>().sprite = undamagedSprite;
        }
    }
}
