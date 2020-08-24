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
        public float dashTime = 2;
        public bool isDashing = true; //checking if we dashing
        public float dashForce;
        
        
     
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
            isDashing = true;
            yield return new WaitForSeconds(dashTime);
        }
    }
    
}

