using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform player;
    Vector2 delta;
    Vector3 desPos = new Vector3(-0.5f,0.0f,-10.0f);

    void FixedUpdate() {
        delta = player.position - transform.position;
        if(delta.x < -10) desPos += new Vector3(-20,0,0);
        if(delta.x >  10) desPos += new Vector3( 20,0,0);
        if(delta.y <  -6) desPos += new Vector3(0,-12,0);
        if(delta.y >   6) desPos += new Vector3(0, 12,0);

        transform.position = Vector3.Lerp(transform.position, desPos, 0.2f);
    }
}
