using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            GameManagerClass.instancia.LoseLife(); 
            
            
            other.GetComponentInParent<PlayerControler>().Respawn();
        }
    }
}