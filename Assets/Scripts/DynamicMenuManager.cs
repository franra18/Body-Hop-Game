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

    public Slider sliderVolumen;

    [Header("Controles")]
    public InputAction pauseAction = new InputAction("Pause", binding: "<Keyboard>/escape");

    private MenuState estadoActual = MenuState.Oculto;

    private void OnEnable() => pauseAction.Enable();
    private void OnDisable() => pauseAction.Disable();

    void Start()
    {
        // 1. Cargar el volumen guardado (si no existe un guardado previo, por defecto será 1f)
        float volumenGuardado = PlayerPrefs.GetFloat("VolumenJuego", 1f);

        // 2. Aplicar el volumen al juego y actualizar la posición de la barra
        AudioListener.volume = volumenGuardado;
        sliderVolumen.minValue = 0f;
        sliderVolumen.maxValue = 1f;
        sliderVolumen.value = volumenGuardado;

        sliderVolumen.onValueChanged.AddListener(CambiarVolumen);

        MostrarMenu(MenuState.Inicio);
    }

    public void CambiarVolumen(float valor)
    {
        // 1. Cambiar el volumen maestro de todo el juego en tiempo real
        AudioListener.volume = valor;

        // 2. Guardar el nuevo valor en la memoria para mantenerlo al cambiar de escena
        PlayerPrefs.SetFloat("VolumenJuego", valor);
        PlayerPrefs.Save();
    }

    void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            // Si el menú está oculto, pausamos. Si ya estamos en pausa, reanudamos.
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

        // Pausar el tiempo del juego y mostrar el ratón
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Limpiar las acciones anteriores de los botones para que no se acumulen
        botonContinuar.onClick.RemoveAllListeners();
        botonAccion.onClick.RemoveAllListeners();

        switch (nuevoEstado)
        {
            case MenuState.Inicio:
                textoTitulo.text = "BODY HOP";

                botonContinuar.gameObject.SetActive(false); // Ocultamos el botón continuar

                textoBotonAccion.text = "Comenzar partida";
                botonAccion.onClick.AddListener(ReanudarJuego);
                break;

            case MenuState.Pausa:
                textoTitulo.text = "JUEGO EN PAUSA";

                botonContinuar.gameObject.SetActive(true);
                textoBotonContinuar.text = "Continuar";
                botonContinuar.onClick.AddListener(ReanudarJuego);

                textoBotonAccion.text = "Reiniciar partida";
                botonAccion.onClick.AddListener(ReiniciarEscena);
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

                textoBotonAccion.text = "Empezar partida nueva";
                botonAccion.onClick.AddListener(ReiniciarEscena);
                break;
        }
    }

    public void ReanudarJuego()
    {
        estadoActual = MenuState.Oculto;
        panelFondo.SetActive(false);

        // Restaurar el tiempo y ocultar el ratón
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void ReiniciarEscena()
    {
        // Restaura el tiempo antes de recargar, si no, la nueva escena cargará pausada
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}