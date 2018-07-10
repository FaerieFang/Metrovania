using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class projectileScript : MonoBehaviour {
    public float destroyTime;
    public bool destroy = false;

    void Start () {
        if (destroy){
            Destroy(gameObject, destroyTime);
        }
        
    }
	
	
	void Update () {
		
	}

}

