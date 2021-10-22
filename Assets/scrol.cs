using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrol : MonoBehaviour
{

    //Script only for testing
    [SerializeField] string TestingScript = "Test script!";
    [SerializeField] float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(-speed*Time.deltaTime,0f,0f));
    }
}
