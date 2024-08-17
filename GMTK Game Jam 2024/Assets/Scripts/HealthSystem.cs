using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealed;
    public UnityEvent OnDeath;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    [Button]
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnDamaged.Invoke();
        if(currentHealth <= 0) OnDeath.Invoke();
    }
    [Button]
    public void Heal(float amount)
    {
        currentHealth += amount;
        OnHealed.Invoke();
    }
    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
