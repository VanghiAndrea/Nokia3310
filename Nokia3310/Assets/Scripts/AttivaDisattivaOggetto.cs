using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttivaDisattivaOggetto : MonoBehaviour
{
    public GameObject oggettoDaAttivareDisattivare;

    void Start()
    {
        // Assicurati che l'oggetto da attivare/disattivare non sia nullo
        if (oggettoDaAttivareDisattivare == null)
        {
            Debug.LogError("L'oggetto da attivare/disattivare non è stato assegnato.");
        }
    }

    void Update()
    {
        // Controlla lo stato di attivazione del GameObject possessore dello script
        if (gameObject.activeSelf)
        {
            // Se l'oggetto possessore dello script è attivo, disattiva l'oggetto
            if (oggettoDaAttivareDisattivare.activeSelf)
            {
                oggettoDaAttivareDisattivare.SetActive(false);
            }
        }
        else
        {
            // Se l'oggetto possessore dello script è disattivo, attiva l'oggetto
            if (!oggettoDaAttivareDisattivare.activeSelf)
            {
                oggettoDaAttivareDisattivare.SetActive(true);
            }
        }
    }
}
