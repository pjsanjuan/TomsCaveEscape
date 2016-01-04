using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
   // public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public Image screenFaderImage;
    public float fadeSpeed = 1.5f;
    private bool sceneStarting = true;
    public bool sceneEnding = false;


    //Animator anim;
    //AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    public Transform SpawnPoint;


    void Awake ()
    {
        //anim = GetComponent <Animator> ();
        // playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent<PlayerMovement>();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {

        if(sceneStarting)
        {
            StartScene();
        }

        if (sceneEnding)
        {
            EndScene();
        }

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;


        if (transform.position.y < -20 && transform.position.x > -244.5)
        {
            Death();
        }
        else if (transform.position.y < -100)
        {
            Death();
        }
       
    }


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        // playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
			Death ();
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth < 100)
        {
            currentHealth += amount;
            if (currentHealth > 100)
            {
                currentHealth = 100;
            }

            healthSlider.value = currentHealth;
        }
    }


    public void Death ()
    {
		isDead = true;

        // playerShooting.DisableEffects ();

        // anim.SetTrigger ("Die");

        // playerAudio.clip = deathClip;
        // playerAudio.Play ();

        //playerMovement.enabled = false;
        //playerShooting.enabled = false;

        transform.position = SpawnPoint.position;

        isDead = false;

        currentHealth = 100;
        healthSlider.value = currentHealth;
       

    }

    void FadeToClear()
    {
        screenFaderImage.color = Color.Lerp(screenFaderImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        screenFaderImage.color = Color.Lerp(screenFaderImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();

        if(screenFaderImage.color.a <= 0.05f)
        {
            screenFaderImage.color = Color.clear;
            screenFaderImage.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene()
    {
        screenFaderImage.enabled = true;
        FadeToBlack();

        if(screenFaderImage.color.a >= 0.95f)
        {
            if(Application.loadedLevel == 0)
            {
                Application.LoadLevel(1);
            }
            else if(Application.loadedLevel == 1)
            {
                Application.LoadLevel(2);
            }
            else if(Application.loadedLevel == 2)
            {
                Application.LoadLevel(3);
            }
			else if(Application.loadedLevel == 3)
			{
				Application.LoadLevel(4);
			}
            else if (Application.loadedLevel == 4)
            {
                Application.LoadLevel(5);
            }
			else if (Application.loadedLevel == 5)
			{
				Application.LoadLevel(0);
			}
        }
    }


    // public void RestartLevel ()
    // {
    //     Application.LoadLevel (Application.loadedLevel);
    // }

}
