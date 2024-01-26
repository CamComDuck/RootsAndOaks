using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager: MonoBehaviour
{
    private LineRenderer lineRenderer;
    public GameObject drawingPrefab;
    public float[] currentPosX;
    public float[] currentPosY;
    private int nextUpdate = 1;
    public GameObject losePanel;
    public GameObject winPanel;
    public GameObject titleScreenPanel;
    public bool gameOver = false;
    public int leafProtectionTimer = 0;
    Color rootColor = new Color(0.9056604f, 0.6397031f, 0.3645425f, 0.9f);
    Color rootColorProtected = new Color(0.6f, 0.6f, 0.3f, 0.9f);
    private int splitCount = 1;
    public bool gameStarted = false;
    CameraFollow cameraFollowScript;

    public AudioSource hitRockSound;
    public AudioSource splitSound;
    public AudioSource hitLeafSound;
    public AudioSource hitWaterSound;
    public AudioSource rootsGrowingSound;

    public GameObject[] petalTotal;

    void Start() {
        cameraFollowScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
    }

    void Update()
    {
        if (!gameOver && gameStarted) {
            if(Time.time>=nextUpdate){
                nextUpdate=Mathf.FloorToInt(Time.time)+1;

                if (Input.GetKey("left") && Input.GetKey("right")) {
                    for (int i = 0; i < splitCount; i++) {
                        downDraw(i);
                    }
                } else if (Input.GetKey("left")) {
                    for (int i = 0; i < splitCount; i++) {
                        leftDraw(false, i);
                    }
                } else if (Input.GetKey("right")) {
                    for (int i = 0; i < splitCount; i++) {
                        rightDraw(false, i);
                    }
                } else {
                    for (int i = 0; i < splitCount; i++) {
                        downDraw(i);
                    }
                }

                if (leafProtectionTimer > 0) leafProtectionTimer--;

            }
        }
        
        
    }

    void leftDraw(bool split, int rootNum) {
        var thisCurrentX = currentPosX[rootNum];
        var thisCurrentY = currentPosY[rootNum];
        GameObject drawing = Instantiate(drawingPrefab);
        lineRenderer = drawing.GetComponent<LineRenderer>();

        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        if (leafProtectionTimer == 0) {
            lineRenderer.startColor = rootColor;
            lineRenderer.endColor = rootColor;
        } else {
            lineRenderer.startColor = rootColorProtected;
            lineRenderer.endColor = rootColorProtected;
        }
        

        thisCurrentX--;
        Vector3 index0 = new Vector3(thisCurrentX+1, thisCurrentY, 1);
        Vector3 index1 = new Vector3(thisCurrentX, thisCurrentY-1, 1);

        lineRenderer.SetPosition(0, index0);
        lineRenderer.SetPosition(1, index1);
        if (!split) thisCurrentY--;

        currentPosX[rootNum] = thisCurrentX;
        currentPosY[rootNum] = thisCurrentY;
        
    }

    void rightDraw(bool split, int rootNum) {
        var thisCurrentX = currentPosX[rootNum];
        var thisCurrentY = currentPosY[rootNum];
        GameObject drawing = Instantiate(drawingPrefab);
        lineRenderer = drawing.GetComponent<LineRenderer>();

        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        if (leafProtectionTimer == 0) {
            lineRenderer.startColor = rootColor;
            lineRenderer.endColor = rootColor;
        } else {
            lineRenderer.startColor = rootColorProtected;
            lineRenderer.endColor = rootColorProtected;
        }

        thisCurrentX++;
        if (!split) {
            Vector3 index0 = new Vector3(thisCurrentX-1, thisCurrentY, 1);
            Vector3 index1 = new Vector3(thisCurrentX, thisCurrentY-1, 1);

            lineRenderer.SetPosition(0, index0);
            lineRenderer.SetPosition(1, index1);
            thisCurrentY--;
        } else {
            Vector3 index0 = new Vector3(thisCurrentX, thisCurrentY, 1);
            Vector3 index1 = new Vector3(thisCurrentX+1, thisCurrentY-1, 1);
            lineRenderer.SetPosition(0, index0);
            lineRenderer.SetPosition(1, index1);
            thisCurrentY--;
        }
        currentPosX[rootNum] = thisCurrentX;
        currentPosY[rootNum] = thisCurrentY;
        
    }

    void downDraw(int rootNum) {
        var thisCurrentX = currentPosX[rootNum];
        var thisCurrentY = currentPosY[rootNum];
        GameObject drawing = Instantiate(drawingPrefab);
        lineRenderer = drawing.GetComponent<LineRenderer>();
        
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        if (leafProtectionTimer == 0) {
            lineRenderer.startColor = rootColor;
            lineRenderer.endColor = rootColor;
        } else {
            lineRenderer.startColor = rootColorProtected;
            lineRenderer.endColor = rootColorProtected;
        }

        thisCurrentY--;
        Vector3 index0 = new Vector3(thisCurrentX, thisCurrentY+1, 1);
        Vector3 index1 = new Vector3(thisCurrentX, thisCurrentY, 1);

        lineRenderer.SetPosition(0, index0);
        lineRenderer.SetPosition(1, index1);

        currentPosX[rootNum] = thisCurrentX;
        currentPosY[rootNum] = thisCurrentY;

    }

    public void hitRock() {
        hitRockSound.Play();
        if (leafProtectionTimer > 0) {
            leafProtectionTimer = 0;
        } else {
            losePanel.SetActive(true);
            gameOver = true;
        }
    }

    public void hitLeaf() {
        hitLeafSound.Play();
        leafProtectionTimer = leafProtectionTimer + 5;
    }

    public void hitWater() {
        hitWaterSound.Play();
        winPanel.SetActive(true);
        gameOver = true;
        for (int i = 0; i<splitCount;i++) {
            petalTotal[i].SetActive(true);
        }
    }

    public void hitPetal() {
        splitSound.Play();
        var currentXArray = currentPosX;
        var currentYArray = currentPosY;
        for (int i = 0; i < splitCount; i++) {
            leftDraw(true, i);
            rightDraw(true, i);
        }
        splitCount++;

        currentPosX = new float[splitCount];
        currentPosY = new float[splitCount];
        for (int i = 0; i <= currentXArray.Length; i++) {

            if (i < currentXArray.Length) {
                currentPosX[i] = currentXArray[i]-1;
                currentPosY[i] = currentYArray[i];
            } else {
                currentPosX[i] = currentXArray[i-1]+1;
                currentPosY[i] = currentYArray[i-1];
            }
            
        }
    }

    public void startGameButton() {
        rootsGrowingSound.Play();
        titleScreenPanel.SetActive(false);
        gameStarted = true;
        currentPosX = new float[1];
        currentPosY = new float[1];
        currentPosX[0] = 0;
        currentPosY[0] = -1;

        downDraw(0);
        cameraFollowScript.goingDownCamera();
    }

    public void stopGameButton() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
