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
    public List<segment> segmentPoses = new();
    Vector2[] segmentV;

    public GameObject segmentPrefab;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Start()
    {
        lineRenderer.positionCount = segmentAmount;
    }
    private void Update()
    {
        while (segmentPoses.Count < segmentAmount) { segmentPoses.Add(new(Instantiate(segmentPrefab, transform.parent))); }
        while (segmentPoses.Count > segmentAmount) { Destroy(segmentPoses[segmentPoses.Count - 1].gameObject); segmentPoses.Remove(segmentPoses[segmentPoses.Count - 1]); }

        segmentPoses[0].gameObject.GetComponent<Rigidbody2D>().position = targetDir.position;

        for (int i = 1; i < segmentPoses.Count; i++)
        {
            Vector2 position = segmentPoses[i].gameObject.GetComponent<Rigidbody2D>().position;
            Vector2 previousPosition = segmentPoses[i - 1].gameObject.GetComponent<Rigidbody2D>().position;
            segmentPoses[i].gameObject.GetComponent<Rigidbody2D>().position = Vector2.SmoothDamp(position, previousPosition + (Vector2)(targetDir.right * targetDist), ref segmentPoses[i].velocity, smoothSpeed);
        }
        lineRenderer.SetPositions(segmentPoses.ConvertAll(item => item.gameObject.transform.position).ToArray());
    }
    public void AddSegment()
    {
        segmentAmount++;
    }

    public class segment
    {
        public GameObject gameObject;
        public Vector2 velocity;

        public segment(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
