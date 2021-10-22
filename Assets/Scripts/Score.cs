using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] float timeBeforeUpadtingScore = 0.25f;
    int scoreAmount = 0;

    private void Start()
    {
        StartCoroutine(AddScore());
    }
    IEnumerator AddScore() 
    {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Text>().color = newColor;
        scoreAmount += 1;
        GetComponent<Text>().text = scoreAmount.ToString();

        yield return new WaitForSeconds(timeBeforeUpadtingScore);
        if (FindObjectOfType<Balloon>().IsDead()) 
        {
            yield break;
        }
        StartCoroutine(AddScore());
    }
}
