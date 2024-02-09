using UnityEngine;

public class AttivaDisattivaSpriteRenderer : MonoBehaviour
{
    public GameObject oggettoDaControllare;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Ottieni il riferimento al proprio SpriteRenderer
        if (spriteRenderer == null)
        {
            Debug.LogError("Lo SpriteRenderer non � stato trovato sull'oggetto corrente.");
        }

        if (oggettoDaControllare == null)
        {
            Debug.LogError("L'oggetto da controllare non � stato assegnato.");
        }
    }

    void Update()
    {
        if (oggettoDaControllare.activeSelf)
        {
            // Disattiva lo sprite renderer dell'oggetto possessore dello script
            spriteRenderer.enabled = false;
        }
        else
        {
            // Altrimenti, se l'oggetto da controllare � disattivo, attiva lo sprite renderer dell'oggetto possessore dello script
            spriteRenderer.enabled = true;
        }
    }
}
