using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playformLayerMask;

    //target the movement script
    public CharacterController2D contoller;

    //movement stats
    float horiontalMove = 0f;
    public float runSpeed = 40f;

    //is the player?
    private bool jump = false;
    private bool crouch = false;

    public Collider2D boxCollider2D;

    Animator ani;

    public float propelSpeed = 30f;
    private Rigidbody2D rb;
    private bool useJetpack = false;

    float currentGravity;

    //Jetpack Fuelbar
    public int fuelRemaining;
    public int maxFuel = 100;
    public int fuelDrain = 1;

    public FuelBar fuelBar;



    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fuelRemaining = maxFuel;
        fuelBar.setTotalFuelLevel(maxFuel);
    }

    // Update is called once per frame
    void Update()
    {
        horiontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            ani.SetBool("Walking", true);
        }
        else
        {
            ani.SetBool("Walking", false);
        }

        //jump & Jectpack
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                jump = true;
                print("You have jumped, " + jump);
                ani.SetBool("Jump", true);
                //canUseJetpack = true;
            }
            else
            {
                useJetpack = true;
                currentGravity = rb.gravityScale;
                rb.gravityScale = 1f;
                
                //Add initial fuel boost
                if (rb.velocity.y < 0 && !(fuelRemaining <= 0))
                {
                    rb.velocity = new Vector3(0, 1, 0);
                    fuelRemaining -= fuelDrain * 2; 
                }
                
            }
        }

        //stop using the jetpack
        if (Input.GetButtonUp("Jump"))
        {
            useJetpack = false;
            if (currentGravity != 0)
            {
                rb.gravityScale = currentGravity;
            }
        }

        //crouch
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            print("You are crouching");
            ani.SetBool("Crouch", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            ani.SetBool("Crouch", false);
        }
    }

    void FixedUpdate()
    {
        contoller.Move(horiontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        ani.SetBool("Jump", false);

        if (useJetpack == true)
        {
            
            if ((fuelRemaining - fuelDrain) <= 0)
            {
                fuelRemaining = 0;
                useJetpack = false;
            }
            else
            {
                rb.AddForce(transform.up * propelSpeed * transform.localScale.y);
                fuelRemaining -= fuelDrain;
            }
            fuelBar.SetFuelLevel(fuelRemaining);
            //print("You are using the jetpack");

        }

        if (IsGrounded())
        {
            useJetpack = false;

            //regain fuel
            if (fuelRemaining + fuelDrain <= maxFuel)
            {
                fuelRemaining += fuelDrain;
                
            }
            else if (fuelRemaining != maxFuel)
            {
                fuelRemaining = maxFuel;
            }
            fuelBar.SetFuelLevel(fuelRemaining);
        }
    }

    //Ref https://www.youtube.com/watch?v=c3iEl5AwUF8
    // determins if the player is on the ground or not
    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, playformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + extraHeightText), Vector2.right * 2 * (boxCollider2D.bounds.extents.x), rayColor);
        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }


}