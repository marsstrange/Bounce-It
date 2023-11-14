using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SC_IRPlayer4 : MonoBehaviour
{
    public float gravity = 20.0f;
    public float jumpHeight = 2.5f;

    Rigidbody player4RB;
    bool grounded = false;
    Vector3 defaultScale;
    private bool isCrouching; 

    // Start is called before the first frame update
    void Start()
    {
        player4RB = GetComponent<Rigidbody>();
        player4RB.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        player4RB.freezeRotation = true;
        player4RB.useGravity = false;
        defaultScale = transform.localScale;
        isCrouching = false;
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded)
        {
            player4RB.velocity = new Vector3(player4RB.velocity.x, CalculateJumpVerticalSpeed(), player4RB.velocity.z);
        }

        // Crouching
        if (Input.GetKeyDown(KeyCode.DownArrow)) 
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
        player4RB.AddForce(new Vector3(0, -gravity * player4RB.mass, 0));
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
             SC_GroundGenerator.instance.gameOverPlayer4 = true;
        }
    }
}