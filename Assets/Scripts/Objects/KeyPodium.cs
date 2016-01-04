using UnityEngine;
using System.Collections;

public class KeyPodium : MonoBehaviour {

    public PlayerHealth playerhealth;

    void Update()
    {
        if(transform.GetChild(1).gameObject.activeSelf.Equals(false))
        {
            transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
            transform.GetChild(3).gameObject.GetComponent<Giantboulder>().enabled = true;
            if (Application.loadedLevel != 1)
            {
                playerhealth.sceneEnding = true;
            }
        }
    }
}
