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
    public float waitTimeAnimations = 1.5f; // Tiempo que tarda en caer/levantarse

    private bool isCharacter1Active = true;
    private bool isSwitching = false;

    private void OnEnable()
    {
        switchAction.Enable();
    }

    private void OnDisable()
    {
        switchAction.Disable();
    }

    void Start()
    {
        // Estado inicial: Personaje 1 en 1ª persona, Personaje 2 en el suelo
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

        // 1. Ir a vista de 3ª persona del personaje actual y caer
        SetAllPrioritiesLow();
        current3rd.Priority = 10;
        
        KidsController currentCtrl = currentPos.GetComponent<KidsController>();
        Animator currentAnim = currentPos.GetComponent<Animator>();
        
        if(currentCtrl) currentCtrl.enabled = false;
        if(currentAnim) {
            currentAnim.SetFloat("Speed", 0);
            currentAnim.Play("down");
        }

        yield return new WaitForSeconds(waitTimeAnimations);

        // 2. Ir a vista de 3ª persona del otro personaje y levantarse
        SetAllPrioritiesLow();
        target3rd.Priority = 10;
        
        Animator targetAnim = targetPos.GetComponent<Animator>();
        if(targetAnim) targetAnim.Play("standup_faint");

        yield return new WaitForSeconds(waitTimeAnimations);

        // 3. Finalmente, ir a la 1ª persona del nuevo personaje y darle el control
        SetAllPrioritiesLow();
        target1st.Priority = 10;
        
        KidsController targetCtrl = targetPos.GetComponent<KidsController>();
        if(targetCtrl) targetCtrl.enabled = true;

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
        character.GetComponent<KidsController>().enabled = isActive;
        if (!isActive) character.GetComponent<Animator>().Play("down");
    }
}