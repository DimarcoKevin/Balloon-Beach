using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // player variable and distance between player and camera
    public GameObject player;
    public float distance;

    // Late Update is called once per frame after regular Update
    void LateUpdate() {

        // allowing distance to be adjusted if needed
        distance = player.gameObject.transform.position.z - 10;

        // lerp means smoothly move
        // camera follows the ball, trailing by 10 
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, 
            new Vector3(0, gameObject.transform.position.y, distance), 
            Time.deltaTime * 100);
    }
}
