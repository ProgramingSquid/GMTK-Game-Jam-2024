using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHealth : MonoBehaviour
{
    public HealthSystem healthSystem;
    public float health = 10;
    [CurveRange(0,0, 1,1 ,EColor.Green)]
    public AnimationCurve bias;
    private void Start()
    {
        //To Fix...
        float amount = bias.Evaluate(Random.Range(0, 1)) * health;
        healthSystem.currentHealth = amount;
        Debug.Log(amount);
    }

}
