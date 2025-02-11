using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource audio;
    public Camera camera;
    public Rigidbody2D rb;
    public float maxSpeed;
    public float rotSpeed;
    Quaternion targetRotation;

    public float airMoveForce = 1;
    public float constantMoveForce = 2;
    bool moving;
    Vector2 movement;

    public float flyForce = 1;
    public float flyCoolDown = .25f;
    float flyTimer = .25f;

    public float dashForce = 2;
    public int maxDashes = 2;
    public int dashes = 2;
    public AudioClip dash;
    public AudioClip dashFail;

    public static PlayerMovement player;
    public Vector3 aimPos;

    void Awake()
    {
        dashes = maxDashes;
        flyTimer = flyCoolDown;

        if (player == null) { player = this; } 
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddRelativeForce(Vector2.right * constantMoveForce * (1 - Vector2.Dot(transform.right, Vector2.up)) * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);

        if (moving)
        {
            Debug.Log(movement);
            rb.AddForce(movement * airMoveForce * Time.deltaTime);
        }
        rb.velocity = rb.velocity.magnitude > maxSpeed?  Vector2.ClampMagnitude(rb.velocity, maxSpeed) : rb.velocity;
        flyTimer -= Time.deltaTime;
    }

    public void Aim(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        var screenPos = context.ReadValue<Vector2>();
        aimPos = camera.ScreenToWorldPoint(screenPos);
        var dir =  aimPos - transform.position;
        var angle = Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x);
        targetRotation = Quaternion.Euler(0, 0, angle);
    }

    public void Dash(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (dashes > 0)
        {
            audio.clip = dash;
            audio.Play();
            dashes--;
            rb.AddRelativeForce(Vector2.right * dashForce, ForceMode2D.Impulse);
        }
        else 
        {
            audio.clip = dashFail;
            audio.Play();
        }
    }

    public void Fly(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (flyTimer <= 0)
        {
            flyTimer = flyCoolDown;
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
        }
    }

    public void Move(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        if (context.started) moving = true;
        else if(context.canceled) moving = false;
        Debug.Log(moving);
    }
    public void resetDashes()
    {
        dashes = maxDashes;
    }
    public void AddDash()
    {
        if (dashes >= maxDashes) return;
        dashes++;
    }

    public void StartGame(InputAction.CallbackContext context)
    {
        if (!context.started || ScoreDisplay.instance.startGame) return;

        ScoreDisplay.instance.startGame = true;
        rb.simulated = true;
    }
    public void QuitGame(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!context.started) return;
        ScoreDisplay.instance.QuitAndSaveHighScore();
    }
}
