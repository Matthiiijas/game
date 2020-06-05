using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeKingController : MonoBehaviour
{
    Transform player;
    Vector2 playerPos;

    public float knockbackStrength;

    public float speed;
    public float jumpHitRadius = 2, jumpKnockback;
    public float bossActionWaitTime = 3;

    public GameObject RoyalSlime;
    public Vector2 leftOffset, rightOffset;

    public GameObject SlimeKingProjectile;
    public float projectileSpeed;

    void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, jumpHitRadius);
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update() {
        playerPos = (Vector2) (player.position - transform.position);
    }

    void JumpToPlayer() {
        transform.position = player.position;
    }

    void JumpDealDamage() {
        float distance = Vector3.Distance(player.position, transform.position);
        if(distance < jumpHitRadius) {
            player.GetComponent<HealthController>().TakeDamage(2, playerPos, jumpKnockback);
        }
    }

    void SpawnRoyal() {
        Instantiate(RoyalSlime,transform.position + (Vector3) leftOffset, Quaternion.identity);
        Instantiate(RoyalSlime,transform.position + (Vector3) rightOffset, Quaternion.identity);
    }

    void Shoot() {
        GameObject shotProjectile = Instantiate(SlimeKingProjectile, transform);
        shotProjectile.GetComponent<Rigidbody2D>().velocity = playerPos.normalized * projectileSpeed;

    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<HealthController>().TakeDamage(1, playerPos, knockbackStrength);
        }
    }
}
