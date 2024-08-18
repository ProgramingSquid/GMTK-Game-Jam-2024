using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRespawn : MonoBehaviour
{
    public GameObject prefab;
    public GameObject RespawnObject;
    public float radius = 1;
    public float respawnTime;
    bool started = false;
    bool respawning = false;
    // Update is called once per frame
    void Update()
    {
        if(respawning) return;
        if (RespawnObject == null && started == false) { Respawn(); started = true; }
        else if (RespawnObject == null) { StartRespawn(); }
    }

    private void Respawn()
    {
        var pos = Random.insideUnitCircle * radius;
        RespawnObject = Instantiate(prefab, transform);
        RespawnObject.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        respawning = false;
    }
    [Button]
    public void StartRespawn()
    {
        respawning = true;
        Destroy(RespawnObject);
        Invoke("Respawn", respawnTime);
    }
}
