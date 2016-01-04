using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneScriptEnding : MonoBehaviour
{

    public PlayerMovement playermovement;
    public PlayerHealth playerhealth;

    bool x = false;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
         if (playermovement.gameObject.transform.position.x > 193)
        {
            playermovement.playerRB.MovePosition(playermovement.gameObject.transform.position += (new Vector3(-5, 0, 0) * Time.deltaTime));
            Animate_walk();
        }
        else if (playermovement.gameObject.transform.position.x > 150)
        {
            playermovement.gameObject.transform.position += (new Vector3(-5, 0, 0) * Time.deltaTime);
            Animate_walk();
        }
        else 
        {
            playerhealth.sceneEnding = true;
        }
        
    }

     
    

    //Animations
    void Animate_walk()
    {
        // Create a boolean that is true if either of the input axes is non-zero.

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", true);
    }

    void Animate_jump()
    {
        anim.SetTrigger("Jump");
    }
}
