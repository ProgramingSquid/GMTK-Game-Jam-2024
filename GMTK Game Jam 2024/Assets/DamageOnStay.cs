using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamageOnStay : MonoBehaviour
{
    public float damage;
    public float damageTime;
    public float damageTimer;

    private void Update()
    {
        damageTimer -= Time.deltaTime;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(!collision.TryGetComponent(out HealthSystem health)) return;

        if(damageTimer <= 0)
        {
            health.TakeDamage(damage);
            damageTimer = damageTime;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out HealthSystem health)) return;
        damageTimer = damageTime;
    }
}
