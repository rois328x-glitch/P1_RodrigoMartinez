using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float sensibilidad = 100f;
    [SerializeField] private Transform playerBody;

    private float rotacionX = 0f;

    void Start()
    {
        // Bloquea el ratón en el centro de la pantalla y lo oculta
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Capturar el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        // Rotación vertical (Cámara)
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); // Evita que la cámara dé la vuelta completa

        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);

        // Rotación horizontal (Cuerpo del Jugador)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}