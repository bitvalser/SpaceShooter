using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    public float tiltx;
    public float tilty;
    public GameObject shotSpawn;
    public GameObject ammo;
    public float fireRate;
    public GameObject TouchPad;
    private float nextFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        if(Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(ammo, new Vector3(shotSpawn.transform.position.x, 36, shotSpawn.transform.position.z) , Quaternion.Euler(0, 0, 0));
        }

    }
    private void FixedUpdate()
    {
        Vector3 move = Vector3.zero;

        Vector2 direct = TouchPad.GetComponent<TouchPad>().getDirect();
        move = new Vector3(direct.x * speed * 1000 * Time.deltaTime, 0, direct.y * speed * 1000 * Time.deltaTime);

        //move = new Vector3(Input.GetAxis("Horizontal") * speed * 1000 * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * 1000 * Time.deltaTime);
        rb.rotation = Quaternion.Euler(rb.velocity.z * -tilty, 180, rb.velocity.x * tiltx);
        if ((transform.position.x < -200))
        {
            transform.position = new Vector3(-200, transform.position.y, transform.position.z);
        }
        if ((transform.position.x > 200))
        {
            transform.position = new Vector3(200, transform.position.y, transform.position.z);
        }
        if ((transform.position.z < -781))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -781);
        }
        rb.velocity = move;
    }
}
