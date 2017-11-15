using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUICrystal : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = camera.transform.rotation;
	}
}
