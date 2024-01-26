using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    private int nextUpdate = 1;
    private int timeCounter = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time>=nextUpdate){
            nextUpdate=Mathf.FloorToInt(Time.time)+1;

            timeCounter--;
            if (timeCounter == 0) Destroy(gameObject);
        }
    }
}
