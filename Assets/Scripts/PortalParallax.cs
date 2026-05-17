using UnityEngine;

public class PortalParallax : MonoBehaviour
{
    [Header("Referencias")]
    public Transform camaraPrincipal;
    private Renderer portalRenderer;

    [Header("Ajustes de Profundidad")]
    [Tooltip("Sensibilidad para el movimiento horizontal (X).")]
    public float sensibilidadX = 0.05f; 
    [Tooltip("Sensibilidad para el movimiento vertical (Y). Solo reaccionará a la altura real.")]
    public float sensibilidadY = 0.05f;

    private float alturaInicialCamara;

    void Start()
    {
        portalRenderer = GetComponent<Renderer>();
        
        if (camaraPrincipal == null && Camera.main != null)
        {
            camaraPrincipal = Camera.main.transform;
        }

        // Guardamos la altura inicial de la cámara para usarla como punto de referencia (cero)
        if (camaraPrincipal != null)
        {
            alturaInicialCamara = camaraPrincipal.position.y;
        }
    }

    void LateUpdate()
    {
        if (camaraPrincipal == null || portalRenderer == null) return;

        // 1. Desplazamiento Horizontal (X): Sigue siendo relativo al portal para cuando camines de lado
        Vector3 posicionRelativa = transform.InverseTransformPoint(camaraPrincipal.position);
        float offsetX = -posicionRelativa.x * sensibilidadX;

        // 2. Desplazamiento Vertical (Y): Calculamos la diferencia de altura REAL en el mundo global
        // Restamos la altura actual menos la inicial. Si el jugador avanza al frente en terreno plano, esto da 0.
        float diferenciaAltura = camaraPrincipal.position.y - alturaInicialCamara;
        float offsetY = -diferenciaAltura * sensibilidadY;

        // Aplicamos ambos offsets al material
        portalRenderer.material.SetTextureOffset("_BaseMap", new Vector2(offsetX, offsetY));
        
        // Si usas Emission, descomenta la línea de abajo:
        // portalRenderer.material.SetTextureOffset("_EmissionMap", new Vector2(offsetX, offsetY));
    }
}