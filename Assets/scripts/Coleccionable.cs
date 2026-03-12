using Unity.VisualScripting;
using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    private bool fueRecogido = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerControler jugador = other.GetComponentInParent<PlayerControler    >();
        if (jugador != null)
        {
            fueRecogido = true;
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (fueRecogido && GameManagerClass.instancia != null)
        GameManagerClass.instancia.AddMoneda();
    }
}
