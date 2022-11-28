using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float forwardAcceleration;
    [SerializeField] private float backwardAcceleration;
    [SerializeField] private float strafeAcceleration;
    [SerializeField] private float gravityAcceleration;
    [SerializeField] private float jumpAcceleration;
    [SerializeField] private float maxForwardVelocity;
    [SerializeField] private float maxBackwardVelocity;
    [SerializeField] private float maxStrafeVelocity;
    [SerializeField] private float maxFallVelocity;
    [SerializeField] private float rotationVelocityFactor = 1f;
    [SerializeField] private float maxHeadUpAngle;
    [SerializeField] private float minHeadDownAngle;

    private CharacterController characterController;
    private Transform head;
    private Vector3 velocity;
    private Vector3 acceleration;
    private bool startJump;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        head = GetComponentInChildren<Camera>().transform;

        velocity = Vector3.zero;
        acceleration = Vector3.zero;
        startJump = false;

        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CheckForJump();
        UpdateRotation();
        UpdateHead();
    }

    private void UpdateRotation()
    {
        float rotation = Input.GetAxis("Mouse X");
        transform.Rotate(new Vector3(0, rotation * rotationVelocityFactor, 0));
    }

    private void UpdateHead()
    {
        Vector3 headRotation = head.localEulerAngles;
        headRotation.x -= Input.GetAxis("Mouse Y") * rotationVelocityFactor;

        if (headRotation.x < 180f)
            headRotation.x = Mathf.Min(headRotation.x, maxHeadUpAngle);
        else
            headRotation.x = Mathf.Max(headRotation.x, minHeadDownAngle);

        head.localEulerAngles = headRotation;
    }

    private void CheckForJump()
    {
        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
            startJump = true;
    }

    private void FixedUpdate()
    {
        UpdateAcceleration();
        UpdateVelocity();
        UpdatePosition();
    }

    private void UpdateAcceleration()
    {
        UpdateForwardAcceleration();
        UpdateStrafeAcceleration();
        UpdateVerticalAcceleration();
    }

    private void UpdateForwardAcceleration()
    {
        float forwardAxis = Input.GetAxis("Forward");

        if (forwardAxis > 0)
        {
            acceleration.z = forwardAcceleration;
        }
        else if (forwardAxis < 0)
        {
            acceleration.z = backwardAcceleration;
        }
        else
        {
            acceleration.z = 0;
        }
    }

    private void UpdateStrafeAcceleration()
    {
        float strafeAxis = Input.GetAxis("Strafe");

        if (strafeAxis > 0)
        {
            acceleration.x = strafeAcceleration;
        }
        else if (strafeAxis < 0)
        {
            acceleration.x = -strafeAcceleration;
        }
        else
        {
            acceleration.x = 0;
        }
    }

    private void UpdateVerticalAcceleration()
    {
        if (startJump)
        {
            acceleration.y = jumpAcceleration;
        }
        else
            acceleration.y = gravityAcceleration;
    }

    private void UpdateVelocity()
    {
        velocity += acceleration * Time.fixedDeltaTime;

        if (acceleration.z == 0 || acceleration.z * velocity.z < 0)
            velocity.z = 0;
        else
            velocity.z = Mathf.Clamp(velocity.z, maxBackwardVelocity, maxForwardVelocity);
        
        if (acceleration.x == 0 || acceleration.x * velocity.x < 0)
            velocity.x = 0;
        else
            velocity.x = Mathf.Clamp(velocity.x, -maxStrafeVelocity, maxStrafeVelocity);

        if (characterController.isGrounded && !startJump)
            velocity.y = -0.1f;
        else
            velocity.y = Mathf.Max(velocity.y, maxFallVelocity);

        startJump = false;
    }

    private void UpdatePosition()
    {
        Vector3 motion = velocity * Time.fixedDeltaTime;
        motion = transform.TransformVector(motion);
        characterController.Move(motion);
    }
}
