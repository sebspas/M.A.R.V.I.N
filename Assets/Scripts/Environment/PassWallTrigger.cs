using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassWallTrigger : MonoBehaviour {

    public ZoneWall zoneWall;

    public void PassOnEffect()
    {
        zoneWall.DesactivateWall();
        Destroy(this.gameObject, 0f);
    }
}
