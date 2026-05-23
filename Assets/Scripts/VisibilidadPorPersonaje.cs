using UnityEngine;

public class VisibilidadPorPersonaje : MonoBehaviour
{
    [Header("Referencias")]
    public CharacterSwitcher switcher;

    [Header("Objeto a Mostrar/Ocultar")]
    [Tooltip("Arrastra aquí el objeto que quieres que aparezca y desaparezca (recomendable que sea un objeto hijo para que este script no se apague a sí mismo)")]
    public GameObject objetoVisual;

    [Header("Configuración")]
    [Tooltip("Activa esta casilla para que aparezca con el Personaje 1. Desactívala para el Personaje 2.")]
    public bool visibleParaPersonaje1 = true;

    void Start()
    {
        if (switcher == null) switcher = FindAnyObjectByType<CharacterSwitcher>();
    }

    void Update()
    {
        if (switcher == null || objetoVisual == null) return;

        bool deberiaVerse = visibleParaPersonaje1 ? switcher.isCharacter1Active : !switcher.isCharacter1Active;

        if (objetoVisual.activeSelf != deberiaVerse)
        {
            objetoVisual.SetActive(deberiaVerse);
        }
    }
}