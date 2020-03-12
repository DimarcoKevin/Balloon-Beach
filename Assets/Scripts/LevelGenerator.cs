using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
        StartCoroutine("Transfer");
    }

    IEnumerator Transfer() {
        Debug.Log("Ran");
        yield return new WaitForSeconds(1);
        gameObject.transform.parent.position = new Vector3(0, 0, gameObject.transform.position.z + 200);
        Debug.Log("Ran one second ago");
    }
    

}
