using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;

    public float speed;
    public float attackSpeed, retreatSpeed;

    bool attacking;

    public float currentDistance;
    public float stopDistance = 1.5f;
    public float chaseDistance = 8.0f;

    public float animationMinVelocity = 0.1f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if(!attacking) {
            anim.SetBool("Jump",rb.velocity.magnitude > animationMinVelocity);

            currentDistance = Vector3.Distance(player.position, transform.position);
            if(currentDistance > stopDistance) rb.AddForce((Vector2) (player.position - transform.position).normalized * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
            else if(Random.value < 0.1f) StartCoroutine(Attack(player.position));
        }
    }

    IEnumerator Attack(Vector3 target) {
        rb.AddForce((Vector2) (target - transform.position) * attackSpeed, ForceMode2D.Impulse);
        attacking = true;
        yield return new WaitForSeconds(1);
        attacking = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(attacking && col.gameObject.CompareTag("Player")) col.gameObject.GetComponent<DamageManager>().TakeDamage(1,player.position - transform.position);
        rb.AddForce((Vector2) (-player.position + transform.position).normalized * retreatSpeed, ForceMode2D.Impulse);
    }
}
