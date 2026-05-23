using UnityEngine;
using UnityEngine.InputSystem;

public class AN_ButtonPuzzle : MonoBehaviour
{
    [Header("Restricción de Personaje")]
    [Tooltip("Escribe aquí el tag del ÚNICO personaje que puede usar esta palanca/botón")]
    public string tagPermitido = "Player1";
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
    
    private bool personajeDentro = false;
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
        
        if (personajeDentro && switcher != null)
        {
            if (switcher.isCharacter1Active && switcher.character1.CompareTag(tagPermitido)) puedeInteractuar = true;
            if (!switcher.isCharacter1Active && switcher.character2.CompareTag(tagPermitido)) puedeInteractuar = true;
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
            if (focoUI == this.gameObject)
            {
                if (mensajeInteraccionUI != null) mensajeInteraccionUI.SetActive(false);
                focoUI = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPermitido)) personajeDentro = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagPermitido)) personajeDentro = false;
    }
}