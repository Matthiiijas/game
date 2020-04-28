using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public AudioClip hitSound;

    [Header("Strength")]
    [Tooltip("Health points = half hearts")]
    public int healthPoints;
    public int maxHealthPoints = 3;
    [Tooltip("Time of invulnerability")]
    public float hitCoolDown = 0.2f;
    [HideInInspector]
    public float hitCoolDownTimer;
    [Space(10)]
    public bool dead;

    public int dropCount;
    public Drop[] drops;
    int randomNum;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if(rb == null) Debug.LogError("Require Rigidbody Component");
        if(anim == null) Debug.LogError("Not visually pleasing without Animator Component, etc.");

        healthPoints = maxHealthPoints;
        hitCoolDownTimer = hitCoolDown;

        if(drops.Length > 0) {
            float probSum = 0;
            for(int i = 0; i < drops.Length; i++) {
                probSum += drops[i].probability;
            }
            if(probSum != 1) {
                if(probSum == 0) Debug.LogError("Please add probabilities for the drops!");
                else {
                    for(int i = 0; i < drops.Length; i++) drops[i].probability /= probSum;
                    Debug.LogWarning("Drop probabilities do not add up to 100%! Scaled all probabilities to make drops work");
                }
            }
        }
    }

    void Update() {
        if(healthPoints > maxHealthPoints) healthPoints = maxHealthPoints;
        if(healthPoints == 0 && !gameObject.CompareTag("Player")) {
            if(!dead && drops.Length > 0) {
                for(int i = 0; i < dropCount; i++) {
                    do randomNum = Mathf.RoundToInt((Random.value-0.00001f) * drops.Length);
                    while(randomNum == drops.Length || Random.value < drops[randomNum].probability);
                    GameObject droppedItem = Instantiate(drops[Random.Range(0,drops.Length)].item,transform.position, Quaternion.identity);
                    droppedItem.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                }
            }
            dead = true;
            Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
        }
        if(hitCoolDownTimer > 0) hitCoolDownTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage, Vector2 knockbackDir, float knockbackStrength) {
        if(hitCoolDownTimer <= 0) {
            anim.SetTrigger("TakeDamage");
            rb.AddForce(knockbackDir.normalized * knockbackStrength, ForceMode2D.Impulse);
            healthPoints -= damage;
            AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);

            hitCoolDownTimer = hitCoolDown;
        }
    }
}
