using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Camera camera;
    public Rigidbody2D rb;
    public float flyForce = 1;
    public float dashForce = 2;
    public static PlayerMovement player;
    public Vector3 aimPos;
    Input inputs;

    void Awake()
    {
        if (player == null) { player = this; }
        inputs = new();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aim(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var screenPos = context.ReadValue<Vector2>();
        aimPos = camera.ScreenToWorldPoint(screenPos);
        var dir =  aimPos - transform.position;
        var angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        Debug.Log(angle);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void Dash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        rb.AddRelativeForce(Vector2.right * dashForce, ForceMode2D.Impulse);
    }

    public void Fly(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
    }
}
