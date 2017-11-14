using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceAttack : EnemyAttack {

    // bullet shoot by enemy
    public GameObject enemyProj;

    // point where the bullet come from
    public GameObject staff;

    // speed of the bullet
    public int enemyBulletSpeed = 280;

    // override parent method Attack()
    protected override void Attack()
    {
        //Debug.Log("Attack override");
        timer = 0f;
        anim.SetInteger("NumAttack", chooseAttack);
        chooseAttack = (++chooseAttack) % numberOfAttacks;
    }

    public void LaunchEnemyBullet()
    {
        // we launch the bullet
        GameObject bullet = (GameObject)Instantiate(enemyProj, staff.transform.position, Quaternion.identity);
        bullet.gameObject.name = "Bullet";
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * enemyBulletSpeed);
    }
}
