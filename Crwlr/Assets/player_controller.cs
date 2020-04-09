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
    Vector2 movement,moveremain = new Vector2(0,-1), vel;
    bool attack = false;
    bool attackedThisFrame = false;

    public float speed;
    public string orientation;


    void Awake () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        attackPoint = GameObject.FindWithTag("attack_point").GetComponent<Transform>();

        controls = new InputMaster();
        controls.player.move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.player.Attack.started += ctx => attack = true;
        controls.player.Attack.canceled += ctx => attack = false;
    }

    void FixedUpdate () {
        if(attack && !attackedThisFrame) {
            Attack();
            attackedThisFrame = true;
        }
        if(!attack) attackedThisFrame = false;

        Animate();

        //Movement
        movement = Vector2.ClampMagnitude(movement,1);
        rb.velocity = movement*speed;
    }

    void Animate() {
        if(movement != Vector2.zero) moveremain = movement;
        anim.SetFloat("MoveX", rb.velocity.x);
        anim.SetFloat("MoveY", rb.velocity.y);
        anim.SetFloat("LookX", moveremain.x);
        anim.SetFloat("LookY", moveremain.y);
        anim.SetFloat("Velocity", movement.magnitude);
        anim.SetBool("Attack", attack);
    }

    void Attack() {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            Destroy(enemy.gameObject);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void OnEnable () {
        controls.Enable();
    }

    void OnDisable () {
        controls.Disable();
    }
}
