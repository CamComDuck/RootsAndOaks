using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetalSpawner : MonoBehaviour
{
    private int nextUpdate = 1;
    public GameObject[] spawnedObjects;
    DrawManager rootGenScript;

    // Start is called before the first frame update
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
                UpdateEverySecond();
            }
        }
    }

    void UpdateEverySecond(){
        for (var i = 0; i < 2; i++) {
            var randSpawner = Random.Range(0, spawnedObjects.Length);
            var position = new Vector3(Mathf.Round(Random.Range(rootGenScript.currentPosX[0]-8, rootGenScript.currentPosX[0]+8)), rootGenScript.currentPosY[0]-9, 1);
            Instantiate(spawnedObjects[randSpawner], position, Quaternion.identity);
        }

    }
}
