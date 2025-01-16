using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAttackingState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;
    Enemy enemy;

    float stopAttackingDistance = 2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        Punch(animator);
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        LookAtPlayer();
        
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        if (distanceFromPlayer > stopAttackingDistance)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("PUNCH1");
        animator.ResetTrigger("PUNCH2");
    }

    private void Punch(Animator anim)
    {

        int randomValue = UnityEngine.Random.Range(0, 2);
        Debug.Log(randomValue);
        if (randomValue == 0)
        {
            anim.SetTrigger("PUNCH1");
        }
        else if (randomValue == 1)
        {
            anim.SetTrigger("PUNCH2");
        }
    }

    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
