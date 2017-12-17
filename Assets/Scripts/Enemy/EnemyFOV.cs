using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour {

    public float fieldOfViewAngle = 120f;
    public bool playerInSight;
    public bool disabled = false;

    private SphereCollider col;
    private GameObject player;

    private List<GameObject> closestEnemies;

    int layerMask;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<SphereCollider>();
        closestEnemies = new List<GameObject>();

        layerMask = 1 << 10;
        // This would cast rays only against colliders in layer 10.
        // But instead we want to collide against everything except layer 10. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
    }

    void OnTriggerStay(Collider other)
    {
        if (disabled)
        {
            playerInSight = true;
            return;
        }

        if (other.tag == "Player")
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            // if the player is in the fov
            if (angle < fieldOfViewAngle * 0.5f)
            {                
                if (IsClearSight(other.gameObject))
                {
                    this.PlayerDetected(true);
                }
            }
        }
    }

    public bool IsClearSight(GameObject other)
    {
        Vector3 direction = other.transform.position - transform.position;
        // then we check if there is no obstacle to be able to see the player      
        RaycastHit hit;

        // we use transform up to not cast the raycast from the feet
        // we use col.radius to limit the raycast to the size of the sphere collider
        if (Physics.Raycast(transform.position + transform.up,
                    direction.normalized, out hit, col.radius, layerMask))
        {
            if (hit.collider.tag == "Player")
            {
                return true;
            } else
            {
                return false;
            }
        }

        return false;
    }

    public void PlayerDetected(bool triggerOther)
    {
        disabled = true;
        playerInSight = true;

        if (triggerOther)
        {
            // we also warn all the closest allies
            foreach (GameObject e in closestEnemies)
            {
                if (e != null)
                    e.gameObject.GetComponentInChildren<EnemyFOV>().PlayerDetected(false);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // we add it to the list of close enemy
            closestEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // we add it to the list of close enemy
            closestEnemies.Remove(other.gameObject);
        }
    }
}