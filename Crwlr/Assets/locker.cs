using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class locker : MonoBehaviour
{
    Animator anim;
    public bool locked = false;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Open() {
        if(locked) {
            anim.SetTrigger("Open");
            locked = false;
        }
    }

    public void Close() {
        if(!locked) {
            anim.SetTrigger("Close");
            locked = true;
        }
    }
}
