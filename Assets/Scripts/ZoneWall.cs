using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWall : MonoBehaviour {

    public GameObject wall;
    public GameObject effect;
    public float timeToFade = 2f;
    BoxCollider invisibleWall;

	// Use this for initialization
	void Start () {
        effect.SetActive(false);
        invisibleWall = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DesactivateWall()
    {
        Destroy(wall, timeToFade);
        effect.SetActive(true);
        Destroy(effect, timeToFade+1f);
        invisibleWall.isTrigger = true;
    }
}
