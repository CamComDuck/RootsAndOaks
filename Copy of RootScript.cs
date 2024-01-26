using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class RootScript : MonoBehaviour
{
    private int nextUpdate = 1;
    private int timeCounter = 10;

    EdgeCollider2D edgeCollider;
    LineRenderer myLine;
    DrawManager rootGenScript;

    // Start is called before the first frame update
    void Start()
    {
        edgeCollider = this.GetComponent<EdgeCollider2D>();
        myLine = this.GetComponent<LineRenderer>();
        rootGenScript = GameObject.FindGameObjectWithTag("DrawManager").GetComponent<DrawManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetEdgeCollider(myLine);
        if(Time.time>=nextUpdate){
            nextUpdate=Mathf.FloorToInt(Time.time)+1;

            timeCounter--;
            if (timeCounter == 0) Destroy(gameObject);
        }
    }

    void SetEdgeCollider(LineRenderer lineRenderer) {
        List<Vector2> edges = new List<Vector2>();

        for (int point = 0; point<lineRenderer.positionCount; point++) {
            Vector3 lineRendererPoint = lineRenderer.GetPosition(point);
            edges.Add(new Vector2(lineRendererPoint.x, lineRendererPoint.y));
        } 

        edgeCollider.SetPoints(edges);
    }

    void OnTriggerEnter2D(Collider2D hitObject) {
        if (hitObject.tag == "Rock") {
            Destroy(hitObject.gameObject);
            rootGenScript.hitRock();
        } else if (hitObject.tag == "Leaf") {
            Destroy(hitObject.gameObject);
            rootGenScript.hitLeaf();
        } else if (hitObject.tag == "Water") {
            rootGenScript.hitWater();
        } else if (hitObject.tag == "Petal") {
            Destroy(hitObject.gameObject);
            rootGenScript.hitPetal();
        }
        
    }
}
