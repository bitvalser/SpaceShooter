using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    public float speed;
    public GameObject shotSpawn;
    public GameObject shotSpawn1;
    public GameObject shotSpawn2;
    public GameObject shotSpawn3;
    public float fireRate;
    public GameObject ammo;
    private float nextFire;
    public GameObject explosion;
    public GameObject explosionFire;
    private Rigidbody rb;
    public float tilt;
    public int health;
    private int dir;
    public float moveFowardSpeed;

    void Start()
    {
        dir = 0;
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * speed * 1000 * Time.deltaTime;      
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ammo, new Vector3(shotSpawn.transform.position.x, 30, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
            Instantiate(ammo, new Vector3(shotSpawn1.transform.position.x, 30, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
            Instantiate(ammo, new Vector3(shotSpawn2.transform.position.x, 30, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
            Instantiate(ammo, new Vector3(shotSpawn3.transform.position.x, 30, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
        }
    }


    private void FixedUpdate()
    {
        if ((transform.position.x < -130) || (transform.position.x > 130))
        {
            dir = -dir;
        }
        if((transform.position.z < -81) && (moveFowardSpeed != 0))
        {
            moveFowardSpeed = 0;
            dir = -1;
        }
        rb.velocity = new Vector3(dir * speed * 1000 * Time.deltaTime, 0, moveFowardSpeed * 1000 * Time.deltaTime);
        rb.rotation = Quaternion.Euler(0.0f, -90, rb.velocity.x * -tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Boundary") || (other.tag == "Enemy") || (other.tag == "FireEnemy"))
        {
            return;
        }
        
        Destroy(other.gameObject);
        if (other.tag == "Fire")
        {
            health--;
            Instantiate(explosionFire, other.transform.position, other.transform.rotation);
            if (health < 0)
            {
                GameObject.FindWithTag("GameController").GetComponent<GameController>().addScore(1000);
                Destroy(gameObject);
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        if (other.tag == "Player")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }
        
    }
}
