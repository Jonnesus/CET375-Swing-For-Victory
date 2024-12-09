using UnityEngine;

public class TempKeyboardMovement : MonoBehaviour
{
    public float walkSpeed = 8f;
    public float maxVelChange = 10f;

    private Vector2 input;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();
    }

    private void FixedUpdate()
    {
        if (input.magnitude > 0.5f)
            rb.AddForce(CalculateMovement(walkSpeed), ForceMode.VelocityChange);
        else
        {
            var velocity1 = rb.linearVelocity;
            velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
            rb.linearVelocity = velocity1;
        }
    }

    Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity= new Vector3(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= _speed;
        Vector3 velocity = rb.linearVelocity;

        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;

            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelChange, maxVelChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelChange, maxVelChange);
            velocityChange.y = 0;

            return velocityChange;
        }
        else
            return new Vector3 (0, 0, 0);
    }
}