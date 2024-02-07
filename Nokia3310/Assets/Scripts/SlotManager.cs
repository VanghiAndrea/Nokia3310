using UnityEngine;
using System.Collections.Generic;

public class SlotManager : MonoBehaviour
{
    // Dictionary per la mappatura tra tasti e slot
    public Dictionary<KeyCode, Slot> keyToSlotMap = new Dictionary<KeyCode, Slot>();

    // Lista pubblica contenente tutti gli slot
    public List<Slot> slots = new List<Slot>();
    public ScoreManager scoreManager;
    void Start()
    {
        // Inizializza gli slot in modo casuale
        InitializeSlots();

        // Popola la mappatura tra tasti e slot
        foreach (Slot slot in slots)
        {
            if (slot.key != KeyCode.None && !keyToSlotMap.ContainsKey(slot.key))
            {
                keyToSlotMap.Add(slot.key, slot);
            }
        }
    }

    void Update()
    {
        // Controlla la pressione dei tasti mappati solo se la booleana "Closing" è falsa
        if (!scoreManager.Closing)
        {
            foreach (var kvp in keyToSlotMap)
            {
                if (Input.GetKeyDown(kvp.Key))
                {
                    kvp.Value.ChangeState();
                }
            }
        }
    }

    // Metodo per inizializzare gli stati degli slot in modo casuale
    void InitializeSlots()
    {
        // Contatori per gli stati degli slot
        int vuotoCount = 0;
        int occupatoCount = 0;

        // Lista temporanea degli stati disponibili
        List<Slot.SlotState> availableStates = new List<Slot.SlotState> { Slot.SlotState.Vuoto, Slot.SlotState.Occupato };

        // Scansiona tutti gli slot e inizializza casualmente i loro stati
        foreach (Slot slot in slots)
        {
            // Se sono stati assegnati troppi slot Vuoto o Occupato, forza lo stato ad essere diverso
            if (vuotoCount >= 7)
            {
                availableStates.Remove(Slot.SlotState.Vuoto);
            }
            if (occupatoCount >= 7)
            {
                availableStates.Remove(Slot.SlotState.Occupato);
            }

            // Scegli casualmente uno stato tra quelli disponibili
            Slot.SlotState randomState = availableStates[Random.Range(0, availableStates.Count)];

            // Aggiorna il conteggio degli stati
            if (randomState == Slot.SlotState.Vuoto)
            {
                vuotoCount++;
            }
            else if (randomState == Slot.SlotState.Occupato)
            {
                occupatoCount++;
            }

            // Imposta lo stato dello slot
            slot.currentState = randomState;

            // Disattiva gli oggetti in base allo stato iniziale
            slot.object1.SetActive(randomState != Slot.SlotState.Vuoto);
            slot.object2.SetActive(randomState == Slot.SlotState.Pieno);
        }
    }
}

[System.Serializable]
public class Slot
{
    // Tasto associato allo slot
    public KeyCode key;

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
