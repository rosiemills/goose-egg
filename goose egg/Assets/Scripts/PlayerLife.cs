using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D rb;
    public int maxHealth= 3;

    public int damage=1;
    private int currHealth;
    public heartUI healthUI;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currHealth= maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Traps"))
        {

            TakeDamage(damage);
            Debug.Log("currhealth" + currHealth);
        }
        
    }

    private void TakeDamage(int damage)
    {
        currHealth-= damage;
        healthUI.UpdateHearts(currHealth);

        if(currHealth<=0)
        {
            currHealth= 3;
        }
    }

    

    // private void Die()
    // {
    //     rb.bodyType = RigidbodyType2D.Static;
    //     RestartLevel();
    // }

    // private void RestartLevel()
    // {
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    // }
    

}
