using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    Rigidbody2D rb;
    public int damageToDeal;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Enemy") && !other.CompareTag("Item") && !other.isTrigger) {
            if(other.GetComponent<HealthController>() != null) other.GetComponent<HealthController>().TakeDamage(damageToDeal,rb.velocity, 0);
            Destroy(gameObject);
        }
    }
}
