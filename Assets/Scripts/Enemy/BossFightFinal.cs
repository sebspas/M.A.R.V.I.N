using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightFinal : BossFight
{
    // Simple way to get this script
    public FinalWave f;

    public new void DestroyWall()
    {
        f.DestroyWall();
    }
}
