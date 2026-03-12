using UnityEngine;
using UnityEngine.SceneManagement;
using System; 

public class GameManagerClass : MonoBehaviour
{   
    static public GameManagerClass instancia;
    
   
    private int monedas = 0;
    [SerializeField] private int vidas = 3; 
    
    public event Action<int> OnMonedasChanged;
    public event Action<int> OnVidasChanged;

    private void Awake() 
    {
        if(instancia == null)
        {
            instancia = this;   
        }
        else
        {
            Destroy(gameObject); 
        }
    }



    public void AddMoneda()
    {
        monedas++;
        Debug.Log("Monedas: " + monedas);
      
        OnMonedasChanged?.Invoke(monedas); 
    }

    public void LoseLife()
    {
        vidas--;
        Debug.Log("Vidas restantes: " + vidas);
       
        OnVidasChanged?.Invoke(vidas);

        if (vidas <= 0)
        {
           
            CargarMenuPrincipal(); 
        }
    }

    

    public void CargarNivel()
    {
        SceneManager.LoadScene("nivel1"); 
    }

    public void CargarMenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CargarVictoria()
    {
        SceneManager.LoadScene("Victory");
    }
}