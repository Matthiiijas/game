using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    //Attack
    Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    bool attack = false;
    bool attackedThisFrame = false;

    //Move
    InputMaster controls;
    Vector2 inputMove, realMove, remainMove;
    public float speed;

    public bool transitioning = false;

    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        attackPoint = GameObject.FindWithTag("attack_point").GetComponent<Transform>();

        controls = new InputMaster();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Attack.started += ctx => Attack();
    }

    void FixedUpdate () {
        if(attack && !attackedThisFrame) {
            Attack();
            attackedThisFrame = true;
        }
        if(!attack) attackedThisFrame = false;

        Animate();

        //Movement
        if(!transitioning) realMove = Vector2.ClampMagnitude(inputMove,1);
        else realMove.Normalize();
        rb.velocity = realMove*speed;
    }

    void Animate() {
        if(inputMove != Vector2.zero) remainMove = inputMove;

        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("remainX", remainMove.x);
        anim.SetFloat("remainY", remainMove.y);
        anim.SetFloat("Velocity", inputMove.magnitude);
    }

    void Attack() {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            enemy.gameObject.GetComponent<damage_manager>().TakeDamage(1,remainMove);
        }
    }

    /*void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }*/

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
