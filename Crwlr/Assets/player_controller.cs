using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;

    InputMaster controls;
    Vector2 inputMove, realMove, inputLook, remainLook = new Vector2(0,-1), vel;
    bool attack = false;
    bool attackedThisFrame = false;
    public bool transitioning = false;

    public float speed;
    public string orientation;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        attackPoint = GameObject.FindWithTag("attack_point").GetComponent<Transform>();

        controls = new InputMaster();
        controls.Player.Move.performed += ctx => inputMove = ctx.ReadValue<Vector2>();
        controls.Player.Look.performed += ctx => inputLook = ctx.ReadValue<Vector2>();
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
        if(inputMove != Vector2.zero) remainLook = inputMove;
        if(inputLook != Vector2.zero) remainLook = inputLook;

        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("LookX", inputLook.x);
        anim.SetFloat("LookY", inputLook.y);
        anim.SetFloat("remainLookX", remainLook.x);
        anim.SetFloat("remainLookY", remainLook.y);
        anim.SetFloat("Velocity", inputMove.magnitude);
    }

    void Attack() {
        anim.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            Destroy(enemy.gameObject);
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
