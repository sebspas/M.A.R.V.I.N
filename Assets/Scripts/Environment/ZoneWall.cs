using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWall : MonoBehaviour {

    public GameObject wall;
    public GameObject effect;
    public float timeToFadeWall = 2f;
    public float additionalEffectTime = 0f;
    BoxCollider invisibleWall;

	// Use this for initialization
	void Start () {
        effect.SetActive(false);
        invisibleWall = GetComponent<BoxCollider>();
    }
	
	// Update is called once per frame
	void Update () {
		if (wall == null)
        {
            invisibleWall.isTrigger = true;
        }
	}

    public void DesactivateWall()
    {
        Destroy(wall, timeToFadeWall);
        effect.SetActive(true);
        Destroy(effect, timeToFadeWall + additionalEffectTime);
    }
}
