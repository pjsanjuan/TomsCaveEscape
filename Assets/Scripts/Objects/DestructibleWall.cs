using UnityEngine;
using System.Collections;

public class DestructibleWall : MonoBehaviour {

    public GameObject remains;

    void Update()
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Instantiate(remains, transform.position, transform.rotation);
        //    Destroy(gameObject);
        //}
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Giant Boulder"))
        {
            Instantiate(remains, transform.position += new Vector3(50,0,0), transform.rotation);
            Destroy(gameObject);
        }
    }
}
