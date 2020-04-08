using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slime_controller : MonoBehaviour
{
    Transform player;
    Vector3 target;
    Vector3 randOffset;
    public float stopDistance = 1.0f;

    Animator anim;
    Rigidbody2D rb;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate() {
        anim.SetBool("Jump",Random.Range(0.0f,1.0f) < 0.2f);

        randOffset = Random.insideUnitSphere * 0.5f;
        randOffset.z = 0.0f;
        target = player.position + ((transform.position - player.position).normalized * stopDistance) + randOffset;
        transform.position = Vector3.Slerp(transform.position, target, 0.05f);
    }
}
