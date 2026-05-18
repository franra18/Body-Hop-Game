using UnityEngine;
using UnityEngine.InputSystem;

public class AN_Button : MonoBehaviour
{
    [Header("Restricción de Personaje")]
    public string tagPermitido = "Boy1";
    public InputAction interactAction = new InputAction("Interact", binding: "<Keyboard>/e", expectedControlType: "Button");

    [Header("Configuración Original")]
    public bool isLever = false;
    public bool Locked = false;

    [Header("Nuevo: Objeto a Alternar")]
    [Tooltip("Arrastra aquí el objeto que quieres que aparezca o desaparezca")]
    public GameObject objetoObjetivo;

    [Header("Exclusividad (Opcional)")]
    [Tooltip("Arrastra aquí el objeto de la OTRA palanca para desactivarla automáticamente")]
    public AN_Button palancaExcluyente;

    private Animator anim;
    private bool personajeEnRango = false;
    public bool estadoActivo; 

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    void Start()
    {
        anim = GetComponent<Animator>();
        if (objetoObjetivo != null)
        {
            estadoActivo = objetoObjetivo.activeSelf;
            if (isLever) anim.SetBool("LeverUp", estadoActivo);
        }
    }

    void Update()
    {
        if (Locked) return;

        if (objetoObjetivo != null && personajeEnRango)
        {
            if (interactAction.WasPressedThisFrame())
            {
                estadoActivo = !estadoActivo;
                objetoObjetivo.SetActive(estadoActivo);
                
                // Si esta palanca se acaba de encender, apaga la otra
                if (estadoActivo && palancaExcluyente != null)
                {
                    palancaExcluyente.ApagarForzosamente();
                }
                
                if (isLever) anim.SetBool("LeverUp", estadoActivo);
                else anim.SetTrigger("ButtonPress"); 
            }
        }
    }

    public void ApagarForzosamente()
    {
        if (estadoActivo)
        {
            estadoActivo = false;
            if (objetoObjetivo != null) objetoObjetivo.SetActive(false);
            if (isLever && anim != null) anim.SetBool("LeverUp", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagPermitido)) personajeEnRango = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagPermitido)) personajeEnRango = false;
    }
}