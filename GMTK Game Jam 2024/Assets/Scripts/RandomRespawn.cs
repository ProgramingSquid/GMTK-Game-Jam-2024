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
    // Update is called once per frame
    void Update()
    {

    }

    private void Respawn()
    {
        var pos = Random.insideUnitCircle * radius;
        RespawnObject = Instantiate(prefab, transform);
        RespawnObject.transform.position += new Vector3(pos.x, pos.y, 0f);
    }
    [Button]
    public void StartRespawn()
    {
        Destroy(RespawnObject);
        Invoke("Respawn", respawnTime);
    }
}
