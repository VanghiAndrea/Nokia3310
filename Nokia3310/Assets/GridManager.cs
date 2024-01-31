using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject[,] grid = new GameObject[3, 3]; // Matrice per la griglia 3x3
    private int[,] state = new int[3, 3]; // Stati degli slot

    void Update()
    {
        // Controlla l'input da tastiera
        if (Input.GetKeyDown(KeyCode.Keypad1))
            ToggleState(0, 0);
        else if (Input.GetKeyDown(KeyCode.Keypad2))
            ToggleState(0, 1);
        else if (Input.GetKeyDown(KeyCode.Keypad3))
            ToggleState(0, 2);
        else if (Input.GetKeyDown(KeyCode.Keypad4))
            ToggleState(1, 0);
        else if (Input.GetKeyDown(KeyCode.Keypad5))
            ToggleState(1, 1);
        else if (Input.GetKeyDown(KeyCode.Keypad6))
            ToggleState(1, 2);
        else if (Input.GetKeyDown(KeyCode.Keypad7))
            ToggleState(2, 0);
        else if (Input.GetKeyDown(KeyCode.Keypad8))
            ToggleState(2, 1);
        else if (Input.GetKeyDown(KeyCode.Keypad9))
            ToggleState(2, 2);
    }

    // Cambia lo stato dello slot della griglia
    void ToggleState(int row, int col)
    {
        // Se lo stato è 3 ("Pieno"), torna direttamente allo stato 2 ("Occupato")
        if (state[row, col] == 3)
        {
            state[row, col] = 2;
        }
        else
        {
            state[row, col] = (state[row, col] % 3) + 1; // Passa allo stato successivo (1->2->3->1->...)
        }
        UpdateObjects(row, col);
    }

    // Aggiorna gli oggetti sulla base dello stato dello slot
    void UpdateObjects(int row, int col)
    {
        GameObject obj1 = grid[row, col].transform.GetChild(0).gameObject;
        GameObject obj2 = grid[row, col].transform.GetChild(1).gameObject;

        switch (state[row, col])
        {
            case 1: // Vuoto
                obj1.SetActive(false);
                obj2.SetActive(false);
                break;
            case 2: // Occupato
                obj1.SetActive(true);
                obj2.SetActive(false);
                break;
            case 3: // Pieno
                obj1.SetActive(true);
                obj2.SetActive(true);
                break;
        }
    }
}
