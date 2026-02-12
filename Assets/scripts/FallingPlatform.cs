using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float tiempoEspera = 1.0f; // Tiempo antes de caer 
    [SerializeField] private float velocidadCaida = 5.0f; // Velocidad de descenso [cite: 86]
    [SerializeField] private float alturaMinima = -10.0f; // Altura donde se detiene [cite: 87]
    [SerializeField] private float tiempoAbajo = 2.0f; // Tiempo que espera antes de subir [cite: 89]

    private Vector3 posicionInicial; // Guarda la posición original [cite: 90]
    private float tiempoActivacion = -1f; // Para calcular el paso del tiempo con Time.time 
    
    // Estados de la plataforma
    private bool estaCayendo = false;
    private bool estaRegresando = false;
    private bool activada = false;

    void Start()
    {
        // Guardamos la posición inicial al arrancar [cite: 68]
        posicionInicial = transform.position;
    }

    void Update()
    {
        // 1. Lógica de la cuenta atrás [cite: 83, 85]
        if (activada && !estaCayendo && !estaRegresando)
        {
            if (Time.time >= tiempoActivacion + tiempoEspera)
            {
                estaCayendo = true;
            }
        }

        // 2. Lógica de caída constante [cite: 86]
        if (estaCayendo)
        {
            transform.position += Vector3.down * velocidadCaida * Time.deltaTime;

            // Comprobar si ha llegado al límite [cite: 87]
            if (transform.position.y <= alturaMinima)
            {
                estaCayendo = false;
                StartCoroutine(EsperarYRegresar()); // [cite: 89]
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    // Detectar si el jugador pisa la plataforma 
        if (collision.gameObject.CompareTag("Player") && !activada && !estaRegresando)
        {
            // Opcional: Solo activar si el jugador toca desde arriba 
            if (collision.contacts[0].normal.y < -0.5f)
            {
                activada = true;
                tiempoActivacion = Time.time; // Registra el momento del contacto 
            }
        }
    }

    IEnumerator EsperarYRegresar()
    {
        yield return new WaitForSeconds(tiempoAbajo); // Esperar un breve tiempo [cite: 89]
        
        estaRegresando = true;

        // Volver a la posición inicial suavemente [cite: 90]
        while (Vector3.Distance(transform.position, posicionInicial) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, posicionInicial, velocidadCaida * Time.deltaTime);
            yield return null;
        }

        // Resetear todo para que vuelva a funcionar [cite: 91]
        transform.position = posicionInicial;
        activada = false;
        estaRegresando = false;
    }
}