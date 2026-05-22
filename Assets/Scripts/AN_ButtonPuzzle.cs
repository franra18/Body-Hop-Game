using UnityEngine;
using UnityEngine.InputSystem;

public class AN_ButtonPuzzle : MonoBehaviour
{
    [Header("Tags de Personajes")]
    public string tagPersonaje1 = "Player1";
    public string tagPersonaje2 = "Player2";
    public InputAction interactAction = new InputAction("Interact", binding: "<Keyboard>/e", expectedControlType: "Button");

    [Header("Control de Personaje")]
    public CharacterSwitcher switcher;

    [Header("Configuración Original")]
    public bool isLever = true;
    public bool Locked = false;

    [Header("Conexión con el Puzzle")]
    public PuzzleManager manager;

    [Header("Interfaz de Interacción")]
    public GameObject mensajeInteraccionUI;

    [HideInInspector]
    public bool estadoActivo = false;
    private Animator anim;
    
    private bool char1Dentro = false;
    private bool char2Dentro = false;
    private static GameObject focoUI;

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    void Start()
    {
        anim = GetComponent<Animator>();
        if (mensajeInteraccionUI != null) mensajeInteraccionUI.SetActive(false);
    }

    void Update()
    {
        if (Locked) return;

        bool puedeInteractuar = false;
        if (switcher != null)
        {
            if (char1Dentro && switcher.isCharacter1Active) puedeInteractuar = true;
            if (char2Dentro && !switcher.isCharacter1Active) puedeInteractuar = true;
        }

        if (puedeInteractuar)
        {
            focoUI = this.gameObject;
            if (mensajeInteraccionUI != null) mensajeInteraccionUI.SetActive(true);

            if (interactAction.WasPressedThisFrame())
            {
                estadoActivo = !estadoActivo;
                if (isLever) anim.SetBool("LeverUp", estadoActivo);
                else anim.SetTrigger("ButtonPress");

                if (manager != null) manager.EvaluarCombinaciones();
            }
        }
        else
        {
            // Solo apagamos la UI si fuimos nosotros quienes la encendimos
            if (focoUI == this.gameObject)
            {
                if (mensajeInteraccionUI != null) mensajeInteraccionUI.SetActive(false);
                focoUI = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPersonaje1)) char1Dentro = true;
        if (other.CompareTag(tagPersonaje2)) char2Dentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagPersonaje1)) char1Dentro = false;
        if (other.CompareTag(tagPersonaje2)) char2Dentro = false;
    }
}