using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public int dropCount;
    public ObjectTable[] drops;
    int randomNum;

    public void Drop() {
        for(int i = 0; i < dropCount; i++) {
            GameObject itemToDrop = ObjectTable.GetRandom(drops);
            if(itemToDrop != null) {
                GameObject droppedItem = Instantiate(itemToDrop, transform.position, Quaternion.identity);
                droppedItem.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
                droppedItem.GetComponent<Rigidbody2D>().AddForce(Random.insideUnitCircle * 10,ForceMode2D.Impulse);
            }
        }
    }
}
