using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private int nextUpdate = 1;
    private int currentY = -3;
    private int currentX = 0;
    DrawManager rootGenScript;

    void Start()
    {
        rootGenScript = GameObject.FindGameObjectWithTag("DrawManager").GetComponent<DrawManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rootGenScript.gameOver && rootGenScript.gameStarted) {
            if(Time.time>=nextUpdate){
                nextUpdate=Mathf.FloorToInt(Time.time)+1;
                if (Input.GetKey("left") && Input.GetKey("right")) {
                    goingDownCamera();
                } else if (Input.GetKey("left")) {
                    goingLeftCamera();
                } else if (Input.GetKey("right")) {
                    goingRightCamera();
                } else {
                    goingDownCamera();
                }
            }
        }
    }

    void goingLeftCamera()
    {
        currentX--;
        transform.position = new Vector3 (currentX, currentY, -10);
        currentY--; 
    }

    void goingRightCamera()
    {
        currentX++;
        transform.position = new Vector3 (currentX, currentY, -10);
        currentY--; 
    }

    public void goingDownCamera()
    {
        transform.position = new Vector3 (currentX, currentY, -10);
        currentY--;
    }

}
