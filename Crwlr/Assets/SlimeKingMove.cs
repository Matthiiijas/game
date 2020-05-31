using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingMove : StateMachineBehaviour
{
    Transform player;
    Rigidbody2D rb;

    float timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();

        timer = animator.GetComponent<SlimeKingController>().bossActionWaitTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       rb.AddForce((player.position - animator.transform.position).normalized * Time.fixedDeltaTime * animator.GetComponent<SlimeKingController>().speed, ForceMode2D.Impulse);

       if(timer <= 0) {
           animator.SetTrigger("Action");
           animator.SetFloat("RandomAction", Random.Range(0,100));
       }

       timer -= Time.fixedDeltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }
}
