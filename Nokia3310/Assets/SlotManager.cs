using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SlotManager : MonoBehaviour
{
    // Array pubblici che contengono gli slot
    public Slot[] slots;

    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Controlla se il tasto corrispondente allo slot è premuto
            if (Input.GetKeyDown(KeyCode.Keypad1 + i))
            {
                // Passa allo stato successivo per lo slot corrente
                slots[i].ChangeState();
            }
        }
    }
}

[System.Serializable]
public class Slot
{
    // Due oggetti pubblici da assegnare nell'Inspector
    public GameObject object1;
    public GameObject object2;

    // Enum per gli stati dello slot
    public enum SlotState
    {
        Vuoto,
        Occupato,
        Pieno
    }

    // Stato attuale dello slot
    public SlotState currentState;

    public void ChangeState()
    {
        // Cambia lo stato dello slot
        switch (currentState)
        {
            case SlotState.Vuoto:
                // Attiva il primo oggetto
                object1.SetActive(true);
                currentState = SlotState.Occupato;
                break;
            case SlotState.Occupato:
                // Attiva il secondo oggetto
                object2.SetActive(true);
                currentState = SlotState.Pieno;
                break;
            case SlotState.Pieno:
                // Disattiva il secondo oggetto
                object2.SetActive(false);
                currentState = SlotState.Occupato;
                break;
        }
    }
}
