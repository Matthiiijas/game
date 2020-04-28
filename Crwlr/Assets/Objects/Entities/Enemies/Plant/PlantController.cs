using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    Transform player;
    Animator anim;
    Vector3 playerPos;

    public GameObject projectilePrefab, projectile;
    public float shootWaitTime, shootTimer;
    public float projectileSpeed;
    public float shootRange, shootDelay;
    public Vector3 shootOffset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        shootTimer = shootWaitTime;
    }

    void Update() {
        playerPos = player.position - transform.position - shootOffset;
        anim.SetFloat("PlayerPos", playerPos.x);
        if(shootTimer <= 0 && Vector3.Distance(player.position, transform.position) < shootRange) StartCoroutine(SingleShoot(playerPos.normalized));
        shootTimer -= Time.deltaTime;
    }

    IEnumerator SingleShoot(Vector3 direction) {
        shootTimer = shootWaitTime;
        //Play attack animation
        anim.SetTrigger("Attack");
        //wait for animation
        yield return new WaitForSeconds(shootDelay);
        //Spawn projectile with velocity towards current player position
        projectile = Instantiate(projectilePrefab, transform);
        projectile.transform.position += shootOffset;
        projectile.GetComponent<Rigidbody2D>().velocity = (Vector2) direction * projectileSpeed;
    }
}
