using UnityEngine;
using UnityEngine.InputSystem; 

/*
Este script permite controlar una de prueba con los controles:
- Rotación con el ratón (sensibilidad ajustable)
- Movimiento horizontal con WASD (velocidad ajustable)
- Movimiento vertical con ESPACIO (subir) y CTRL (bajar)
- Escape para salir del modo bloqueo del cursor
*/

public class PruebaCamara : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidadMovimiento = 10f;
    public float sensibilidadRaton = 0.1f;

    private float rotacionX = 0f;
    private float rotacionY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- ROTACIÓN CON EL RATÓN ---
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        
        rotacionY += mouseDelta.x * sensibilidadRaton;
        rotacionX -= mouseDelta.y * sensibilidadRaton;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        transform.eulerAngles = new Vector3(rotacionX, rotacionY, 0);

        // --- MOVIMIENTO HORIZONTAL (WASD) ---
        Vector2 inputWASD = Vector2.zero;
        if (Keyboard.current.wKey.isPressed) inputWASD.y = 1;
        if (Keyboard.current.sKey.isPressed) inputWASD.y = -1;
        if (Keyboard.current.aKey.isPressed) inputWASD.x = -1;
        if (Keyboard.current.dKey.isPressed) inputWASD.x = 1;

        // Calculamos direcciones horizontales (ignorando la inclinación de la cámara)
        Vector3 forwardPlano = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        Vector3 rightPlano = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;

        Vector3 movimientoHorizontal = (forwardPlano * inputWASD.y + rightPlano * inputWASD.x);

        // --- MOVIMIENTO VERTICAL (ESPACIO / CTRL) ---
        float movimientoVertical = 0f;
        if (Keyboard.current.spaceKey.isPressed) movimientoVertical = 1f;      // Subir
        if (Keyboard.current.leftCtrlKey.isPressed) movimientoVertical = -1f; // Bajar

        Vector3 subidaBajada = Vector3.up * movimientoVertical;

        // --- APLICAR MOVIMIENTO FINAL ---
        transform.position += (movimientoHorizontal + subidaBajada) * velocidadMovimiento * Time.deltaTime;

        // Salir del modo bloqueo
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}