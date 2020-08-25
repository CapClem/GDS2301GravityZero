using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AnthonyY
{
   
    public class Dash : MonoBehaviour
    {
        public CharacterController2D controller;

        [Header("Dash Variables")]
        public Rigidbody2D rb;
        public float dashTime = 2f;
        public bool isDashing; //checking if we dashing
        public float dashForce = 55f;
        
        
     
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        
        
        public void DashEffect()
        {
            if (isDashing)
            {
                {
                    if (controller.m_FacingRight)
                    {
                        rb.velocity = Vector2.right * dashForce;
                    }
                    else
                    {
                        rb.velocity = Vector2.left * dashForce;
                    }
                }
               
            }

        }
        
        public IEnumerator DashTime()
        {
            isDashing = false;
            yield return new WaitForSeconds(dashTime);
            isDashing = true;
        }
    }
    
}

