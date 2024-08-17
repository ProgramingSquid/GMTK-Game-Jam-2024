using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFromHealth : MonoBehaviour
{
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
        scale = baseScale * (healthSystem.currentHealth / healthSystem.maxHealth);
        transform.localScale = new(scale, scale, 1);
    }
}
