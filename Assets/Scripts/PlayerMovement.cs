using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public LayerMask playformLayerMask;

    //target the movement script
    public CharacterController2D contoller;

    //movement stats
    float horiontalMove = 0f;
    public float runSpeed = 40f;

    //is the player?
    private bool jump = false;
    private bool crouching = false;

    public Collider2D boxCollider2D;

    private Animator ani;
    


    //Jet Hover Variables
    public float propelSpeed = 30f;
    private Rigidbody2D rb;
    public bool useJetpack = false;
    public ParticleSystem jetEffect;
    public ParticleSystem jumpDust;

    public RaycastHit2D hit;

    //Jetpack Dash Variables
    public float dashSpeed = 50;
    private float abilityTimer;
    public float startAbilityTimer = 5f;
    private bool dashActivatable = false;
    public ParticleSystem dashEffect;
    private float origGrav;

    //Jetpack Fuelbar UI
    public int fuelRemaining;
    public int maxFuel = 100;
    private int fuelDrain = 1;
    public float startGravity;

    public FuelBar fuelBar;

    public bool regenFuel = false;

    //Coyote time
    private float jumpDelayTime = 0.20f;
    private float dashDelayTime = 0.5f;

    //can the player jump bool
    public bool canIJump = true;
    public bool haveIAlreadyJumped = false;

    bool walking = false;
    public bool ShouldILandMyJump = false;
    bool checkLandJump = false;

    float landJumpTimer = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {        
        ani = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fuelRemaining = maxFuel;
        fuelBar.setTotalFuelLevel(maxFuel);
        fuelBar.setTotalDashLevel(startAbilityTimer);

        abilityTimer = startAbilityTimer;
    }   

    // Update is called once per frame
    void Update()
    {
        horiontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            if (walking == false)
            {
                walking = true;
                ani.SetBool("Walking", true);
                ani.SetTrigger("StartWalking");
            }

        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (walking == true)
            {
                walking = false;
                ani.SetBool("Walking", false);
            }
        }

        //jump & Jectpack
        if (Input.GetButtonDown("Jump"))
        {
            if ( /* IsGrounded() || */ canIJump == true && haveIAlreadyJumped == false)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/VoiceOvers/Jumping&Landing/Jump", this.gameObject);
                jump = true;
                StartCoroutine(jumpResetTime());
                ani.SetTrigger("Jump");
                jumpDust.Play();
            }
            else if(IsGrounded() != true && canIJump == false || IsGrounded() != true && haveIAlreadyJumped == true)
            {
                useJetpack = true;


                startGravity = rb.gravityScale;

                rb.gravityScale = 1f;

                //Add initial fuel boost
                if (rb.velocity.y < 0 && !(fuelRemaining >= 100))
                {
                    rb.velocity = new Vector3(0, 1, 0);
                    fuelRemaining += fuelDrain * 2; 
                }                
            }
        }

        //stop using the jetpack
        if (Input.GetButtonUp("Jump"))
        {
            if (useJetpack == true)
            {
                rb.gravityScale = startGravity;
                useJetpack = false;                
            }
            jetEffect.Stop();
            ani.SetBool("UsingJetpack", false);
        }

        //crouch
        if (Input.GetButtonDown("Crouch"))
        {
            //if we only want to crouch whilst on the ground
            if (IsGrounded())
            {
                crouching = true;
                print("You are crouching");
            }
            //ani.SetBool("Crouch", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouching = false;
            //ani.SetBool("Crouch", false);
        }
        
    }

    void FixedUpdate()
    {
        landJumpTimer -= Time.fixedDeltaTime;
        if(landJumpTimer <= 0)
        {
            ShouldILandMyJump = true;
        }
        else
        {
            ShouldILandMyJump = false;
        }


        contoller.Move(horiontalMove * Time.fixedDeltaTime, crouching, jump);
        jump = false;


        //Dash Cooldown Timer
        if (abilityTimer <= 0)
        {
            dashActivatable = true;           
        }
        else
        {
            abilityTimer -= Time.fixedDeltaTime;
            fuelBar.SetDashLevel(abilityTimer);
        }

        //Dash Ability
        if (dashActivatable == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) //Dash Input
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/Player/JetpackDash", this.gameObject);
                dashActivatable = false; //Resetting the dash's activatability: Now the player can't use it again until it's activated by the cooldown
                abilityTimer = startAbilityTimer;
                dashEffect.Play();
                Vector2 myVelocity = rb.velocity;

                if (contoller.m_FacingRight == true)
                {
                    if (myVelocity.x < 0)
                    {
                        rb.velocity = new Vector3(1, 0.5f, 0);
                        myVelocity = rb.velocity;
                    }

                   myVelocity += Vector2.right * dashSpeed; //DashRight
                    StartCoroutine(DashCoyoteTime(dashDelayTime, myVelocity));
                }
                else if (contoller.m_FacingRight == false)
                {
                    if (rb.velocity.x > 0)
                    {
                        rb.velocity = new Vector3(-1, 0.5f, 0);
                        myVelocity = rb.velocity;
                    }

                    myVelocity += Vector2.left * dashSpeed; //DashLeft       
                    StartCoroutine(DashCoyoteTime(dashDelayTime, myVelocity));
                }
            }
        }

        if (useJetpack == true)
        {
            if ((fuelRemaining + fuelDrain) >= 100)
            {
                FMODUnity.RuntimeManager.PlayOneShotAttached("event:/VoiceOvers/Jetpack/JetPackFuel", this.gameObject);
                fuelRemaining = 100;
                useJetpack = false;
                rb.gravityScale = startGravity;
                jetEffect.Stop();
            }
            else
            {
                rb.AddForce(Vector3.up * propelSpeed * transform.localScale.y);
                fuelRemaining += fuelDrain;
                jetEffect.Play();
                ani.SetBool("UsingJetpack", true);
            }

            fuelBar.SetFuelLevel(fuelRemaining); 
        }

        if (IsGrounded())
        {
            useJetpack = false;

            //regain fuel
            if (!(fuelRemaining - fuelDrain < 0) && regenFuel == true)
            {
                fuelRemaining -= fuelDrain;

            }
            else if (fuelRemaining != 0 && regenFuel == true)
            {
                fuelRemaining = 0;
            }
            fuelBar.SetFuelLevel(fuelRemaining);

            //check if player has already jumped
            canIJump = true;
            ani.SetBool("InAir", false);
        }
        else
        {
            //check if the player was touching the floor recently
            ani.SetBool("InAir", true);
            StartCoroutine(jumpDelay());
        }
    }

    //Ref https://www.youtube.com/watch?v=c3iEl5AwUF8
    // determins if the player is on the ground or not
    public bool IsGrounded()
    {
        //do we need to flip ground check
        Vector2 x;
        int y;
        if (contoller.m_UpRight == true)
        {
            x = Vector2.down;
            y = 1;
        }
        else
        {
            x = Vector2.up;
            y = -1;
        }

        float extraHeightText = 0.1f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, x, extraHeightText, playformLayerMask);
        Color rayColor;
        hit = raycastHit;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            if (ShouldILandMyJump == true)
            {
                ani.SetTrigger("LandJump");
                landJumpTimer = 0.5f;
                ShouldILandMyJump = false;
                StartCoroutine(landSlowdown());
            }
            landJumpTimer = 0.5f;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), x * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), x * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, (boxCollider2D.bounds.extents.y + extraHeightText)*y), Vector2.right * 2 * (boxCollider2D.bounds.extents.x), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(jumpDelayTime);

        if (!IsGrounded())
        {
            canIJump = false;
        }            
    }
    IEnumerator jumpResetTime() 
    {
        if (haveIAlreadyJumped != true)
        {
            haveIAlreadyJumped = true;
            yield return new WaitForSeconds(jumpDelayTime);
            haveIAlreadyJumped = false;
        }
    }

    IEnumerator landSlowdown()
    {
        float s = runSpeed;
        runSpeed = 20;
        yield return new WaitForSeconds(0.5f);
        runSpeed = s;
    }

    IEnumerator DashCoyoteTime(float time, Vector3 yVelocity)
    {     
        rb.velocity = yVelocity;
        yield return new WaitForSeconds(time);
         yVelocity.y -= 1 * Time.deltaTime;
      //  rb.velocity = yVelocity;
    }

}

/*IEnumerator landJump()
{
     yield return new WaitForSeconds(0.5f);
     if(!IsGrounded())
     {
     ShouldILandMyJump = true;
     }   
}*/
