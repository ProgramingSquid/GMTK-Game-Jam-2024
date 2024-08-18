using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameObjects : MonoBehaviour
{
    public GameObject gameObjectToSpawn;
    public float radius;
    public Vector2 size;
    public int maxAmountOfTries = 30;

    [Button]
    public void Spawn()
    {
        var points = PoissonDiscSampling.GeneratePoints(radius, size, maxAmountOfTries);
        foreach (var point in points)
        {
            Instantiate(gameObjectToSpawn, (Vector3)point + transform.position, Quaternion.identity, transform);
        }
    }
}
