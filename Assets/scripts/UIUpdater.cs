using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMonedas; 
    [SerializeField] private TextMeshProUGUI textoVidas; 

    private void Start()
    {
        
        GameManagerClass.instancia.OnMonedasChanged += ActualizarMonedas; 
        GameManagerClass.instancia.OnVidasChanged += ActualizarVidas; 
        
        // Valores iniciales
        ActualizarMonedas(0);
        ActualizarVidas(3);
    }

    private void ActualizarMonedas(int cantidad)
    {
        textoMonedas.text = "Monedas: " + cantidad; 
    }

    private void ActualizarVidas(int cantidad)
    {
        textoVidas.text = "Vidas: " + cantidad; 
    }

    private void OnDestroy()
    {
        // Es buena práctica desuscribirse para evitar errores
        if (GameManagerClass.instancia != null)
        {
            GameManagerClass.instancia.OnMonedasChanged -= ActualizarMonedas;
            GameManagerClass.instancia.OnVidasChanged -= ActualizarVidas;
        }
    }
}