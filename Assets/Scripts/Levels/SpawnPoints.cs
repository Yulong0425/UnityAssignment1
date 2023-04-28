using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 spawnPoint;

    void Start()
    {
        
    }

    public void SetSpawnPoint(GameObject obj)
    {
        spawnPoint = obj.transform.position;
        Destroy(obj.gameObject);
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }
}
