using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Variabile booleana per controllare se tutti gli slot sono "Occupato"
    private bool allOccupied = false;

    // Variabile booleana per controllare se si sta chiudendo
    private bool closing = false;
    public bool Closing
    {
        get { return closing; }
        private set
        {
            closing = value;
            Debug.Log("Closing state changed to: " + value);
            if (value)
            {
                ActivateClosingObjects();
            }
            else
            {
                DeactivateObjectsByKeys();
                ResetActivationHistory();
            }
        }
    }

    // Variabile booleana per controllare se l'ordine è corretto
    private bool rightOrder = false;
    public bool RightOrder
    {
        get { return rightOrder; }
        private set
        {
            rightOrder = value;
            Debug.Log("Right order state changed to: " + value);
        }
    }

    // Variabile intera per il punteggio
    private int score = 0;

    // GameObject da attivare premendo i tasti
    public List<GameObject> objectsToActivateByKeys = new List<GameObject>();

    // GameObject da attivare quando "Closing" è vera
    public List<GameObject> objectsToActivateOnClosing = new List<GameObject>();

    // Tasti corrispondenti per attivare gli oggetti
    public KeyCode[] activationKeys;

    // Delay per disattivare gli oggetti
    public float delayToDeactivate = 0.6f;

    // GameObject contenente gli oggetti di interesse per il controllo dell'ordine
    public GameObject orderCheckObject;

    private List<KeyCode> pressedKeys = new List<KeyCode>();

    void Update()
    {
        // Controlla se tutti gli slot sono "Occupato"
        allOccupied = CheckAllOccupied();

        // Controlla se si sta chiudendo
        if (allOccupied)
        {
            Closing = true;
        }

        // Controlla la sequenza solo se si sta chiudendo
        if (Closing == true)
        {
            // Registra i tasti premuti
            foreach (KeyCode key in activationKeys)
            {
                if (Input.GetKeyDown(key) && !pressedKeys.Contains(key))
                {
                    pressedKeys.Add(key);
                }
            }

            // Controlla se sono stati premuti tutti i tasti almeno una volta
            if (pressedKeys.Count == activationKeys.Length)
            {
                if (CheckAllObjectsActivated())
                {
                    CheckOrderOfActivation();
                }
            }

            // Attiva gli oggetti premendo i tasti solo se si sta chiudendo
            for (int i = 0; i < activationKeys.Length; i++)
            {
                if (Input.GetKeyDown(activationKeys[i]))
                {
                    if (Closing)
                    {
                        objectsToActivateByKeys[i].SetActive(true);
                        objectsToActivateByKeys[i].transform.SetAsLastSibling();
                    }
                }
            }
        }
        else
        {
            // Disattiva gli oggetti premendo i tasti solo se la chiusura è già avvenuta
            for (int i = 0; i < activationKeys.Length; i++)
            {
                if (Input.GetKeyDown(activationKeys[i]))
                {
                    objectsToActivateByKeys[i].SetActive(false);
                }
            }
        }
    }

    // Metodo per controllare se tutti gli slot sono "Occupato"
    private bool CheckAllOccupied()
    {
        foreach (Slot slot in FindObjectOfType<SlotManager>().slots)
        {
            if (slot.currentState != Slot.SlotState.Occupato)
            {
                return false;
            }
        }
        return true;
    }

    // Metodo per attivare gli oggetti quando "Closing" è vera
    private void ActivateClosingObjects()
    {
        foreach (GameObject obj in objectsToActivateOnClosing)
        {
            obj.SetActive(true);
        }
    }

    // Metodo per disattivare gli oggetti premendo i tasti
    private void DeactivateObjectsByKeys()
    {
        foreach (GameObject obj in objectsToActivateByKeys)
        {
            obj.SetActive(false);
        }
    }

    // Metodo per controllare se tutti gli oggetti sono stati attivati
    private bool CheckAllObjectsActivated()
    {
        foreach (GameObject obj in objectsToActivateByKeys)
        {
            if (!obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    // Metodo per controllare l'ordine della sequenza
    private void CheckOrderOfActivation()
    {
        Transform firstChild = orderCheckObject.transform.GetChild(0);
        Transform secondChild = orderCheckObject.transform.GetChild(1);

        if ((firstChild == objectsToActivateByKeys[0].transform || secondChild == objectsToActivateByKeys[0].transform) &&
            (firstChild == objectsToActivateByKeys[1].transform || secondChild == objectsToActivateByKeys[1].transform))
        {
            RightOrder = true;
        }
        else
        {
            RightOrder = false;
            DeactivateObjectsByKeysWithDelay(delayToDeactivate);
        }
    }

    // Metodo per disattivare gli oggetti con un ritardo
    private void DeactivateObjectsByKeysWithDelay(float delay)
    {
        StartCoroutine(DeactivateObjectsCoroutine(delay));
    }

    // Coroutine per disattivare gli oggetti con un ritardo
    private IEnumerator DeactivateObjectsCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (GameObject obj in objectsToActivateByKeys)
        {
            obj.SetActive(false);
        }
    }

    // Metodo per reimpostare la storia di attivazione dei tasti
    private void ResetActivationHistory()
    {
        pressedKeys.Clear();
    }
}
