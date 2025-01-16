using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    private Animator animator;

    public bool isDead;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            int randomValue = Random.Range(0, 3);
            if (randomValue == 0) 
            {
                animator.SetTrigger("DIE1");
            }
            else if(randomValue == 1)
            {
                animator.SetTrigger("DIE2");
            }
            else
            {
                animator.SetTrigger("DIE3");
            }

            isDead = true;

           GetComponent<CapsuleCollider>().enabled = false;
            
        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }
    }

}
