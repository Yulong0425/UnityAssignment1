using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject BoostSpaceFX;
    public GameObject FireFX;
    public GameObject Position;
    public GameObject Target;
    public float movingSpeed = 100;

    enum states {IDLE, POSITIONING, AIM, CHARGE, FIRE};
    public int state;

    Vector3 anchor;
    // Start is called before the first frame update
    void Start()
    {
        anchor = transform.position;
        state = 0;
    }

    bool charged = false;
    // Update is called once per frame
    void Update()
    {
        if(state >= 2)
        {
            if((transform.position - Position.transform.position).magnitude < 50)
            transform.position = new Vector3(transform.position.x, transform.position.y, Position.transform.position.z);
        }

        if (state == (int)states.IDLE)
        {
            transform.position = anchor;
        }
        if (state == (int)states.POSITIONING)
        {
            if ((transform.position - Position.transform.position).magnitude < 5) StateSwitch();
            transform.LookAt(Position.transform);
            transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
        }
        if (state == (int)states.AIM)
        {
            transform.LookAt(Target.transform);
        }
        if (state == (int)states.CHARGE)
        {
            if(!charged)
            {
                GameObject whiteRing = Instantiate(BoostSpaceFX, transform);
                whiteRing.transform.position = transform.position - Vector3.up;
                charged = true;
            }
        }
        if (state == (int)states.FIRE)
        {
            Fire();
            state = 5;
            charged = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            state = 1;
        //Invoke( "StateSwitch", 3);
    }
    private void OnTriggerExit(Collider other)
    {
        state = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Robot Dest");
        if (collision.collider.tag == "Terrain")
        {
            GameObject whiteRing = Instantiate(BoostSpaceFX, transform);
            whiteRing.transform.position = transform.position + Vector3.forward * 3;
            whiteRing.transform.SetParent(null);
            Destroy(gameObject);
        }
    }

    void StateSwitch()
    {
        if (state != 0)
        {
            state = state >= 4 ? 2 : state + 1;
            Invoke("StateSwitch", 1f);
        }
    }

    int bulletCount = 0;
    void Fire()
    {
        if(bulletCount == 5)
        {
            bulletCount = 0;
        }
        else
        {
            GameObject bullet = Instantiate(FireFX, transform);
            bullet.transform.position = transform.position - Vector3.up;
            //bullet.transform.rotation = (new Quaternion(transform.rotation.x, 180, transform.rotation.z, 0));
            bullet.transform.SetParent(this.transform);
            bulletCount += 1;
            Invoke("Fire", 0.1f);
        }
    }
}
