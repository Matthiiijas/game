using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public AudioClip hitSound;

    [Header("Strength")]
    [Tooltip("Health points = half hearts")]
    public int healthPoints;
    public int maxHealthPoints = 3;
    public bool invulnerable;
    [Tooltip("Time of invulnerability")]
    public float hitCoolDown = 0.2f;
    [HideInInspector]
    public float hitCoolDownTimer;
    [Space(10)]
    DropManager dropper;
    public bool dead;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(rb == null) Debug.LogError("Require Rigidbody Component");
        if(anim == null) Debug.LogError("Not visually pleasing without Animator Component, etc.");

        healthPoints = maxHealthPoints;
        hitCoolDownTimer = hitCoolDown;

        dropper = GetComponent<DropManager>();
    }

    void Update() {
        if(healthPoints > maxHealthPoints) healthPoints = maxHealthPoints;
        if(healthPoints <= 0 && !gameObject.CompareTag("Player")) {
            if(!dead) {
                dropper.Drop();
                StartCoroutine(Die());
            }
            dead = true;
        }
        if(hitCoolDownTimer > 0) hitCoolDownTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage, Vector2 knockbackDir, float knockbackStrength) {
        if(!invulnerable && hitCoolDownTimer <= 0) {
            anim.SetTrigger("TakeDamage");
            rb.AddForce(knockbackDir.normalized * knockbackStrength, ForceMode2D.Impulse);
            healthPoints -= damage;
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);

            hitCoolDownTimer = hitCoolDown;
        }
    }

    public IEnumerator Die() {
        anim.SetTrigger("Die");
        //rb.simulated = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
