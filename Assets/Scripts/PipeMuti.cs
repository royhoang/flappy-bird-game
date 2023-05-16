using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PipeMuti : MonoBehaviour
{
    public GameObject pipePrefab;

    private float countdown;
    public float timeDuration;
    public bool enableGenratePipe;

    private void Awake()
    {
        countdown = 1f;
        enableGenratePipe = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enableGenratePipe == true)
        {
            countdown -= Time.deltaTime;
        if(countdown <= 0)
        {
            Instantiate(pipePrefab, new Vector3(10, Random.Range(-1.2f, 2.1f), 0), Quaternion.identity);
            countdown = timeDuration;
        }
        }
        
    }
   

}
