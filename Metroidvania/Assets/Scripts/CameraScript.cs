using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject player;
    public float speed;

    void Start () {

    }
	
	void FixedUpdate () {

        speed = transform.position.x - player.transform.position.x;
        if (speed < 0){
            speed = speed * -1;
        }
        float step = Time.deltaTime * speed;
        Vector2 offset = player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, offset, step);

    }
}