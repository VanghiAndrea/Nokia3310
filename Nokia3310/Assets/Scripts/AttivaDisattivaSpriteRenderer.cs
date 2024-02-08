using UnityEngine;

public class AttivaDisattivaSpriteRenderer : MonoBehaviour
{
    public SpriteRenderer spriteRendererOggettoCorrente;
    public SpriteRenderer spriteRendererOggettoEsterno;

    void Start()
    {
        // Assicurati che i sprite renderers siano stati assegnati
        if (spriteRendererOggettoCorrente == null || spriteRendererOggettoEsterno == null)
        {
            Debug.LogError("Uno o entrambi gli sprite renderer non sono stati assegnati.");
        }
    }

    void Update()
    {
        // Controlla lo stato di attivazione del GameObject possessore dello script
        if (gameObject.activeSelf)
        {
            // Se l'oggetto possessore dello script � attivo, disattiva il suo sprite renderer
            if (spriteRendererOggettoCorrente.enabled)
            {
                spriteRendererOggettoCorrente.enabled = false;
            }

            // Controlla se lo sprite renderer dell'oggetto esterno � disattivo e in tal caso attivalo
            if (!spriteRendererOggettoEsterno.enabled)
            {
                spriteRendererOggettoEsterno.enabled = true;
            }
        }
        else
        {
            // Se l'oggetto possessore dello script � disattivo, attiva il suo sprite renderer
            if (!spriteRendererOggettoCorrente.enabled)
            {
                spriteRendererOggettoCorrente.enabled = true;
            }

            // Controlla se lo sprite renderer dell'oggetto esterno � attivo e in tal caso disattivalo
            if (spriteRendererOggettoEsterno.enabled)
            {
                spriteRendererOggettoEsterno.enabled = false;
            }
        }
    }
}
