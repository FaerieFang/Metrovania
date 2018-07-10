using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wyvernScript : MonoBehaviour {
    public GameObject player;
    public float speed;

	void Start () {
		
	}
	
	
	void Update () {
        float step = Time.deltaTime * speed;
        Vector2 offset = player.transform.position;
        offset.y += 0.2f;
        transform.position = Vector3.MoveTowards(transform.position, offset, step);
    }
}
