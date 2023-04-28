using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject BoostSpaceFX;
    public GameObject Camera;
    private Rigidbody movement;

    PlayerCharacter character;

    // Move Control
    float speed = 20f;
    float rotateSpeed = 50f;
    float quickStepSpeed = 60f;

    // Jump Control
    float G = 50f;
    float jumpspeed = 30f;

    // Temp Variables
    float boostSpeed;
    float airAcc;
    float quickStepAcc = 0;
    bool rising = true;
    bool grounded;

    // Animation Control
    GameObject boostFX;
    GameObject boostFX2;

    private void Awake()
    {
        character = GetComponent<PlayerCharacter>();

        speed = character.speed;
        rotateSpeed = character.rotateSpeed;
        quickStepSpeed = character.quickStepSpeed;

        G = character.G;
        jumpspeed = character.jumpspeed;
    }

    void Start()
    {
        boostSpeed = 0;
        airAcc = 0;
        grounded = false;
        Physics.autoSyncTransforms = true;
        movement = GetComponent<Rigidbody>();

        boostFX = GameObject.Find("boostFX");
        boostFX2 = GameObject.Find("boostFX2"); ;
    }

    void Update()
    {
        if (transform.position.y < -10 || Input.GetKey(KeyCode.R))
        {
            Debug.Log("GameOver");
            transform.position = GameObject.Find("Abstract").GetComponent<SpawnPoints>().GetSpawnPoint();
        }
        Actions();
        Audio();
        Animation();
    }

    private void OnTriggerEnter(Collider other)
    {
        NewSpawnPoint(other);
    }
    private void OnTriggerExit(Collider other)
    {
        grounded = false;
        movement.velocity -= Vector3.up * 0.1f;
    }
    private void OnCollisionEnter(Collision collision)
    {
        HurtDetection(collision.collider);
    }

    private void Actions()
    {
        // Boost
        if(Input.GetButton("Boost"))
        {
            boostFX.SetActive(true);
            boostFX2.SetActive(true);
            boostFX.transform.Rotate(new Vector3(0, 0, 1) * 1000 * Time.deltaTime);
            boostFX2.transform.Rotate(new Vector3(0, 0, -1) * 1000 * Time.deltaTime);
            if (Camera.GetComponent<Camera>().fieldOfView < 80) Camera.GetComponent<Camera>().fieldOfView += 2;
            boostSpeed = 2;
        }
        else
        {
            boostFX.SetActive(false);
            boostFX2.SetActive(false);
            if (Camera.GetComponent<Camera>().fieldOfView > 60) Camera.GetComponent<Camera>().fieldOfView -= 2;
            boostSpeed = 0;
        }

        // Quick Step
        if (Input.GetKeyDown(KeyCode.Q)) quickStepAcc = -quickStepSpeed;
        if (Input.GetKeyDown(KeyCode.E)) quickStepAcc = quickStepSpeed;
        if (quickStepAcc > 10) quickStepAcc -= 500 * Time.deltaTime;
        else if (quickStepAcc < -10) quickStepAcc += 500 * Time.deltaTime;
        else { quickStepAcc = 0; }
        transform.Translate(Vector3.right * quickStepAcc * Time.deltaTime);

        // Jump
        airAcc -= G * Time.deltaTime;
        if (rising && movement.velocity.y < 0)
        {
            rising = false;
        }
        else if (!rising && movement.velocity.y == 0)
        {
            airAcc = 0;
            grounded = true;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rising = true;
            grounded = false;
            airAcc = jumpspeed;
        }

        // Horizontal
        movement.velocity = (speed * transform.forward * (Input.GetAxis("Vertical") + boostSpeed) + airAcc * Vector3.up);
        transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up * Input.GetAxis("Horizontal"));
    }

    private void HurtDetection(Collider obj)
    {
        if(obj.tag == "DamageObject")
        {
            Debug.Log("GameOver");
            transform.position = 
                GameObject.Find("Abstract").GetComponent<SpawnPoints>().GetSpawnPoint() + Vector3.up*5;
        }
    }

    private void NewSpawnPoint(Collider obj)
    {
        if(obj.tag == "Spawn")
            GameObject.Find("Abstract").GetComponent<SpawnPoints>().SetSpawnPoint(obj.gameObject);
    }

    void Audio()
    {
    }

    void Animation()
    {
        if (Input.GetButtonDown("Boost"))
        {
            GameObject wr = Instantiate(BoostSpaceFX,transform);
            wr.transform.position = transform.position;
        }
    }
}
