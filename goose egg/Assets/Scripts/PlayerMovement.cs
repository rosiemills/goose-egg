using System.Reflection;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public AudioSource jumpSoundEffect;
    public AudioSource landingSoundEffect;



    public float runSpeed = 25f;
    float horizontalMove = 0f;
    bool jump = false;
    bool run = false;

    //allows the flap animation to work well
    public float landRate = 5f;
    float nextLandTime = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Walk") * runSpeed;

        //code works w Animator - can change walk/run animation
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("IsRunning", run);
        animator.SetBool("Attack", fly);

        if(Input.GetButtonDown("Jump")) //up or space key
        {
            jump = true;
            animator.SetBool("IsFlying", true);
            jumpSoundEffect.Play();
        }
        
        if(Input.GetButtonDown("Walk")) //arrows or AD
        {
            run = false;
        }
        if(Input.GetButtonDown("Run"))  //ctrl
        {
            run = true;
        }
        if(Input.GetButtonDown("Attack"))   //f
        {
            fly = true;
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
            nextLandTime = Time.time + 1f / landRate;
            landingSoundEffect.Play();
        }
    }

    void FixedUpdate()
    {
        //Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }
}
