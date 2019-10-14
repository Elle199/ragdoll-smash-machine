using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private float gravityScale = 2f;
    [SerializeField] private float lowGravityScale = 1.8f;
    [SerializeField] private float globalGravity = -9.82f;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private bool resetVelocityOnJump = true;

    private Rigidbody rb;
    private float inputAxisH = 0;
    private float inputAxisV = 0;
    private bool jumpRequest = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        ApplyGravity();

        Vector3 movement = new Vector3(inputAxisH, 0, inputAxisV) * movementSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        if (jumpRequest)
        {
            if(resetVelocityOnJump)
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequest = false;
        }
    }

    /// <summary>
    /// Reads input values from player
    /// </summary>
    private void GetMovementInput()
    {
        inputAxisH = Input.GetAxis("Horizontal");
        inputAxisV = Input.GetAxis("Vertical");
    }

    private void ApplyGravity()
    {
        //Space for more complex physics
        Vector3 gravity = new Vector3();

        gravity = Vector3.up * globalGravity * gravityScale;

        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    private void OnTriggerStay(Collider other)
    {

    }
}
