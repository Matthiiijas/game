using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]

public class ChestController : MonoBehaviour
{
    Animator anim;

    public GameObject Mimic;
    public float mimicSpawnProb;

    bool open;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && !open) {
            if(Random.value < mimicSpawnProb) {
                GameObject mimic = Instantiate(Mimic, transform.position, Quaternion.identity);
                mimic.transform.parent = transform.parent;
                Destroy(gameObject);
            }
            else {
                anim.SetTrigger("Open");
                open = true;
                GetComponent<DropManager>().Drop();
            }
        }
    }
}
