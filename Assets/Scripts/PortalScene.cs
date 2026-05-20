using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalScene : MonoBehaviour
{
    [Header("Configuración")]
    public string tagPermitido1 = "Boy1";
    public string tagPermitido2 = "Boy2";
    public string nombreDeEscena;

    [Header("Referencias a Guardar")]
    public TimerHUD timer;
    public CharacterSwitcher switcher;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPermitido1) || other.CompareTag(tagPermitido2))
        {
            if (!string.IsNullOrEmpty(nombreDeEscena))
            {
                // 1. Guardar estado actual en la memoria global
                if (timer != null) GameData.tiempoGuardado = timer.tiempoRestante;
                if (switcher != null) GameData.personaje1Activo = switcher.isCharacter1Active;

                // 2. Cargar nueva escena
                SceneManager.LoadScene(nombreDeEscena);
            }
        }
    }
}