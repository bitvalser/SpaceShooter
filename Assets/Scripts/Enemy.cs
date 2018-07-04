using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public GameObject shotSpawn;
    public float fireRate;
    public GameObject ammo;
    private float nextFire;
    public GameObject explosion;
    private Rigidbody rb;
    public float tilt;
    public float manevrWait;
    private int dir;

    // Use this for initialization
    void Start () {
        dir = 0;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(RandomManevr());
        //rb.velocity = transform.forward * speed * 1000 * Time.deltaTime;      
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ammo, new Vector3(shotSpawn.transform.position.x, shotSpawn.transform.position.y, shotSpawn.transform.position.z), Quaternion.Euler(0, 0, 0));
        }
    }

    IEnumerator RandomManevr()
    {
        while (true)
        {
            yield return new WaitForSeconds(manevrWait);
            dir = Mathf.CeilToInt(Random.Range(-1, 2));
        }
    }

    private void FixedUpdate()
    {
        if ((transform.position.x < -200))
        {
            dir = -1;
        }
        if ((transform.position.x > 200))
        {
            dir = 1;
        }
        rb.velocity = new Vector3(dir * 2 * speed * 1000 * Time.deltaTime, 0, speed * 1000 * Time.deltaTime);
        rb.rotation = Quaternion.Euler(0.0f, 180, rb.velocity.x * -tilt);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Boundary") || (other.tag == "Enemy") || (other.tag == "FireEnemy"))
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
        if(other.tag == "Fire")
        {
            GameObject.FindWithTag("GameController").GetComponent<GameController>().addScore(100);
        }
        if (other.tag == "Player")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}
