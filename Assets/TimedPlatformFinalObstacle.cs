using UnityEngine;
using System.Collections;

public class TimedPlatformFinalObstacle : MonoBehaviour {

    // Use this for initialization

    GameObject playerOnPlatform;

    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        Invoke("setActiceFalseTimedPlatform", 1f);
        //Invoke("setActiceTrueTimedPlatform", 6f);

    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerOnPlatform = other.gameObject;
        }
    }

    void setActiceFalseTimedPlatform()
    {
        if(playerOnPlatform != null)
        playerOnPlatform.transform.parent = null;

        gameObject.SetActive(false);

        //transform.parent.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0.41176470588f, 0.24705882352f, 0.16078431372f, 1f);
        CancelInvoke("setActiceFalseTimedPlatform");
    }

    void setActiceTrueTimedPlatform()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<TimedPlatform>().enabled = false;
        //transform.parent.GetChild(0).gameObject.SetActive(true);
        CancelInvoke("setActiceTrueTimedPlatform");
    }
}
