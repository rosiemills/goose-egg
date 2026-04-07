using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
//using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        //f to attack
        if(Input.GetButtonDown("Fire1"))
        {
            Attack();
            print("attack");
        }
    }

    void Attack()
    {
        // Play an attack animation
        animator.SetTrigger("Attack");

        // Deteck enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            UnityEngine.Debug.Log("We hit " + enemy.name);
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
