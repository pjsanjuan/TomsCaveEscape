using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

    public PlayerMovement playermovement;
    public PlayerHealth playerhealth;
    bool x = false;
    bool i = false;
    float duration = 2f;
    float magnitude = 1f;

    public Animator anim;
    public bool jump = false;

    // Use this for initialization
    void Start () {
        StartCoroutine(cutsceneStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (playermovement.gameObject.transform.position.x > -296 && !x)
        {
            playermovement.playerRB.MovePosition(playermovement.gameObject.transform.position += (new Vector3(-5, 0, 0) * Time.deltaTime));
            Animate_walk();
        }
        else if (playermovement.gameObject.transform.position.x > -300 && !x)
        {
            playermovement.gameObject.transform.position += (new Vector3(-5, 5, 0) * Time.deltaTime);
            Animate_jump();
        }
        else if (playermovement.gameObject.transform.position.x <-300 && playermovement.gameObject.transform.position.x > -305 && !x)
        {
            playermovement.gameObject.transform.position += (new Vector3(-5, 0, 0) * Time.deltaTime);
            Animate_walk();
        }
        else if (!x)
        {
            x = true;
            playermovement.gameObject.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        }
        else if (playermovement.gameObject.transform.position.x < -250)
        {
            playermovement.playerRB.MovePosition(playermovement.gameObject.transform.position += (new Vector3(5, 0, 0) * Time.deltaTime));
            Animate_walk();
        }
        else if (!i)
        {
            i = true;
            playermovement.gameObject.transform.rotation *= Quaternion.Euler(0, 180f, 0);
            anim.SetBool("IsWalking", false);
        }
    }

    IEnumerator cutsceneStart()
    {
 
        yield return new WaitForSeconds(5);
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        StartCoroutine(Shake());
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(4).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        StartCoroutine(Shake());
        StartCoroutine(Shake());
        yield return new WaitForSeconds(1);
        StartCoroutine(Shake());
        StartCoroutine(Shake());
        StartCoroutine(Shake());
        StartCoroutine(Shake());
        yield return new WaitForSeconds(2);
        //transform.GetChild(5).gameObject.SetActive(false);
        //transform.GetChild(6).gameObject.SetActive(true);
        playerhealth.sceneEnding = true;
    }

    IEnumerator Shake()
    {

        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.current.transform.position;

        while (elapsed < duration)
        {

            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            y *= magnitude * damper;

            Camera.current.transform.position = new Vector3(x += originalCamPos.x, y += originalCamPos.y, originalCamPos.z);

            yield return null;
        }

        Camera.current.transform.position = originalCamPos;
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
