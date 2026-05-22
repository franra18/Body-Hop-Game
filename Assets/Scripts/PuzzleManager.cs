// 2. Script gestor del puzzle: PuzzleManager.cs
// Crea un GameObject vacío en tu escena, llámalo "PuzzleManager" y asígnale este script.
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Palancas del Puzzle")]
    public AN_ButtonPuzzle palanca1;
    public AN_ButtonPuzzle palanca2;
    public AN_ButtonPuzzle palanca3;

    [Header("Paredes / Puertas")]
    public GameObject pared1;
    public GameObject pared2;
    public GameObject pared3;

    void Start()
    {
        // Nos aseguramos de que al empezar, todas las paredes bloqueen el paso
        RestaurarParedes();
    }

    public void EvaluarCombinaciones()
    {
        // 1. Siempre activamos todas las paredes primero. 
        // Así, si se rompe una combinación correcta, la pared reaparece automáticamente.
        RestaurarParedes();

        // 2. Leemos el estado actual de las 3 palancas
        bool p1 = palanca1.estadoActivo;
        bool p2 = palanca2.estadoActivo;
        bool p3 = palanca3.estadoActivo;

        // 3. Evaluamos las combinaciones (Cambia los true/false según la lógica que quieras)
        
        // COMBINACIÓN 1: Palanca 2 -> Quita la pared 1
        if (p1 == false && p2 == true && p3 == false)
        {
            if (pared1 != null) pared1.SetActive(false);
        }
        // COMBINACIÓN 2: Palanca 1 y 3 -> Quita la pared 2
        else if (p1 == true && p2 == false && p3 == true)
        {
            if (pared2 != null) pared2.SetActive(false);
        }
        // COMBINACIÓN 3: Palanca 2 y 3 -> Quita la pared 3
        else if (p1 == false && p2 == true && p3 == true)
        {
            if (pared3 != null) pared3.SetActive(false);
        }
        
        // Si la combinación actual de palancas no coincide con ninguna de las anteriores,
        // ninguna pared se desactiva (se quedan todas puestas).
    }

    private void RestaurarParedes()
    {
        if (pared1 != null) pared1.SetActive(true);
        if (pared2 != null) pared2.SetActive(true);
        if (pared3 != null) pared3.SetActive(true);
    }
}