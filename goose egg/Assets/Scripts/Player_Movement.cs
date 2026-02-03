using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D body;
    public BoxCollider2D groundCheck;
    public LayerMask groundMask;

    public float acceleration;
    [Range(0f, 1f)]
    public float groundDecay;   //makes the ground "stickier" the lower the value
    public float maxXSpeed;

    public float jumpSpeed;
    
    private bool grounded;
    private bool jumpPressed;
    float xInput, yInput;
    

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        if(Input.GetButtonDown("Jump"))
        {
            jumpPressed= true;
        }
    }

    //usually for physics updates
    void FixedUpdate()
    {
        CheckGround();
        HandleXMovement();
        HandleJump();
        ApplyFriction();
    }

    void CheckInput()
    {
        xInput = Input.GetAxis("Horizontal");
        //yInput = Input.GetAxis("Vertical");
    }

    void HandleXMovement()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            //increment velocity by our acceleration, then clamp within max
            float increment = xInput * acceleration;
            float newSpeed = Mathf.Clamp(body.linearVelocityX + increment, -maxXSpeed, maxXSpeed);
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocityY);

            FaceInput();
        }
    }

    void FaceInput()
    {
        float direction = Mathf.Sign(xInput);
        transform.localScale = new Vector3(direction, 1, 1);
    }

    void HandleJump()
    {
        if(jumpPressed && grounded)
        {
            //body.linearVelocity = new Vector2(body.linearVelocityX, yInput * jumpSpeed);
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
        }
        jumpPressed= false;
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
        
    }

    void ApplyFriction()
    {
        //extra check negates the ground stickiness if only moving on the x-axis
        if (grounded && xInput == 0 && Mathf.Abs(body.linearVelocityY) == 0)
        {
            body.linearVelocity *= groundDecay;
        }
    }
}
