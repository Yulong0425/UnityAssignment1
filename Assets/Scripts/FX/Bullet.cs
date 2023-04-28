using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int speed = 100;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disappear", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void Disappear()
    {
        Destroy(gameObject);
    }
}
