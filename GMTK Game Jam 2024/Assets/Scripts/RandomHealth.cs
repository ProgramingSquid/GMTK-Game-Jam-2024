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
        float time = Random.Range(0f, 1f);
        float amount = bias.Evaluate(time) * health;
        healthSystem.currentHealth = amount;
        Debug.Log($"Amount: {amount}");
        Debug.Log($"Time: {time}");
    }

}
