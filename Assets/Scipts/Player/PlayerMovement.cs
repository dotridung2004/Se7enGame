using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 5f;
    private Vector3 direction;
    private float rotationSpeed = 720f;
    private Animator anim;
    public float idleBlend = 0f;
    public float runBlend = 0.6f;

    private static readonly int Blend = Animator.StringToHash("Blend");

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float target;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        direction = new Vector3(x, 0, z).normalized;
        bool isRunning = direction.sqrMagnitude > 0.01f;
        if (isRunning)
        {
            target = runBlend;
        }
        else
        {
            target = idleBlend;
        }
        anim.SetFloat(Blend, target, 0.1f, Time.deltaTime);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = direction * speed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;
        if(direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
