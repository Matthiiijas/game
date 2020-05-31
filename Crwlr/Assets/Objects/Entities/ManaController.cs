using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [Header("Strength")]
    [Tooltip("Mana points = half shards")]
    public int manaPoints;
    public int maxManaPoints = 3;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        manaPoints = maxManaPoints;
    }

    void Update() {
        if(manaPoints > maxManaPoints) manaPoints = maxManaPoints;
    }

    public void ModifyMana(int deltaMana) {
        manaPoints += deltaMana;
    }
}
