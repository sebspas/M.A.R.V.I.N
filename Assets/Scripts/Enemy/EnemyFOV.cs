using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {

    public float fieldOfViewAngle = 120f;
    public bool playerInSight;
    public bool disabled = false;

    private SphereCollider col;
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<SphereCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (disabled)
        {
            playerInSight = true;
            return;
        }

        if (other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // if the player is in the fov
            if (angle < fieldOfViewAngle * 0.5f)
            {
                // then we check if there is no obstacle to be able to see the player
                RaycastHit hit;

                // we use transform up to not cast the raycast from the feet
                // we use col.radius to limit the raycast to the size of the sphere collider
                if (Physics.Raycast(transform.position + transform.up,
                    direction.normalized, out hit, col.radius))
                {
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                        disabled = true;
                    }
                }
            }
        }
    }
}