using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;

public class SnakeController : MonoBehaviour
{
    public ScaleFromHealth scaler;
    public float segmentMaxScale;
    public int segmentAmount;
    public List<segment> segmentPoses = new();
    Vector2[] segmentV;

    public GameObject segmentPrefab;
    public Transform targetDir;
    public float targetDist;
    public float smoothSpeed;

    private void Update()
    {
        while (segmentPoses.Count < segmentAmount) { segmentPoses.Add(new(Instantiate(segmentPrefab, transform.parent))); }
        while (segmentPoses.Count > segmentAmount) { Destroy(segmentPoses[segmentPoses.Count - 1].gameObject); segmentPoses.Remove(segmentPoses[segmentPoses.Count - 1]); }

        segmentPoses[0].gameObject.GetComponent<Rigidbody2D>().position = targetDir.position;
        segmentPoses[0].gameObject.transform.localScale = new(scaler.scale, scaler.scale, 1);

        for (int i = 1; i < segmentPoses.Count; i++)
        {
            Vector2 position = segmentPoses[i].gameObject.GetComponent<Rigidbody2D>().position;
            Vector2 previousPosition = segmentPoses[i - 1].gameObject.GetComponent<Rigidbody2D>().position;
            segmentPoses[i].gameObject.GetComponent<Rigidbody2D>().position = Vector2.SmoothDamp(position, previousPosition + (Vector2)(targetDir.right * targetDist), ref segmentPoses[i].velocity, smoothSpeed);
            segmentPoses[i].gameObject.transform.localScale = new(scaler.scale, scaler.scale, 1);
        }
    }
    public void AddSegment()
    {
        segmentAmount++;
    }
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
