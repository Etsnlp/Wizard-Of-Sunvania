using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    // The speed at which the object will dash
    public float dashSpeed = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 1f;

    public float dashFriction = 0.5f;

    
    Animator animator;
    Rigidbody2D rb;
    ParticleSystem particle;

    
    float dashTimer;
    float dashCooldownTimer;

    [HideInInspector]
    public bool isDashing = false;

    PlayerController PlayerC;


    void Start()
    {
        PlayerC = GetComponent<PlayerController>();
        animator = GetComponentInChildren<Animator> ();
        rb = GetComponent<Rigidbody2D>();
        particle = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }


        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                animator.SetBool("Dashing", false);
                isDashing = false;
                rb.drag = dashFriction;
            }
        }


        if (!isDashing && dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }


    void FixedUpdate()
    {
        if (isDashing)
        {           
            rb.AddForce(transform.right * dashSpeed, ForceMode2D.Impulse);
        }
    }


    public void Dash()
    {


        if (!isDashing && dashCooldownTimer <= 0)
        {
            isDashing = true;

            animator.SetBool("Dashing", true);
            particle.Play();
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
            rb.drag = 0;
        }
    }
}
