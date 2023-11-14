using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SC_IRPlayer3 : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;

    Rigidbody player3RB;
    bool grounded = false;
    Vector3 defaultScale;
    private bool isCrouching; 

    // Start is called before the first frame update
    void Start()
    {
        player3RB = GetComponent<Rigidbody>();
        player3RB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        player3RB.freezeRotation = true;
        player3RB.useGravity = false;
        defaultScale = transform.localScale;
        isCrouching = false;
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.O) && grounded)
        {
            player3RB.velocity = new Vector3(player3RB.velocity.x, CalculateJumpVerticalSpeed(), player3RB.velocity.z);
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            // Toggle the scale
            if (isCrouching)
                transform.localScale = defaultScale;
            else
            {
                // Scale the object along the Y-axis to 40% of the original height
                Vector3 newScale = defaultScale;
                newScale.y = defaultScale.y * 0.4f;
                transform.localScale = newScale;
            }
            isCrouching = !isCrouching;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // We apply gravity manually for more tuning control
        player3RB.AddForce(new Vector3(0, -gravity * player3RB.mass, 0));
        grounded = false;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Finish"))
        {
            //print("GameOver!");
             SC_GroundGenerator.instance.gameOverPlayer3 = true;
        }
    }
}