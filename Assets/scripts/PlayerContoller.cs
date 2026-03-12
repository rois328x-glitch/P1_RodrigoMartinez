using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintMultiplier = 2.0f;
    [SerializeField] private Transform cameraTransform;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 5.0f;
    [SerializeField] private bool enableDoubleJump = false;

    private Rigidbody rb;
    private Animator anim;
    private float currentSpeed; // Variable para manejar la velocidad actual

    // Estado
    private bool isGrounded = false;
    private bool hasDoubleJumped = false;
    private Vector3 movement;

  

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = walkSpeed * sprintMultiplier; // Aumenta la velocidad temporalmente 
        }
        else
        {
            currentSpeed = walkSpeed; // Vuelve a la velocidad normal
        }
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        

       
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f; 
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();
        Vector3 movement = (camForward * vertical + camRight * horizontal).normalized;
        transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
        // Salto
        if (anim != null)
        {
            
            float speedValue = movement.magnitude * (currentSpeed / walkSpeed);
            anim.SetFloat("speed", speedValue);
            anim.SetBool("isGrounded", isGrounded);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
                hasDoubleJumped = false; // al saltar desde suelo, reseteamos doble salto
            }
            else if (enableDoubleJump && !hasDoubleJumped)
            {
                Jump();
                hasDoubleJumped = true; // consumimos el doble salto
            }
        }
    }

    private void Jump()
    {
       
        Vector3 v = rb.linearVelocity; 
        v.y = 0f;
        rb.linearVelocity = v;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasDoubleJumped = false; // al tocar suelo, recuperas doble salto
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
      
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
  
private Vector3 posicionRespawn;

private void Start()
{
     rb = GetComponent<Rigidbody>();
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        currentSpeed = walkSpeed;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    posicionRespawn = transform.position; 
    
    rb = GetComponent<Rigidbody>();
    if (cameraTransform == null && Camera.main != null)
    {
        cameraTransform = Camera.main.transform;
    }
    currentSpeed = walkSpeed;
    rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
}


public void Respawn() 
{
    transform.position = posicionRespawn; 
    
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero; 
        rb.angularVelocity = Vector3.zero; 
    }
}
}