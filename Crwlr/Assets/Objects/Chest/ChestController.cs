using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]

public class ChestController : MonoBehaviour
{
    Animator anim;

    public GameObject[] dropList;
    GameObject currentItem;
    public int numberOfItems;
    public float throughOutStrength = 1.0f;
    bool open;

    void Start() {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player") && !open) {
            anim.SetTrigger("Open");
            open = true;
            for (int i = 0; i < numberOfItems; i++) {
                currentItem = Instantiate(dropList[Random.Range(0,dropList.Length)], transform.position, Quaternion.identity);
                currentItem.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * throughOutStrength, ForceMode2D.Impulse);
            }
        }
    }
}
