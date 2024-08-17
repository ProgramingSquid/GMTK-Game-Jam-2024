using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed;
    [Range(0,1)]public float aimBias = .5f;
    public Transform target;
    Vector2 velocity;
    float zPos = 0;
    // Start is called before the first frame update
    void Start()
    {
        zPos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = Vector3.Lerp(target.position, PlayerMovement.player.aimPos, aimBias);
        Vector2 pos2D = Vector2.SmoothDamp(transform.position, targetPos, ref velocity, smoothSpeed * Time.deltaTime);
        transform.position = new(pos2D.x, pos2D.y, zPos);
    }
}
