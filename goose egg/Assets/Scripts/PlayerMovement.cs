using System.Reflection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;


    public float runSpeed = 25f;
    float horizontalMove = 0f;
    bool jump = false;
    bool run = false;

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

        if(Input.GetButtonDown("Jump")) //up or space key
        {
            jump = true;
            animator.SetBool("IsFlying", true);
        }
        
        if(Input.GetButtonDown("Walk")) //arrows or AD
        {
            run = false;
            animator.SetBool("IsRunning", false);
        }
        if(Input.GetButtonDown("Run"))  //ctrl
        {
            run = true;
            animator.SetBool("IsRunning", true);
        }

        if(run == false)    //walking
        {
            runSpeed = 25f;
        }
        else    //running
        {
            runSpeed = 65f;
        }
        
    }

    public void OnLanding()
    {
        //checks if touching ground - changes to floor animations if so
        animator.SetBool("IsFlying", false);
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
