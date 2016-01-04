using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using UnityEditor;


    public class PlayerMovement : MonoBehaviour
    {
        private const string kStandardSpritePath = "UI/Skin/UISprite.psd";

        public Camera cam;
        public float speed;
        public float turnSpeed;
        public float jumpSpeed;
        public float raycastdistance;
        public float timeBetweenAttacks = 2f;
		public Transform bulletOrigin;
        //raycastdistance will have to be adjusted depending on the center of the 
        //gameobject used

        public Rigidbody playerRB;
        PlayerHealth PlayerHealth;
        float timer = 2.1f;
        bool HasKey = false;
        Vector3 movement;

        public GameObject InventoryBar;
        public Sprite image;
        bool ItemPickedUp = false;
        bool ItemUsed = false;
        public Sprite Defaultimage;
        string UsedItemName;
        public Text Checkpoint;
        public Color CheckpointColour = new Color(255f, 248f, 0f, 30);


        Animator anim;
        public bool jump = false;
        PlayerAttack playerAttack;
        Ray aimRay;
        Vector3 aimRayDirection;
        RaycastHit aimInfo;




    // Use this for initialization
    void Awake()
    {
        playerRB = GetComponent<Rigidbody>();

        PlayerHealth = GetComponent<PlayerHealth>();

        Defaultimage = AssetDatabase.GetBuiltinExtraResource<Sprite>(kStandardSpritePath);
        image = Defaultimage;

        Cursor.visible = false;

        playerAttack = GetComponent<PlayerAttack>();
        anim = GetComponent<Animator>();
    }

       void Update()
        {

		print (HasKey);
            if (Input.GetButtonDown("Jump"))
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.down, out hit, raycastdistance);
            

            if (hit.collider != null && !hit.collider.CompareTag("Lava"))
                {
                    playerRB.velocity = Vector3.zero;
                    playerRB.angularVelocity = Vector3.zero;
                    jump = true;
                }
               
            }



        }



    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Move(h, v);
        Animate_walk(h, v);


        //if (Input.GetKeyDown("t"))
        //   {
        //    //if(playerRB.useGravity)
        //    //playerRB.useGravity = false;
        //    //else
        //    //playerRB.useGravity = true;
        //    playerRB.MovePosition(new Vector3(-200.4f, 1.152f, -2.300f));
        //    //PlayerHealth.sceneEnding = true;
        //}

        if (jump)
        {
            //anim.SetTrigger("Jump");
   
            playerRB.AddForce(new Vector2(0f, jumpSpeed));
            Animate_jump();
            jump = false;
        }


        //if (Input.GetButtonDown("Jump"))
        //{
        //    RaycastHit hit;
        //    Physics.Raycast(transform.position, Vector3.down, out hit, raycastdistance);
        //    if (hit.collider != null)
        //    {
        //        Jump();
        //    }
        //    else
        //    {
        //        Debug.Log("Hi");
        //        //playerRB.velocity = Vector3.zero;
        //        //playerRB.angularVelocity = Vector3.zero;
        //        //Invoke("ResetPlayerVelocity", 3f);
        //    }
        //}

        Checkpoint.color = Color.Lerp(Checkpoint.color, Color.clear, 3f * Time.deltaTime);

        ResetPlayerVelocity();
    }

    void ResetPlayerVelocity()
    {
        playerRB.velocity -= new Vector3 (playerRB.velocity.x,0,playerRB.velocity.z);
        playerRB.angularVelocity -= new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
        //CancelInvoke("ResetPlayerVelocity");
    }


    public void Move(float h, float v)
    {
        movement += cam.transform.forward * v;
        movement += cam.transform.right * h;
        movement.y = 0;

        movement = movement.normalized * speed * Time.deltaTime;

        if (movement != Vector3.zero && !playerAttack.shootMode)
        {
            Rotate(movement);
        }
        else if (playerAttack.shootMode)
        {
            ShootRotate();
        }

        playerRB.MovePosition(transform.position + movement);

        movement = Vector3.zero;
    }

    //public void Jump()
    // {
    //     //		Vector3 jumpmovement = new Vector3(0,20,0);
    //      playerRB.velocity = Vector3.zero;
    //      playerRB.angularVelocity = Vector3.zero;
    //      playerRB.AddForce(new Vector3(0,1,0) * jumpSpeed);
    // }

    void Rotate(Vector3 direction)
    {
        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;

        Quaternion newRot = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, turnSpeed * Time.deltaTime);
    }

    void UpdateRay()
    {
        aimRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
		Debug.DrawRay (aimRay.origin, aimRayDirection*200);

        if (Physics.Raycast(aimRay, out aimInfo))
        {
			aimRayDirection = aimInfo.point - bulletOrigin.position;
        }
        else
        {
            aimRayDirection = aimRay.direction;
        }
    }

    void ShootRotate()
    {
        UpdateRay();

        Vector3 direction = aimRayDirection;

        direction.y = 0;

        direction = Quaternion.AngleAxis(90, Vector3.up) * direction;

        Quaternion newRot = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, turnSpeed * Time.deltaTime);
    }


    public Ray GetAimRay()
    {
        return aimRay;
    }

    public Vector3 GetAimRayDirection()
    {
        return aimRayDirection;
    }

    public RaycastHit GetAimInfo()
    {
        return aimInfo;
    }

    //Lawrence added code
    void OnCollisionEnter(Collision other)
    {
        
    }

    void OnCollisionStay(Collision other)
    {
        

        if (!Input.GetButtonDown("Jump"))
        {
            ResetPlayerVelocity();
        }
        
        //if (other.gameObject.CompareTag("Boulder"))
        //{
        //    if (timer > timeBetweenAttacks)
        //    {
        //        timer -= timeBetweenAttacks;
        //        // ... attack.
        //        Attack(30);
        //    }
        //    timer += Time.deltaTime * 3;
        //}
    }

    //Animations
    void Animate_walk(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }

    void Animate_jump()
    {
        anim.SetTrigger("Jump");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Giant Boulder") && Application.loadedLevel == 2)
        {
            Application.LoadLevel(2);
        }

        if (other.gameObject.CompareTag("Final Stage"))
        {
            other.gameObject.GetComponent<PickRandomPlatform>().enabled = true;

            other.transform.GetChild(0).gameObject.SetActive(true);
            other.transform.GetChild(1).gameObject.GetComponent<MoveXFinalObstacle>().enabled = true;
            other.transform.GetChild(1).gameObject.GetComponent<TimedPlatformFinalObstacle>().enabled = true;
        }

        if (other.gameObject.CompareTag("Spawn Point"))
        {
            PlayerHealth.SpawnPoint = other.transform;

            Checkpoint.color = CheckpointColour;
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            enabled = false;

            PlayerHealth.sceneEnding = true;
        }

        if (other.gameObject.CompareTag("Timed Platform"))
        {
            other.gameObject.GetComponent<TimedPlatform>().enabled = true;
        }

        if (other.gameObject.CompareTag("Timed Platform 2"))
        {
            other.gameObject.GetComponent<TmedPlatform2>().enabled = true;
        }

        if (other.gameObject.CompareTag("Moving Timed Platform"))
        {
            other.gameObject.GetComponent<TimedPlatformMoving>().enabled = true;
        }

		if (other.gameObject.CompareTag("Ammo Pick Up"))
		{
			
			other.gameObject.SetActive(false);
			
			playerAttack.PickUpAmmo(24);
		}
    }    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Health Pick Up"))
        {
            other.gameObject.SetActive(false);

            PlayerHealth.Heal(30);

        }

        if (other.gameObject.CompareTag("Lava"))
        {

            if (timer > timeBetweenAttacks)
            {
                timer -= timeBetweenAttacks;
                // ... attack.
                Attack(20);
            }
            timer += Time.deltaTime * 3;
        }

        if (other.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown("e") && HasKey)
            {
                other.gameObject.SetActive(false);
                UsedItemName = "key";
                ItemUsed = true;
                HasKey = false;
            }
        }
        

        if (other.gameObject.CompareTag("Key"))
        {
            HasKey = true;
            other.gameObject.SetActive(false);
            image = Resources.Load<Sprite>("key");
            ItemPickedUp = true;
        }

        if (other.gameObject.CompareTag("platform trigger"))
        {
            if (Input.GetKeyDown("e"))
            {
                other.gameObject.GetComponent<Renderer>().material.color = Color.green;
                other.transform.parent.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.green;
            }
        }

        if (other.gameObject.CompareTag("move if player on platform"))
        {
            if (other.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color.Equals(Color.green))
            {
                other.gameObject.GetComponent<MoveX>().enabled = true;
            
                transform.parent = other.transform;
                playerRB.velocity = Vector3.zero;
                playerRB.angularVelocity = Vector3.zero;
            }
        }

        if (other.gameObject.CompareTag("Midway Platform"))
        {
            if (other.transform.GetChild(0).gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().material.color.Equals(Color.green) && other.transform.GetChild(1).gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color.Equals(Color.green) && other.transform.GetChild(2).gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Renderer>().material.color.Equals(Color.green))
            {
              other.transform.GetChild(0).transform.GetChild(1).GetComponent<MoveX>().enabled = true;
              other.transform.GetChild(1).transform.GetChild(1).GetComponent<MoveX>().enabled = true;
              other.transform.GetChild(2).transform.GetChild(1).GetComponent<MoveX>().enabled = true;

              playerRB.velocity = Vector3.zero;
              playerRB.angularVelocity = Vector3.zero;
            }
        }

        if (other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("Moving Timed Platform"))
        {
            transform.parent = other.transform;
            //playerRB.velocity = Vector3.zero;
            //playerRB.angularVelocity = Vector3.zero;
            
        }


        if (other.gameObject.CompareTag("Ladder"))
        {
            playerRB.useGravity = false;
            
            if(Input.GetKey("e"))
            {
                transform.Translate(Vector3.up * Time.deltaTime * 10);
            }
            else if(Input.GetKey("x"))
            {
                transform.Translate(Vector3.down * Time.deltaTime * 10);
            } 
        }


        if (other.gameObject.CompareTag("Boulder"))
        {
            Attack(100);
            //other.gameObject.SetActive(false);
            //
            //other.transform.parent.GetChild(1).gameObject.SetActive(true);
            //other.transform.parent.GetChild(1).gameObject.GetComponent<Rigidbody>().AddForce(0, 0, 20000);

            //Destroy(other.transform.parent.GetChild(1).gameObject, 10f);
        }


        if (other.gameObject.CompareTag("Spike Trap"))
        {
            if (timer > timeBetweenAttacks)
            {
                timer -= timeBetweenAttacks;
                // ... attack.
                Attack(20);
            }
            timer += Time.deltaTime * 2;
        }

    if (ItemPickedUp || ItemUsed)
        {
            if (ItemPickedUp)
            {
                for (int i = 0; i < 7; ++i)
                {
                    if (InventoryBar.transform.GetChild(i).GetComponent<Image>().sprite.name.Equals("UISprite"))
                    {
                        InventoryBar.transform.GetChild(i).GetComponent<Image>().sprite = image;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < 7; ++i)
                {
                    if (InventoryBar.transform.GetChild(i).GetComponent<Image>().sprite.name.Equals(UsedItemName))
                    {
                        InventoryBar.transform.GetChild(i).GetComponent<Image>().sprite = Defaultimage;
                        break;
                    }
                }
            }
            ItemPickedUp = false;
            ItemUsed = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            playerRB.useGravity = true;
        }

        if (other.gameObject.CompareTag("platform") || other.gameObject.CompareTag("Moving Timed Platform"))
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("move if player on platform"))
        {
            transform.parent = null;
        }

        if (other.gameObject.CompareTag("Boulder"))
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }

        if (other.gameObject.CompareTag("Spike Trap"))
        {
            playerRB.velocity = Vector3.zero;
            playerRB.angularVelocity = Vector3.zero;
        }

    }

    public void Attack(int amount)
    {
        timer = 0f;

        // If the player has health to lose...
        if (PlayerHealth.currentHealth > 0)
        {
            // ... damage the player.
            PlayerHealth.TakeDamage(amount);
        }
    }

       

    }
