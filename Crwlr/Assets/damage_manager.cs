using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_manager : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public int healthPoints = 3;
    public float hitCoolDown = 1f;
    public float hitCoolDownTimer;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        hitCoolDownTimer = hitCoolDown;
    }

    void Update() {
        if(healthPoints == 0) {
            //anim.SetBool("Dead",true);
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        if(hitCoolDownTimer > 0) hitCoolDownTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage, Vector2 knockbackDir) {
        if(hitCoolDownTimer <= 0) {
            anim.SetTrigger("TakeDamage");
            rb.AddForce(knockbackDir.normalized*10, ForceMode2D.Impulse);
            healthPoints -= damage;

            hitCoolDownTimer = hitCoolDown;
        }
    }
}
