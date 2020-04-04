using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class add_room : MonoBehaviour
{
    private room_templates templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<room_templates>();
        templates.rooms.Add(this.gameObject);
    }
}
