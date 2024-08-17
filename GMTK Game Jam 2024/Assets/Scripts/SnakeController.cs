using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public int segmentAmount;
    public LineRenderer lineRenderer;
    public GameObject[] segmentPoses;
    Vector2[] segmentV;

    public GameObject segmentPrefab;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Start()
    {
        lineRenderer.positionCount = segmentAmount;
        segmentPoses = new GameObject[segmentAmount];
        segmentV = new Vector2[segmentAmount];

        for (int i = 0; i < segmentPoses.Length; i++) 
        { 
            segmentPoses[i] = Instantiate(segmentPrefab); 
        }

    }
    private void Update()
    {
        segmentPoses[0].transform.position = targetDir.position;

        for (int i = 1; i < segmentPoses.Length; i++)
        {
            Vector2 position = segmentPoses[i].GetComponent<Rigidbody2D>().position;
            Vector2 previousPosition = segmentPoses[i - 1].GetComponent<Rigidbody2D>().position;
            segmentPoses[i].GetComponent<Rigidbody2D>().position = Vector2.SmoothDamp(position, previousPosition + (Vector2)(targetDir.right * targetDist), ref segmentV[i], smoothSpeed);
        }
        lineRenderer.SetPositions(Array.ConvertAll(segmentPoses, item => item.transform.position));
    }
}
