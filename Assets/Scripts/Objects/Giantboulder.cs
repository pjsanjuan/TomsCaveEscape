using UnityEngine;
using System.Collections;

public class Giantboulder : MonoBehaviour {
    // Use this for initialization
    Vector3 originalPosition;
    Quaternion originalRotation;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if(Application.loadedLevel == 2)
        rigidbody.AddForce(800000000, 0, 0);
        else
        rigidbody.AddForce(71000, 0, 0);
    }

    void Update()
    {
        
        //Invoke("Reset", 30f);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("floor") || other.gameObject.CompareTag("Lava") || other.gameObject.CompareTag("Final Stage"))
        {
            
        }
        else if (other.gameObject.CompareTag("destructible"))
        {
            rigidbody.useGravity = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            other.gameObject.SetActive(false);
        }
        //if (other.gameObject.CompareTag("floor") )
        //{
        //    other.gameObject.SetActive(true);
        //}

        // if(other.gameObject.CompareTag("door"))
        // {
        //     rigidbody.velocity = Vector3.zero;
        //     rigidbody.angularVelocity = Vector3.zero;
        //     GetComponent<Collider>().isTrigger = false;
        //     GetComponent<Giantboulder>().enabled = false;
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("destructible"))
        {
            rigidbody.useGravity = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

}
