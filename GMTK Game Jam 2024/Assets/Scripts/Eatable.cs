using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eatable : MonoBehaviour
{
    public Collider2D Collider;
    public HealthSystem healthSystem;
    public float scale;
    float baseScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        baseScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSystem.currentHealth <= PlayerMovement.player.GetComponent<HealthSystem>().currentHealth) { Collider.isTrigger = true; }
        else { Collider.isTrigger = false; }

        scale = baseScale * (healthSystem.currentHealth / healthSystem.maxHealth);
        transform.localScale = new(scale, scale, 1);
    }
}
