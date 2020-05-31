using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicController : MonoBehaviour
{
    Transform player;
    Animator anim;
    Rigidbody2D rb;

    Vector2 playerPos;

    public bool jumping;

    public float jumpThrust;
    public float jumpCooldown, jumpCooldownTimer;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        jumpCooldownTimer = jumpCooldown;
    }

    void Update() {
        playerPos = player.position - transform.position;

        if(!jumping && jumpCooldownTimer <= 0) {
            rb.AddForce(playerPos.normalized * jumpThrust, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            jumpCooldownTimer = jumpCooldown;
        }

        if(jumpCooldownTimer > 0) jumpCooldownTimer -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<HealthController>().TakeDamage(1,playerPos,rb.velocity.magnitude);
        }
    }
}
