using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

    Rigidbody Platform;
    float i;
    float timer = 0;
    
	// Use this for initialization
	void Start () {
    
        Platform = GetComponent<Rigidbody>();
        i = 0.05f;
	
	}
	
	// Update is called once per frame
	void Update () {
    
        timer += 1;
    
        if (timer < 150)
        {
            transform.position += new Vector3 (0,0,-i);
        }
        else if(timer < 200)
        {
            //Platform.velocity = Vector3.zero;
            //Platform.angularVelocity = Vector3.zero;
        }
        else
        {
            i *= -1;
            timer = 0;
        }
	}

 

}
