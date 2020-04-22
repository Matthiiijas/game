using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_controller : MonoBehaviour
{
    Rigidbody2D rb;
    public int damageToDeal;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("enemy") && !other.isTrigger) {
            if(other.GetComponent<damage_manager>() != null) other.GetComponent<damage_manager>().TakeDamage(damageToDeal,rb.velocity);
            Destroy(gameObject);
        }
    }
}
