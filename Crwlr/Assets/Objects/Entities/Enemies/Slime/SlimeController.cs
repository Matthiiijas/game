using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Material mat;
    Transform player;

    public AudioClip attackSound;
    AudioSource source;
    public int attackDamage;

    public float speed, attackSpeed, retreatSpeed;

    bool attacking;
    HealthController HealthController;

    public float currentDistance;
    public float stopDistance = 1.5f;
    public float chaseDistance = 8.0f;
    public float attackWaitTime;

    public float knockbackStrength;

    public float animationMinVelocity = 0.1f;

    public bool randomColor = false;
    public float hueMin, hueMax;

    void Start() {
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HealthController = GetComponent<HealthController>();
        mat = GetComponent<SpriteRenderer>().material;

        if(randomColor) mat.SetColor("_Color",Random.ColorHSV(hueMin/256f, hueMax/256f, 0.5f, 0.7f, 1f, 1f));
    }

    void FixedUpdate() {
        if(!attacking) {
            anim.SetBool("Jump",rb.velocity.magnitude > animationMinVelocity);


            currentDistance = Vector3.Distance(player.position, transform.position);
            Vector2 force = (Vector2) (player.position - transform.position).normalized * Time.fixedDeltaTime * speed;
            if(currentDistance > stopDistance) rb.AddForce(force, ForceMode2D.Impulse);
            else if(HealthController.hitCoolDownTimer <= 0) StartCoroutine(Attack(player.position));
        }
    }

    IEnumerator Attack(Vector3 target) {
        attacking = true;
        anim.SetTrigger("AttackIdle");
        yield return new WaitForSeconds(attackWaitTime);
        rb.AddForce((Vector2) (target - transform.position).normalized * attackSpeed, ForceMode2D.Impulse);
        anim.SetTrigger("Attack");
        AudioSource.PlayClipAtPoint(attackSound, transform.position, 1);
        yield return new WaitForSeconds(1);
        attacking = false;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(attacking && !GetComponent<HealthController>().dead && col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<HealthController>().TakeDamage(attackDamage, player.position - transform.position, knockbackStrength);
            rb.AddForce((Vector2) (-player.position + transform.position).normalized * retreatSpeed, ForceMode2D.Impulse);
        }
    }
}
