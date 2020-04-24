using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_controller : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    Transform player;

    Vector3 target;
    Vector3 randOffset;
    public float currentDistance;
    public float stopDistance = 1.0f;
    public float chaseDistance = 8.0f;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        anim.SetBool("Jump",rb.velocity.magnitude > 0.5f);

        currentDistance = Vector3.Distance(player.position, transform.position);
        randOffset = Random.insideUnitSphere * 0.5f;
        randOffset.z = 0.0f;
        target = player.position + ((transform.position - player.position).normalized * stopDistance) + randOffset;
        if(currentDistance < chaseDistance) rb.position += (Vector2) (target - transform.position) * Time.fixedDeltaTime;
    }
}
