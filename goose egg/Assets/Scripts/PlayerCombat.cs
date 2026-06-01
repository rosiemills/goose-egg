using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

/*
 * Allows player to attack enemies.
 * Code by Brackeys: https://www.youtube.com/watch?v=sPiVz1k-fEs&t=678s
 */

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    

    public float attackRange = 0.5f;
    public int attackDamage = 40;
    
    public float attackRate = 2f; 
    float nextAttackTime = 0f;

    // Update is called once per frame
    void Update()
    {
        //f to attack
        //attacks can only happen every so often (can't be spammed)
        if(Time.time >= nextAttackTime)
        {
            if(Input.GetButtonDown("Attack"))   //f
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Deteck enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage them
        foreach(Collider2D e in hitEnemies)
        {
            Enemy enemy = e.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
           return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
