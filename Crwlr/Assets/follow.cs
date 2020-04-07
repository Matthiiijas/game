using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform player;
    Vector2 delta;
    Vector3 desPos = new Vector3(0.0f,0.0f,-10.0f);

    void FixedUpdate() {
        delta = player.position - transform.position;
        if(delta.x < -9.5) desPos += new Vector3(-19,0,0);
        if(delta.x >  9.5) desPos += new Vector3( 19,0,0);
        if(delta.y <  -5.5) desPos += new Vector3(0,-11,0);
        if(delta.y >   5.5) desPos += new Vector3(0, 11,0);

        transform.position = Vector3.Lerp(transform.position, desPos, 0.2f);
    }
}
