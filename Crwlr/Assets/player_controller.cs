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
    public float attackDistance;
    public LayerMask enemyLayers;
    public float attackCoolDown, attackCoolDownTimer;
    public Vector3 attackOffset;

    //Move
    InputMaster controls;
    Vector2 inputMove, realMove, remainMove;
    public float speed;

    public bool transitioning = false;

    void Awake () {
        //Define Components
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        //Get Transform of attackPoint
        attackPoint = transform.Find("attackPoint");
        //Retrieve inputs
        controls = new InputMaster();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Attack.started += ctx => Attack();
        //Setup Attack Cooldown Timer
        attackCoolDownTimer = attackCoolDown;
    }

    void FixedUpdate () {
        //Attack Cooldown Timer
        if(attackCoolDownTimer > 0) attackCoolDownTimer -= Time.fixedDeltaTime;
        //Set position of attackPoint
        if(inputMove != Vector2.zero) attackPoint.position = transform.position + attackOffset+ (Vector3) realMove.normalized * attackDistance;

        //Play Idle and Movement animations
        Animate();

        //Movement
        if(!transitioning) realMove = Vector2.ClampMagnitude(inputMove,1);
        else realMove.Normalize();
        rb.velocity = realMove*speed;
    }

    void Animate() {
        //Define last direction
        if(inputMove != Vector2.zero) remainMove = inputMove;
        //Set Values for Animator
        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("remainX", remainMove.x);
        anim.SetFloat("remainY", remainMove.y);
        anim.SetFloat("Velocity", inputMove.magnitude);
    }

    void Attack() {
        //If Cooldown is over...
        if(attackCoolDownTimer <= 0) {
            //Play attack animation
            anim.SetTrigger("Attack");
            //Collect all enemies in attackRange an -Distance...
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies) {
                //...and deal damage
                enemy.gameObject.GetComponent<damage_manager>().TakeDamage(1,remainMove);
            }
            //Reset Cooldown Timer
            attackCoolDownTimer = attackCoolDown;
        }
    }

    //Draw attackRange circle
    void OnDrawGizmosSelected() {
        attackPoint = transform.Find("attackPoint");
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
