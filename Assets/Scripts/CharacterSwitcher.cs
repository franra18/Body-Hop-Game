using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterSwitcher : MonoBehaviour
{
    [Header("Personajes")]
    public GameObject character1;
    public GameObject character2;

    [Header("Cámaras Personaje 1")]
    public CinemachineCamera vCam1_1st;
    public CinemachineCamera vCam1_3rd;

    [Header("Cámaras Personaje 2")]
    public CinemachineCamera vCam2_1st;
    public CinemachineCamera vCam2_3rd;

    [Header("Interfaz HUD")]
    public Canvas canvasHUD; // <--- Variable para apagar todo el Canvas
    public Image fotoHUD;
    public Sprite fotoPersonaje1;
    public Sprite fotoPersonaje2;

    [Header("Configuración (New Input System)")]
    public InputAction switchAction = new InputAction("Switch", binding: "<Keyboard>/tab", expectedControlType: "Button");

    [Header("Tiempos de Secuencia")]
    public float cameraTransitionTime = 1.5f;
    public float animationTime = 1.5f;

    public bool isCharacter1Active = true;
    private bool isSwitching = false;

    private void OnEnable() => switchAction.Enable();
    private void OnDisable() => switchAction.Disable();

    void Start()
    {
        SetAllPrioritiesLow();

        isCharacter1Active = GameData.personaje1Activo;

        if (isCharacter1Active)
        {
            vCam1_1st.Priority = 10;

            ApplyInitialState(character1, true);
            ApplyInitialState(character2, false);

            if (fotoHUD != null) fotoHUD.sprite = fotoPersonaje1;
        }
        else
        {
            vCam2_1st.Priority = 10; // Sustituye por el nombre de la cámara del personaje 2

            ApplyInitialState(character1, false);
            ApplyInitialState(character2, true);

            if (fotoHUD != null) fotoHUD.sprite = fotoPersonaje2; // Sustituye por la variable de la foto 2
        }
    }

    void Update()
    {
        if (switchAction.WasPressedThisFrame() && !isSwitching)
        {
            StartCoroutine(SwitchSequence());
        }
    }

    IEnumerator SwitchSequence()
    {
        isSwitching = true;

        // Apaga el Canvas completo al instante
        if (canvasHUD != null) canvasHUD.enabled = false;

        GameObject currentPos = isCharacter1Active ? character1 : character2;
        GameObject targetPos = isCharacter1Active ? character2 : character1;

        CinemachineCamera current3rd = isCharacter1Active ? vCam1_3rd : vCam2_3rd;
        CinemachineCamera target3rd = isCharacter1Active ? vCam2_3rd : vCam1_3rd;
        CinemachineCamera target1st = isCharacter1Active ? vCam2_1st : vCam1_1st;

        KidsController currentCtrl = currentPos.GetComponent<KidsController>();
        Animator currentAnim = currentPos.GetComponent<Animator>();

        if (currentCtrl) currentCtrl.isControllable = false;
        if (currentAnim) currentAnim.SetFloat("Speed", 0);

        SetAllPrioritiesLow();
        current3rd.Priority = 10;

        yield return new WaitForSeconds(cameraTransitionTime);

        if (currentAnim) currentAnim.Play("down");

        yield return new WaitForSeconds(animationTime);

        SetAllPrioritiesLow();
        target3rd.Priority = 10;

        yield return new WaitForSeconds(cameraTransitionTime);

        Animator targetAnim = targetPos.GetComponent<Animator>();
        if (targetAnim) targetAnim.Play("standup_faint");

        yield return new WaitForSeconds(animationTime);

        SetAllPrioritiesLow();
        target1st.Priority = 10;

        KidsController targetCtrl = targetPos.GetComponent<KidsController>();
        if (targetCtrl) targetCtrl.isControllable = true;

        isCharacter1Active = !isCharacter1Active;

        if (fotoHUD != null) fotoHUD.sprite = isCharacter1Active ? fotoPersonaje1 : fotoPersonaje2;

        // Vuelve a encender el Canvas completo al terminar
        if (canvasHUD != null) canvasHUD.enabled = true;

        isSwitching = false;
    }

    void SetAllPrioritiesLow()
    {
        vCam1_1st.Priority = 0;
        vCam1_3rd.Priority = 0;
        vCam2_1st.Priority = 0;
        vCam2_3rd.Priority = 0;
    }

    void ApplyInitialState(GameObject character, bool isActive)
    {
        character.GetComponent<KidsController>().isControllable = isActive;
        if (!isActive) character.GetComponent<Animator>().Play("down");
    }
}