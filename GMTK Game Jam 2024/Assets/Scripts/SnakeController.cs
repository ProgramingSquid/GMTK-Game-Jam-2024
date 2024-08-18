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

    public int maxSegmentAmount = 12;
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

        //To implement using jobs...

        /*segmentPoses[0].gameObject.GetComponent<Rigidbody2D>().position = targetDir.position;
        scaler.StartCoroutine(scaler.Scale(segmentPos.gameObject, Mathf.Clamp(scaler.scale, 0, segmentMaxScale)));

        NativeList<JobHandle> handles = new NativeArray<JobHandle>();
        for (int i = 1; i < segmentPoses.Count; i++)
        {
            var handle = ManageSegments();
            handles.
        }
        handle.Complete();*/
    }
    public void AddSegment()
    {
        segmentAmount++;
    }
    public JobHandle ManageSegments()
    {
        ManageSegmentsJob job = new ManageSegmentsJob(scaler,
     segmentMaxScale,
     maxSegmentAmount,
     segmentAmount,
     segmentPoses,
     segmentV,
     segmentPrefab,
     targetDir,
     targetDist,
     smoothSpeed);
        return job.Schedule();
    }

    public struct ManageSegmentsJob : IJob
    {
        public ScaleFromHealth scaler;
        public float segmentMaxScale;

        public int maxSegmentAmount;
        public int segmentAmount;
        public segment segmentPos;
        public segment lastSegmentPos;
        Vector2 segmentV;

        public GameObject segmentPrefab;
        public Transform targetDir;
        public float targetDist;
        public float smoothSpeed;

        public ManageSegmentsJob(ScaleFromHealth scaler, float segmentMaxScale, int maxSegmentAmount, int segmentAmount, segment segmentPos, segment lastSegmentPos,
            Vector2 segmentV, GameObject segmentPrefab, Transform targetDir, float targetDist, float smoothSpeed)
        {
            this.scaler = scaler;
            this.segmentMaxScale = segmentMaxScale;
            this.maxSegmentAmount = maxSegmentAmount;
            this.segmentAmount = segmentAmount;
            this.segmentPos = segmentPos;
            this.lastSegmentPos = lastSegmentPos;
            this.segmentV = segmentV;
            this.segmentPrefab = segmentPrefab;
            this.targetDir = targetDir;
            this.targetDist = targetDist;
            this.smoothSpeed = smoothSpeed;
        }

        public void Execute()
        {
            segmentAmount = Mathf.Clamp(segmentAmount, 0, maxSegmentAmount);
            Vector2 position = segmentPos[i].gameObject.GetComponent<Rigidbody2D>().position;
            Vector2 previousPosition = lastSegmentPos.gameObject.GetComponent<Rigidbody2D>().position;
            segmentPos.gameObject.GetComponent<Rigidbody2D>().position = Vector2.SmoothDamp(position, previousPosition + (Vector2)(targetDir.right * targetDist), ref segmentPoses[i].velocity, smoothSpeed);
            scaler.StartCoroutine(scaler.Scale(segmentPos.gameObject, Mathf.Clamp(scaler.scale, 0, segmentMaxScale)));
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
}
