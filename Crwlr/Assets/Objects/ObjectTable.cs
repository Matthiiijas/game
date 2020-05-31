using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectTable {
    public GameObject item;
    public float probability;

    public ObjectTable(GameObject _item, float _probability) {
        item = _item;
        probability = _probability;
    }
    public static GameObject GetRandom(ObjectTable[] table) {
        if (table.Length > 0) {
            //Validate probabilities
            float probSum = 0;
            for (int i = 0; i < table.Length; i++) {
                probSum += table[i].probability;
            }
            if (probSum != 1) {
                if (probSum == 0) Debug.LogError("Please add probabilities for the table!");
                else {
                    for (int i = 0; i < table.Length; i++) table[i].probability /= probSum;
                    Debug.LogWarning("Drop probabilities do not add up to 100%! Scaled all probabilities to make table work");
                }
            }
            //Pick item
            if (table.Length == 1) {
                return table[0].item;
            }
            else {
                int randomNum;
                do randomNum = Mathf.RoundToInt((Random.value) * table.Length);
                while (randomNum == table.Length || Random.value < table[randomNum].probability);
                return table[randomNum].item;
            }
        }
        else {
            Debug.LogWarning("There are no Objects to choose from");
            return null;
        }
    }
}
