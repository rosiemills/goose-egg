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

    public float landRate = 5f;
    float nextLandTime = 0f;

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
            print("jump");
        }
        
        if(Input.GetButtonDown("Walk")) //arrows or AD
        {
            run = false;
            animator.SetBool("IsRunning", false);
            print("walk");
        }
        if(Input.GetButtonDown("Run"))  //ctrl
        {
            run = true;
            animator.SetBool("IsRunning", true);
            print("run");
        }

        if(run == false)    //walking
        {
            runSpeed = 25f;
        }
        else    //running
        {
            runSpeed = 75f;
        }
        
    }

    public void OnLanding()
    {
        //checks if touching ground - changes to floor animations if so
        if(Time.time >= nextLandTime)
        {
            animator.SetBool("IsFlying", false);
            print("grounded");
            nextLandTime = Time.time + 1f / landRate;
        }
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
