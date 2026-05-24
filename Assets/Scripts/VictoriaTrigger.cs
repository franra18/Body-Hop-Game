using UnityEngine;

public class VictoriaTrigger : MonoBehaviour
{
    private bool juegoTerminado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!juegoTerminado && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            juegoTerminado = true;
            
            DynamicMenuManager menuManager = FindAnyObjectByType<DynamicMenuManager>();
            if (menuManager != null)
            {
                menuManager.MostrarMenu(DynamicMenuManager.MenuState.Victoria);
            }
        }
    }
}