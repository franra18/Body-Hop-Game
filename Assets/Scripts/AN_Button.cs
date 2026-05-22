using UnityEngine;
using UnityEngine.InputSystem;

public class AN_Button : MonoBehaviour
{
    public enum ModoFuncionamiento { Normal, Excluyente, Cooperativo }

    [Header("Restricción de Personaje")]
    public string tagPermitido = "Boy1";
    public InputAction interactAction = new InputAction("Interact", binding: "<Keyboard>/e", expectedControlType: "Button");

    [Header("Configuración Original")]
    public bool isLever = false;
    public bool Locked = false;

    [Header("Comportamiento")]
    [Tooltip("Normal: Funciona sola. Excluyente: Apaga la otra al encenderse. Cooperativo: Ambas deben estar encendidas.")]
    public ModoFuncionamiento modo = ModoFuncionamiento.Normal;

    [Tooltip("El objeto que aparecerá/desaparecerá")]
    public GameObject objetoObjetivo;

    [Tooltip("Arrastra aquí la otra palanca/botón si usas el modo Excluyente o Cooperativo")]
    public AN_Button palancaSocia;

    [Header("Desactivación Extra")]
    [Tooltip("Objeto opcional. Si está activo al pulsar la palanca/botón, se desactivará.")]
    public GameObject objetoADesactivar;

    [HideInInspector]
    public bool estadoActivo = false;
    private Animator anim;
    private bool personajeEnRango = false;

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
    }

    void Update()
    {
        if (Locked) return;

        if (personajeEnRango)
        {
            if (interactAction.WasPressedThisFrame())
            {
                Debug.Log("Interacción pulsada correctamente por el personaje en rango.");
                
                if (objetoADesactivar != null && objetoADesactivar.activeSelf)
                {
                    objetoADesactivar.SetActive(false);
                    Debug.Log("Objeto extra desactivado: " + objetoADesactivar.name);
                }

                estadoActivo = !estadoActivo;
                
                Debug.Log("Ejecutando animación. ¿Es palanca?: " + isLever + " | Nuevo estado: " + estadoActivo);
                if (isLever) anim.SetBool("LeverUp", estadoActivo);
                else anim.SetTrigger("ButtonPress");

                EjecutarLogica();
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
        Debug.Log("Objeto ha entrado en el Trigger: " + other.gameObject.name + " | Tag: " + other.tag);
        
        if (other.CompareTag(tagPermitido))
        {
            Debug.Log("¡Objeto VÁLIDO en rango de interacción!");
            personajeEnRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagPermitido))
        {
            Debug.Log("Objeto válido ha SALIDO del rango de interacción.");
            personajeEnRango = false;
        }
    }
}