// Opción A: Si el objeto se apaga por completo (SetActive(false))
// Usa OnDisable y PlayClipAtPoint para crear un audio temporal que no se corte al desaparecer el objeto.

using UnityEngine;

public class SonidoAlDesactivar : MonoBehaviour
{
    public AudioClip clipDesactivar;
    [Range(0f, 1f)] public float volumen = 1f;

    private void OnDisable()
    {
        // Se usa la posición de la cámara principal para evitar que el audio 3D se pierda por la distancia
        if (clipDesactivar != null && Camera.main != null)
        {
            AudioSource.PlayClipAtPoint(clipDesactivar, Camera.main.transform.position, volumen);
        }
    }
}