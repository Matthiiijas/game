using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    Transform player;
    Vector2 playerPos;
    Vector2 lookDir;

    public float speed, stopDistance, retreatDistance, chaseDistance;
    public float shootWaitTime, shootTimer;

    public GameObject skullProjectile;
    public float projectileSpeed, shootKnockback;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        shootTimer = shootWaitTime;
    }

    void FixedUpdate() {
        anim.SetFloat("remainX", playerPos.x);
        anim.SetFloat("remainY", playerPos.y);

        playerPos = (Vector2) (player.position - transform.position);
        if(playerPos.magnitude > stopDistance && playerPos.magnitude < chaseDistance) rb.AddForce(playerPos.normalized * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);
        else if(playerPos.magnitude < retreatDistance) rb.AddForce(-playerPos.normalized * Time.fixedDeltaTime * speed, ForceMode2D.Impulse);

        if(playerPos.magnitude < stopDistance &&  shootTimer <= 0) StartCoroutine(Shoot(playerPos));
        else shootTimer -= Time.fixedDeltaTime;
    }

    IEnumerator Shoot(Vector2 direction) {
        shootTimer = shootWaitTime;
        GameObject projectile = Instantiate(skullProjectile, transform);
        projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * projectileSpeed;
        rb.AddForce(-direction.normalized * shootKnockback, ForceMode2D.Impulse);
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(!GetComponent<HealthController>().dead && col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<HealthController>().TakeDamage(1, playerPos, 5);
        }
    }
}
