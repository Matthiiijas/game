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
    public float shootRange;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        playerPos = player.position - transform.position;
        anim.SetFloat("PlayerPos", playerPos.x);
        if(Random.Range(0.0f,1.0f) < 0.01f && Vector3.Distance(player.position, transform.position) < shootRange) Shoot(playerPos.normalized);
    }

    void FixedUpdate() {

    }

    void Shoot(Vector3 direction) {
        projectile = Instantiate(projectilePrefab, transform);
        Vector2 vel = direction;
        projectile.GetComponent<Rigidbody2D>().velocity = vel * speed;
    }
}
