using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plant_controller : MonoBehaviour
{
    Transform player;
    Animator anim;
    Vector3 playerPos;

    public GameObject projectilePrefab, projectile;
    public float speed;
    public float shootRange, shootDelay;
    public Vector3 shootOffset;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        playerPos = player.position - transform.position - shootOffset;
        anim.SetFloat("PlayerPos", playerPos.x);
        if(Random.Range(0.0f,1.0f) < 0.01f && Vector3.Distance(player.position, transform.position) < shootRange) StartCoroutine(Shoot(playerPos.normalized));
    }

    IEnumerator Shoot(Vector3 direction) {
        //Play attack animation
        anim.SetTrigger("Attack");
        //wait for animation
        yield return new WaitForSeconds(shootDelay);
        //Spawn projectile with velocity towards current player position
        projectile = Instantiate(projectilePrefab, transform);
        projectile.transform.position += shootOffset;
        projectile.GetComponent<Rigidbody2D>().velocity = (Vector2) direction * speed;
    }
}
