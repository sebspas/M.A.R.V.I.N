using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUICrystal : MonoBehaviour {

    private GameObject camera;

	// Use this for initialization
	void Start () {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = camera.transform.rotation;
	}
}
