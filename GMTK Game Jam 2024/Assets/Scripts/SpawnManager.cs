using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<SpawnGameObjects> spawners;
    bool spawned = false;
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn()
    {
        if (spawned) return;

        foreach (var spawn in spawners) { spawn.Spawn(); }
        spawned = true;
    }
}
