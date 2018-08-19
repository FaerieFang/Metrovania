using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    public float speed;
    public float maxOffset;
    public bool directionRight;
    public float offsetVar;
    public float sightLocation;
    void Start () {

    }
	
	void FixedUpdate () {
        directionRight = player.GetComponent<playerMove>().facingRight;
        
        if (speed < 0){
            speed = speed * -1;
        }
        float step = Time.deltaTime * speed;
        if (directionRight){
            offsetVar = player.transform.position.x + sightLocation;
        }
        else{
            offsetVar = player.transform.position.x - sightLocation;
        }
        speed = transform.position.x - offsetVar;
        Vector2 offset = new Vector2(offsetVar,player.transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, offset, step);

        }

    
}

//if (transform.position.x - player.transform.position.x > maxOffset || transform.position.x - player.transform.position.x < -maxOffset){