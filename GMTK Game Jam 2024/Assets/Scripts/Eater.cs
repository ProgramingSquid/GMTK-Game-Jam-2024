using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Eater : MonoBehaviour
{
    public HealthSystem healthSystem;
    public UnityEvent OnBite;
    public UnityEvent OnEat;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent<Eatable>(out Eatable eatable);
        if(eatable == null) return;

        var health = eatable.healthSystem;


        if (health.maxHealth > healthSystem.currentHealth) { health.TakeDamage(healthSystem.currentHealth); OnBite.Invoke(); }
        else { OnEat.Invoke(); health.TakeDamage(healthSystem.currentHealth); }
    }
}
