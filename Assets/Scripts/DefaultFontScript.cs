using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DefaultFontScript : MonoBehaviour 
{ 
    public Font defaultFont;
    public TMP_FontAsset defaultFontTMP;
    public Material defaultMaterial;
    public int fontSize = -1; // Global Font Size

    void OnGUI()
    {
        var textComponents = Component.FindObjectsOfType<Text>();
        foreach (var component in textComponents)
        { 
            component.font = defaultFont;
            component.material = defaultMaterial;
            if (fontSize > 0)
            { 
                component.fontSize = fontSize;
            }
        }

        var textComponentsTMP = Component.FindObjectsOfType<TextMeshProUGUI>();
        foreach (var component in textComponentsTMP)
        {
            component.font = defaultFontTMP;
            component.material = defaultMaterial;
            if (fontSize > 0)
            {
                component.fontSize = fontSize;
            }
        }
    }

}
