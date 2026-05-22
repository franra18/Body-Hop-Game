using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerHUD : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterSwitcher switcher;

    [Header("Configuración de Tiempo")]
    public float minutosIniciales = 20f;
    public float tiempoRestante;
    private float tiempoTotalSegundos;

    [Header("Interfaz HUD")]
    public Image barraDeTiempo;
    public TextMeshProUGUI textoTiempo;

    void Start()
    {
        tiempoTotalSegundos = minutosIniciales * 60f;

        // Si el tiempo es distinto de -1, significa que venimos de otra escena con el tiempo guardado
        if (GameData.tiempoGuardado != -1f)
        {
            tiempoRestante = GameData.tiempoGuardado;
        }
        else
        {
            tiempoRestante = tiempoTotalSegundos; // Inicio normal
        }

        // Configurar personaje activo guardado
        if (switcher != null)
        {
            switcher.isCharacter1Active = GameData.personaje1Activo;
            // Aquí deberías llamar a la función de tu switcher que activa/desactiva las mallas de los personajes
        }
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante < 0) tiempoRestante = 0;

            ActualizarUI();
        }
    }

    void ActualizarUI()
    {
        barraDeTiempo.fillAmount = tiempoRestante / tiempoTotalSegundos;

        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}