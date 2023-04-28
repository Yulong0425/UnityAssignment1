using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float THRESHOLD = 2f;

    public GameObject movingTarget;
    public float movingSpeed = 4f;

    bool leaving = true;
    Vector3 anchor;
    Vector3 targetPos;


    void Start()
    {
        anchor = transform.position;
        targetPos = movingTarget.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((transform.position - targetPos).magnitude < THRESHOLD) leaving = false;
        if ((transform.position - anchor).magnitude < THRESHOLD) leaving = true;

        if (leaving)
        {
            transform.Translate((targetPos - transform.position).normalized * movingSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate((anchor - transform.position).normalized * movingSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
