using UnityEngine;

public class MovingPlatformClase : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Vector3 posicionFinal;

    [SerializeField] private float velocidad = 2.0f;
    [SerializeField] private GameObject destino;

    private Rigidbody rb;

    // esta variable indica si la plataforma se estt� moviendo hacia la posici�n final
    private bool voyALaFinal = false; 

    private void Start()
    {
        posicionInicial =  transform.position;
        posicionFinal = destino.transform.position;

        destino.GetComponent<MeshRenderer>().enabled = false;

        rb = GetComponent<Rigidbody>();
    }

    // Utilizamos fixedUpdate cuando movemos/aplicamos fuerzas/aplicamos velociad a un RIGIDBODY
    private void FixedUpdate() 
    {
        Vector3 posActual = transform.position;

        Vector3 newPosition;

        if (voyALaFinal)
        {
            newPosition = Vector3.MoveTowards(
                posActual,
                posicionFinal,
                velocidad * Time.fixedDeltaTime
            );
        }
        else
        {
            newPosition = Vector3.MoveTowards(
                posActual,
                posicionInicial,
                velocidad * Time.fixedDeltaTime
            );
        }

        rb.MovePosition(newPosition);

        if (Vector3.Distance(transform.position, posicionFinal) < 0.01f)
        {
            voyALaFinal = false;
        }
        else if (Vector3.Distance(transform.position, posicionInicial) < 0.01f)
        {
            voyALaFinal = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
{
    // Comprobamos si lo que ha tocado la plataforma es el jugador [cite: 55]
    if (collision.gameObject.CompareTag("Player"))
    {
        // Hacemos que el jugador sea hijo de la plataforma
        collision.transform.SetParent(transform);
    }
}

private void OnCollisionExit(Collision collision)
{
    // Cuando el jugador salta o se baja, deja de ser hijo [cite: 44]
    if (collision.gameObject.CompareTag("Player"))
    {
        Debug.Log("¡Jugador detectado! Pegando..."); // Esto saldrá en la consola
        collision.transform.SetParent(transform);
        // Quitamos el parentesco (vuelve a ser independiente)
        collision.transform.SetParent(null);
    }
}
}
