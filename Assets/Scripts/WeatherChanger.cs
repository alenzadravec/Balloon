using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    [SerializeField] float maxWaitingSecondsForWeatherChange;
    [SerializeField] bool isRaining;// only for desing testing
    [SerializeField] bool isSnowing;
    [SerializeField] bool blowLeaf;// delete afterwards
    
    public void IsRaining(bool isRaining) 
    {
        GameObject.Find("Rain").GetComponent<Animator>().SetBool("isRaining", isRaining);
    }

    public void BlowLeaf()
    {
        Debug.Log("Leaf blown");
        switch (Random.Range(0, 4)) 
        {
            case 0:
                SetLeafFlipAndBlow(false, false);
                break;

            case 1:
                SetLeafFlipAndBlow(true, true);
                break;

            case 2:
                SetLeafFlipAndBlow(true, false);
                break;

            case 3:
                SetLeafFlipAndBlow(false, true);
                break;
        }
    }
    private void SetLeafFlipAndBlow(bool xFlip, bool yFlip) 
    {
        GameObject.Find("Leaf").GetComponent<SpriteRenderer>().flipX = xFlip;
        GameObject.Find("Leaf").GetComponent<SpriteRenderer>().flipY = yFlip;
        GameObject.Find("Leaf").GetComponent<Animator>().SetTrigger("blowLeaf");
    }

    public void IsSnowing(bool isSnowing) 
    {
        GameObject.Find("Snow").GetComponent<Animator>().SetBool("isSnowing", isSnowing);
    }
    private void Start()
    {
        BlowLeaf();
        StartCoroutine(ChangeWeather());
    }
    
    private void Update()
    {
        //if (blowLeaf)
        //{
        //    BlowLeaf();
        //    blowLeaf = false;
        //}

        //IsRaining(isRaining);
        //IsSnowing(isSnowing);
    }

    IEnumerator ChangeWeather() 
    {
        switch (Random.Range(0, 5)) 
        {
        
            case 0:
                IsRaining(true);
                break;

            case 1:
                IsSnowing(true);
                break;

            case 2:
                IsRaining(false);
                break;

            case 3:
                IsSnowing(false);
                break;

            case 4:
                BlowLeaf();
                break;
        }
        Debug.Log("Changing weather!!");

        yield return new WaitForSeconds(Random.Range(0, maxWaitingSecondsForWeatherChange));
        StartCoroutine(ChangeWeather());
    }
}
