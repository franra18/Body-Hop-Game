using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerHUD : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterSwitcher switcher;

    [Header("Configuración de Tiempo")]
    public float minutosIniciales = 20f;
    private float tiempoRestante;
    private float tiempoTotalSegundos;

    [Header("Interfaz HUD")]
    public Image barraDeTiempo;
    public TextMeshProUGUI textoTiempo;

    [Header("Colores por Personaje")]
    public Color colorPersonaje1 = Color.cyan;
    public Color colorPersonaje2 = Color.magenta;

    void Start()
    {
        tiempoTotalSegundos = minutosIniciales * 60f;
        tiempoRestante = tiempoTotalSegundos;
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

        if (switcher != null)
        {
            Color colorActual = switcher.isCharacter1Active ? colorPersonaje1 : colorPersonaje2;
            barraDeTiempo.color = colorActual;
            textoTiempo.color = colorActual;
        }

        int minutos = Mathf.FloorToInt(tiempoRestante / 60f);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60f);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}