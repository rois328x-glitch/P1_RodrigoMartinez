using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private float topMargin = 0.15f; 

    [Header("Timing")]
    [SerializeField] private float delayBeforeFall = 0.75f;
    [SerializeField] private float waitAtBottom = 0.5f;

    [Header("Movement")]
    [SerializeField] private float fallSpeed = 2.0f;
    [SerializeField] private float minY = 0.0f;

    private Vector3 posicionInicial;
    private Rigidbody rb;

    private bool activada = false;
    private bool cayendo = false;
    private bool esperandoReset = false;

    private float tiempoActivacion = 0f;
    private float tiempoEnFondo = 0f;

    private Collider myCol;

    private void Start()
    {
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();

        // Evita caída “sola” por físicas
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    private void FixedUpdate()
    {
        if (activada && !cayendo && !esperandoReset)
        {
            if (Time.time >= tiempoActivacion + delayBeforeFall)
                cayendo = true;
        }

        if (cayendo)
        {
            Vector3 posActual = rb.position;
            Vector3 nuevaPos = posActual + Vector3.down * fallSpeed * Time.fixedDeltaTime;

            if (nuevaPos.y <= minY)
            {
                nuevaPos = new Vector3(nuevaPos.x, minY, nuevaPos.z);
                rb.MovePosition(nuevaPos);

                cayendo = false;
                esperandoReset = true;
                tiempoEnFondo = Time.time;
                return;
            }

            rb.MovePosition(nuevaPos);
        }

        if (esperandoReset)
        {
            if (Time.time >= tiempoEnFondo + waitAtBottom)
                ResetearPlataforma();
        }
    }

    private void ResetearPlataforma()
    {
        rb.MovePosition(posicionInicial);
        activada = false;
        cayendo = false;
        esperandoReset = false;
        tiempoActivacion = 0f;
        tiempoEnFondo = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (activada) return;
        if (!collision.gameObject.CompareTag(playerTag)) return;

        
        float platformTopY = myCol.bounds.max.y;
        float playerBottomY = collision.collider.bounds.min.y;

        bool playerOnTop = playerBottomY >= (platformTopY - topMargin);
        if (!playerOnTop) return;

        activada = true;
        tiempoActivacion = Time.time;
    }
}