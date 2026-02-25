using System.Reflection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;


    public float runSpeed = 25f;
    float horizontalMove = 0f;
    bool jump = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Walk") * runSpeed;

        //code works w Animator - can change walk/run animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsFlying", true);
        }
        if(Input.GetButtonDown("Walk"))
        {
            runSpeed = 25f;
        }
        if(Input.GetButtonDown("Run"))
        {
            runSpeed = 50f;
        }
        
    }

    public void OnLanding()
    {
        animator.SetBool("IsFlying", false);
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
