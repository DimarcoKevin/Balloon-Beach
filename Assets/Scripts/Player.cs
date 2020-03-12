using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject sceneManager;
    public float playerSpeed = 1500;
    public float directionSpeed = 20;
    public AudioClip scoreUp;
    public AudioClip damage;


    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        // This if statement will only run when we are editing, this won't be used for final product
        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            float moveHorizontal = Input.GetAxis("Horizontal");

            transform.position = Vector3.Lerp(gameObject.transform.position, // lerp is used for smooth transitions between two location
                new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -2.5f, 2.5f), // locking left position to -2.5, middle position to 0 and right position to 2.5
                gameObject.transform.position.y, gameObject.transform.position.z), directionSpeed * Time.deltaTime); // the time it takes to transfer positions
        #endif

        // moves the player forward
        GetComponent<Rigidbody>().velocity = Vector3.forward * playerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.right * GetComponent<Rigidbody>().velocity.z / 4);

        // mobile controls
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));

        if (Input.touchCount > 0) {
            transform.position = new Vector3(touch.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "scoreup") {
            GetComponent<AudioSource>().PlayOneShot(scoreUp, 1.0f);
        }
        if (other.gameObject.tag == "triangle") {
            GetComponent<AudioSource>().PlayOneShot(damage, 1.0f);
            sceneManager.GetComponent<App_Initialize>().GameOver();
        }
    }
}
