using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;

    Vector3 movement;
    Animator anim;



    Rigidbody playerRigidbody;
    int floorMask; // A layer mask so that a ray can be cast just at gameobjects on the floor
    float camRayLength = 100f;

    private void Awake()
    {
        //setup references
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();

        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // call on every physics update
        float h = Input.GetAxisRaw("Horizontal"); // getraw to have lees variation, 0 or 1 (more reaction because of full speed)
        float v = Input.GetAxisRaw("Vertical");

        // move the player on the scene
        Move();

        // to align the weapon to the move cursor
        Turning();

        // animate the player body
        Animating(h, v);
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
        }
    }

    void Turning()
    {
        // create a ray from the mouse curosor on screen in the direction of the camera
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        // get the information from where the ray is gonna hit the floor
        RaycastHit floorHit;

        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;

            // quaternion is a way of storing a rotation
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;

        if (walking)
        {
            anim.SetFloat("Forward", 1.0f);
        } else
        {
            anim.SetFloat("Forward", 0.0f);
        }
        
    }
}
