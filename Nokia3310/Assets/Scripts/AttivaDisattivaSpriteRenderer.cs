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
        // Se lo sprite renderer dell'oggetto corrente è attivo, disattiva lo sprite renderer dell'oggetto esterno
        if (spriteRendererOggettoCorrente.enabled)
        {
            spriteRendererOggettoEsterno.enabled = false;
        }
        // Se lo sprite renderer dell'oggetto esterno è attivo, disattiva lo sprite renderer dell'oggetto corrente
        else if (spriteRendererOggettoEsterno.enabled)
        {
            spriteRendererOggettoCorrente.enabled = false;
        }
    }
}
