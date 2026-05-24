using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DynamicMenuManager : MonoBehaviour
{
    public enum MenuState { Oculto, Inicio, Pausa, GameOver, Victoria }

    [Header("Panel Principal")]
    public GameObject panelFondo;

    [Header("Elementos Dinámicos")]
    public TextMeshProUGUI textoTitulo;

    [Tooltip("El botón que solo se usa para Continuar")]
    public Button botonContinuar;
    public TextMeshProUGUI textoBotonContinuar;

    [Tooltip("El botón que cambia entre Comenzar y Reiniciar")]
    public Button botonAccion;
    public TextMeshProUGUI textoBotonAccion;

    [Tooltip("El botón para cerrar la aplicación")]
    public Button botonSalir;

    public Slider sliderVolumen;

    [Header("Controles")]
    public InputAction pauseAction = new InputAction("Pause", binding: "<Keyboard>/escape");

    private MenuState estadoActual = MenuState.Oculto;

    private static bool juegoIniciado = false;

    private void OnEnable() => pauseAction.Enable();
    private void OnDisable() => pauseAction.Disable();

    void Start()
    {
        float volumenGuardado = PlayerPrefs.GetFloat("VolumenJuego", 1f);

        AudioListener.volume = volumenGuardado;
        sliderVolumen.minValue = 0f;
        sliderVolumen.maxValue = 1f;
        sliderVolumen.value = volumenGuardado;

        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);

        if (!juegoIniciado)
        {
            juegoIniciado = true;
            MostrarMenu(MenuState.Inicio);
        }
        else
        {
            ReanudarJuego();
        }
    }

    public void CambiarVolumen(float valor)
    {
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("VolumenJuego", valor);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (estadoActual == MenuState.Oculto)
            {
                MostrarMenu(MenuState.Pausa);
            }
            else if (estadoActual == MenuState.Pausa)
            {
                ReanudarJuego();
            }
        }
    }

    public void MostrarMenu(MenuState nuevoEstado)
    {
        estadoActual = nuevoEstado;
        panelFondo.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        botonContinuar.onClick.RemoveAllListeners();
        botonAccion.onClick.RemoveAllListeners();
        botonSalir.onClick.RemoveAllListeners();

        // Configurar el botón de salir para que funcione en cualquier menú visible
        botonSalir.gameObject.SetActive(true);
        botonSalir.onClick.AddListener(SalirDelJuego);

        switch (nuevoEstado)
        {
            case MenuState.Inicio:
                textoTitulo.text = "BODY HOP";

                botonContinuar.gameObject.SetActive(false); 

                textoBotonAccion.text = "Comenzar partida";
                botonAccion.onClick.AddListener(ReanudarJuego);
                break;

            case MenuState.Pausa:
                textoTitulo.text = "JUEGO EN PAUSA";

                botonContinuar.gameObject.SetActive(true);
                textoBotonContinuar.text = "Continuar";
                botonContinuar.onClick.AddListener(ReanudarJuego);

                textoBotonAccion.text = "Reiniciar partida";
                botonAccion.onClick.AddListener(ReiniciarJuegoDesdeCero);
                break;

            case MenuState.GameOver:
                textoTitulo.text = "GAME OVER!";

                botonContinuar.gameObject.SetActive(false);

                textoBotonAccion.text = "Volver a intentarlo";
                botonAccion.onClick.AddListener(ReiniciarEscena);
                break;

            case MenuState.Victoria:
                textoTitulo.text = "ENHORABUENA!\nHAS SALVADO A TU HERMANO";

                botonContinuar.gameObject.SetActive(false);

                textoBotonAccion.text = "Empezar de nuevo";
                botonAccion.onClick.AddListener(ReiniciarJuegoDesdeCero);
                break;
        }
    }

    public void ReanudarJuego()
    {
        estadoActual = MenuState.Oculto;
        panelFondo.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ReiniciarEscena()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ReiniciarJuegoDesdeCero()
    {
        Time.timeScale = 1f;
        juegoIniciado = false; 
        
        GameData.tiempoGuardado = -1f;
        GameData.personaje1Activo = true;

        SceneManager.LoadScene(0); 
    }

    private void SalirDelJuego()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}