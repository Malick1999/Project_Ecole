using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilderController : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public float mouseSensitivity = 100.0f;

    [SerializeField] private Camera cam;
    [SerializeField] private Transform arm;
    [SerializeField] private Transform checkGroundPos;
    [SerializeField] private float groundRayDist;
    [SerializeField] private LayerMask groundLayer;

    float horizontal, vertical;
    float mouseX, mouseY;
    float xRotation, yRotation;
    Vector3 newVelocity;
    Vector3 moveDirection;
    Rigidbody rb;
    bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
        CheckGround();
    }

    private void FixedUpdate()
    {
        Moove();
    }

    void CheckInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Space))
            rb.position += Vector3.up * speed * Time.deltaTime;

        else if (Input.GetKey(KeyCode.RightShift))
            rb.position += Vector3.down * speed * Time.deltaTime;

        else
        {
            newVelocity = rb.velocity;
            newVelocity.y = 0;
            rb.velocity = newVelocity;
        }

        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90f, 90.0f);

        transform.localRotation = Quaternion.Euler(0.0f, yRotation, 0.0f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        //arm.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
    }

    void Moove()
    {

        /*newVelocity = rb.velocity;
        newVelocity.x = horizontal * speed;
        rb.velocity = newVelocity;*/

        Vector3 cameraForward = cam.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        // Get the camera's right direction
        Vector3 cameraRight = cam.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Calculate the desired direction based on camera's forward and right directions
        moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        newVelocity = rb.velocity;
        newVelocity.x = moveDirection.x * speed;
        newVelocity.z = moveDirection.z * speed;
        rb.velocity = newVelocity;

        //rb.AddForce(moveDirection * speed, ForceMode.Force);
    }

    void CheckGround()
    {
        isGrounded = Physics.Raycast(checkGroundPos.position, Vector3.down, groundRayDist, groundLayer);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }
}
