using UnityEngine;

public class SubirObjetoTrigger : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject objetoAMover;
    public float velocidadSubida = 2f;

    private bool activado = false;

    private void Update()
    {
        if (activado && objetoAMover != null)
        {
            objetoAMover.transform.Translate(Vector3.up * velocidadSubida * Time.deltaTime, Space.World);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!activado && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            if (objetoAMover != null)
            {
                activado = true;
                Destroy(objetoAMover, 5f);
            }
        }
    }
}