using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Drop {
    public GameObject item;
    public float probability;

    public Drop(GameObject _item, float _probability) {
        item = _item;
        probability = _probability;
    }
}
