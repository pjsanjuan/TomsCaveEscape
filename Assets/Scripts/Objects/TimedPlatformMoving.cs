using UnityEngine;
using System.Collections;

public class TimedPlatformMoving : MonoBehaviour {

    void Update()
    {
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        Invoke("setActiceFalseTimedPlatform", 3f);
        Invoke("setActiceTrueTimedPlatform", 3.5f);

    }

    void setActiceFalseTimedPlatform()
    {
        gameObject.transform.position += new Vector3(0, -1000, 0);

        //transform.parent.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = new Color(0.41176470588f, 0.24705882352f, 0.16078431372f, 1f);
        gameObject.GetComponent<TimedPlatformMoving>().enabled = false;
        CancelInvoke("setActiceFalseTimedPlatform");
    }

    void setActiceTrueTimedPlatform()
    {
        gameObject.transform.position -= new Vector3(0, -1000, 0);
        gameObject.GetComponent<TimedPlatformMoving>().enabled = false;
        //transform.parent.GetChild(0).gameObject.SetActive(true);
        CancelInvoke("setActiceTrueTimedPlatform");
    }
}
