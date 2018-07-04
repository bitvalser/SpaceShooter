using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoEnemy : MonoBehaviour {

    public GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Boundary") || (other.tag == "Enemy") || (other.tag == "Enemy"))
        {
            return;
        }
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(other.gameObject);
        if (other.tag == "Player")
        {
            Instantiate(explosion, other.transform.position, other.transform.rotation);
        }
        Destroy(gameObject);
    }
}
