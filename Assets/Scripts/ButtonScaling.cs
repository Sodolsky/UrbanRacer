using UnityEngine;
using UnityEngine.UI;

public class ButtonScaling : MonoBehaviour
{
    private Vector3 originalScale;
    private Button button;

    private void Start()
    {
        // Pobieramy pocz�tkowy rozmiar przycisku
        originalScale = transform.localScale;

        // Pobieramy komponent Button przypisany do tego samego obiektu
        button = GetComponent<Button>();
    }

    private void OnMouseEnter()
    {
        // Powi�kszamy przycisk po najechaniu kursorem
        transform.localScale = originalScale * 1.2f;
    }

    private void OnMouseExit()
    {
        // Powr�t do pierwotnego rozmiaru przycisku po opuszczeniu kursorem
        transform.localScale = originalScale;
    }

    private void OnMouseDown()
    {
        // Pomniejszamy przycisk po klikni�ciu w niego
        transform.localScale = originalScale;
    }
}
