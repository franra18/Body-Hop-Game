using UnityEngine;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.InputSystem;

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

    [Header("Configuración (New Input System)")]
    public InputAction switchAction = new InputAction("Switch", binding: "<Keyboard>/tab", expectedControlType: "Button");
    
    [Header("Tiempos de Secuencia")]
    public float cameraTransitionTime = 1.5f; 
    public float animationTime = 1.5f;        

    private bool isCharacter1Active = true;
    private bool isSwitching = false;

    private void OnEnable() => switchAction.Enable();
    private void OnDisable() => switchAction.Disable();

    void Start()
    {
        SetAllPrioritiesLow();
        vCam1_1st.Priority = 10;
        
        ApplyInitialState(character1, true);
        ApplyInitialState(character2, false);
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

        GameObject currentPos = isCharacter1Active ? character1 : character2;
        GameObject targetPos = isCharacter1Active ? character2 : character1;
        
        CinemachineCamera current3rd = isCharacter1Active ? vCam1_3rd : vCam2_3rd;
        CinemachineCamera target3rd = isCharacter1Active ? vCam2_3rd : vCam1_3rd;
        CinemachineCamera target1st = isCharacter1Active ? vCam2_1st : vCam1_1st;

        KidsController currentCtrl = currentPos.GetComponent<KidsController>();
        Animator currentAnim = currentPos.GetComponent<Animator>();

        // 0. Quitamos el control pero el script sigue "encendido" para que caiga
        if(currentCtrl) currentCtrl.isControllable = false;
        if(currentAnim) currentAnim.SetFloat("Speed", 0);

        SetAllPrioritiesLow();
        current3rd.Priority = 10;
        
        yield return new WaitForSeconds(cameraTransitionTime);

        if(currentAnim) currentAnim.Play("down");

        yield return new WaitForSeconds(animationTime);

        SetAllPrioritiesLow();
        target3rd.Priority = 10;

        yield return new WaitForSeconds(cameraTransitionTime);

        Animator targetAnim = targetPos.GetComponent<Animator>();
        if(targetAnim) targetAnim.Play("standup_faint");

        yield return new WaitForSeconds(animationTime);

        SetAllPrioritiesLow();
        target1st.Priority = 10;
        
        KidsController targetCtrl = targetPos.GetComponent<KidsController>();
        if(targetCtrl) targetCtrl.isControllable = true;

        isCharacter1Active = !isCharacter1Active;
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
        // Cambiamos el estado de control inicial
        character.GetComponent<KidsController>().isControllable = isActive;
        if (!isActive) character.GetComponent<Animator>().Play("down");
    }
}