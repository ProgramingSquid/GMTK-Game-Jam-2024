using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    public Vector2 force;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(force);
    }
}
