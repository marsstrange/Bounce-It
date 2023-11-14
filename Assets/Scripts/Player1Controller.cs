using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SC_IRPlayer : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;

    Rigidbody player1RB;
    bool grounded;
    Vector3 defaultScale;
    private bool isCrouching; 

    // Start is called before the first frame update
    void Start()
    {
        player1RB = GetComponent<Rigidbody>();
        player1RB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        player1RB.freezeRotation = true;
        player1RB.useGravity = false;
        defaultScale = transform.localScale;
        isCrouching = false;
        grounded = false;
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.W) && grounded)
        {
            player1RB.velocity = new Vector3(player1RB.velocity.x, CalculateJumpVerticalSpeed(), player1RB.velocity.z);
        }
        
        // Crouching
        if (Input.GetKeyDown(KeyCode.S)) 
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
        player1RB.AddForce(new Vector3(0, -gravity * player1RB.mass, 0));
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
             SC_GroundGenerator.instance.gameOverPlayer1 = true;
        }
    }
}