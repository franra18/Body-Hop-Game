using UnityEngine;
using UnityEngine.InputSystem;

public class AN_Button : MonoBehaviour
{
    public enum ModoFuncionamiento { Normal, Excluyente, Cooperativo }

    [Header("Restricción de Personaje")]
    [Tooltip("Escribe aquí el tag del ÚNICO personaje que puede usar esta palanca/botón")]
    public string tagPermitido = "Player1";
    public InputAction interactAction = new InputAction("Interact", binding: "<Keyboard>/e", expectedControlType: "Button");

    [Header("Control de Personaje")]
    public CharacterSwitcher switcher;

    [Header("Configuración Original")]
    public bool isLever = false;
    public bool Locked = false;

    [Header("Comportamiento")]
    public ModoFuncionamiento modo = ModoFuncionamiento.Normal;
    public GameObject objetoObjetivo;
    public AN_Button palancaSocia;

    [Header("Desactivación Extra")]
    public GameObject objetoADesactivar;

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
        
        if (modo != ModoFuncionamiento.Cooperativo && objetoObjetivo != null)
        {
            estadoActivo = objetoObjetivo.activeSelf;
            if (isLever) anim.SetBool("LeverUp", estadoActivo);
        }

        if (mensajeInteraccionUI != null) mensajeInteraccionUI.SetActive(false);
    }

    void Update()
    {
        if (Locked) return;

        bool puedeInteractuar = false;
        
        // Verificamos si el personaje con el tag permitido está dentro Y si es el que estamos controlando
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
                if (objetoADesactivar != null && objetoADesactivar.activeSelf)
                {
                    objetoADesactivar.SetActive(false);
                }

                estadoActivo = !estadoActivo;
                if (isLever) anim.SetBool("LeverUp", estadoActivo);
                else anim.SetTrigger("ButtonPress");

                EjecutarLogica();
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

    public void EjecutarLogica()
    {
        switch (modo)
        {
            case ModoFuncionamiento.Normal:
                if (objetoObjetivo != null) objetoObjetivo.SetActive(estadoActivo);
                break;

            case ModoFuncionamiento.Excluyente:
                if (objetoObjetivo != null) objetoObjetivo.SetActive(estadoActivo);
                if (estadoActivo && palancaSocia != null)
                {
                    palancaSocia.ApagarForzosamente();
                }
                break;

            case ModoFuncionamiento.Cooperativo:
                ActualizarObjetoCooperativo();
                if (palancaSocia != null)
                {
                    palancaSocia.ActualizarObjetoCooperativo();
                }
                break;
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

    public void ActualizarObjetoCooperativo()
    {
        if (modo == ModoFuncionamiento.Cooperativo && objetoObjetivo != null)
        {
            bool sociaActiva = (palancaSocia != null) ? palancaSocia.estadoActivo : false;
            objetoObjetivo.SetActive(this.estadoActivo && sociaActiva);
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