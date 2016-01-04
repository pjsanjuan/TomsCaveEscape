using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
	AudioSource pistolShot;
	AudioSource pistolReload;

    //Animator variable
    Animator anim;

    //Melee variables
    float meleeTimer;
    public Collider macheteCollider;

    //PlayerStuff
    public Transform origin;
    public bool shootMode = false; //shootMode true means the camera pulls close and the player aims based on rayCast. false means the player shoots directly forward
    public GameObject bullet;

    //Shooting and Ammo variables
    public float fireRate;
    public float spread; //amount to scatter bullet targets by
    public int maxAmmoCount = 24; //max ammo in gun at once
    public int ammoCount; //current ammo in gun
    public int ammoStore = 216; //current ammo not in gun
    public float reloadSpeed = 2f;
    public bool reloading = false;

    //melee variables
    public float meleeAttackRate;

    //shooting variables
    PlayerMovement playerMovement;
    float fireTimer; //time since the player last shot
    Vector3 shotDirection;
    float reloadTimer;

    public Image crossHair;


    // Use this for initialization
    void Start()
    {
		AudioSource[] audios = GetComponents<AudioSource> ();
		pistolShot = audios [0];
		pistolReload = audios [1];
        //Initialize animator component
        anim = GetComponent<Animator>();
        //Initialize movement
        playerMovement = GetComponent<PlayerMovement>();

        //Initialize ammo
        ammoCount = maxAmmoCount;

        //Initialize machete
        macheteCollider.enabled = false;

 
    }


    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        meleeTimer += Time.deltaTime;

        if (Input.GetButton("Fire2"))
        {
            shootMode = true;
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            shootMode = false;
            crossHair.gameObject.SetActive(false);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (shootMode && fireTimer > fireRate && ammoCount > 0 && !reloading)
            {
                PlayerShoot();
				pistolShot.Play ();
            }
            else if (meleeTimer > meleeAttackRate)
            {
                PlayerMelee();
            }
        }

        if (Input.GetKeyDown("r") && ammoStore > 0)
        {
            reloading = true;
			pistolReload.Play ();
        }

        if (reloading)
        {
            Reload();
        }

        Animate_raisegun();

       
    }

    void PlayerShoot()
    {
        fireTimer = 0;

        shotDirection = playerMovement.GetAimRayDirection();

        shotDirection.x += Random.Range(-spread, spread);
        shotDirection.y += Random.Range(-spread, spread);
        shotDirection.z += Random.Range(-spread, spread);

        Instantiate(bullet, origin.position, Quaternion.LookRotation(shotDirection));

        Debug.DrawRay(transform.position, shotDirection);

        ammoCount--;
    }

    public Vector3 GetShotDirection()
    {
        return shotDirection;
    }

    void Reload()
    {
        reloadTimer += Time.deltaTime; //count reload time
        if (reloadTimer > reloadSpeed)
        { //when reload period has passed

            if (ammoStore >= maxAmmoCount - ammoCount)
            { //if stored ammo is less than ammo to be loaded
                ammoStore -= maxAmmoCount - ammoCount; //subtract appropriate number of bullets from stored ammo
                ammoCount = maxAmmoCount; //set loaded ammo to max
            }
            else
            { //if there is not enough stored ammo left to fully reload
                ammoCount += ammoStore; //add all stored ammo to loaded ammo
                ammoStore -= ammoStore; //subtract all stored ammo
            }
            reloadTimer = 0; //reloading is finished
            reloading = false;
        }
    }

    //Add ammo
    public void PickUpAmmo(int amountToAdd)
    {
        ammoStore += amountToAdd;
    }


    void PlayerMelee()
    {
        //Code to make character swing upon melee
        Animate_swing();
        //meleeTimer = 0;
        macheteCollider.enabled = true;

        //Wait few seconds before disabling macheteCollider
        //Disable macheteCollider after swing
        Invoke("DisableMacheteCollider", 0.85f);
    }

    void DisableMacheteCollider()
    {
        macheteCollider.enabled = false;
    }

    //Animations
    void Animate_swing()
    {
        anim.SetTrigger("Swing");
    }

    void Animate_raisegun()
    {
        anim.SetBool("Raise", shootMode);
    }

}