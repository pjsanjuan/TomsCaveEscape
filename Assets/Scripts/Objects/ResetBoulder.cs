using UnityEngine;
using System.Collections;



public class ResetBoulder : MonoBehaviour
{

    // Use this for initialization
    Vector3 originalPosition;
    Quaternion originalRotation;

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        rigidbody.AddForce(400, 0, 0);
        Invoke("Reset", 17f);
    }

    void Reset()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        if (rigidbody != null)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        //gameObject.SetActive(false);

        //transform.parent.GetChild(0).gameObject.SetActive(true);
        CancelInvoke("Reset");
    }
};
