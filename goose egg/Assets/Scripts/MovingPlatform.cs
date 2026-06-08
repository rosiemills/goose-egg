//completed by emily
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatform : MonoBehaviour
{
    public Transform startPoint; 
    public Transform endPoint; 
    public float speed= 3f;
    public float waitTime = 1f; 
    int direction= 1;
    float waitCounter = 0f;
    bool isWaiting = false;


    Transform platform;
    private void Awake()
    {
        platform = transform;
    }
    // Update is called once per frame
    private void Update()
    {

        if (isWaiting)
        {
            waitCounter -= Time.deltaTime;
            if (waitCounter <= 0f)
            {
                isWaiting = false;
                direction *= -1;   
            }
            return; 
        }

        Vector2 target = currentMovementTarget();
        platform.position = Vector2.MoveTowards(platform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2)platform.position).magnitude;
        if (distance <= 0.2f)
        {
            isWaiting = true;
            waitCounter = waitTime;
        }    
    }

    Vector2 currentMovementTarget()
    {
        if (direction==1)
        {
            return endPoint.position;
        }
        else
        {
            return startPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }


}
