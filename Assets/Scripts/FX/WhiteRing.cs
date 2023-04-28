using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteRing : MonoBehaviour
{
    Vector3 startScale = Vector3.zero;
    float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = startScale;

        Invoke("Disappear", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = transform.localScale + Vector3.one * speed * Time.deltaTime;
        if ((new Vector3(7, 7, 7) - transform.localScale).magnitude < 1.5f)
        {
            Destroy(gameObject);
        }
    }
    void Disappear()
    {
        Destroy(gameObject);
    }
}
