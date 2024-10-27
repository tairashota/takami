using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    private Color originalColor;
    public Color highlightColor = Color.yellow; // Œõ‚éF

    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
    }

    void OnMouseEnter()
    {
        objRenderer.material.color = highlightColor; // Œõ‚é
    }

    void OnMouseExit()
    {
        objRenderer.material.color = originalColor; // Œ³‚ÌF‚É–ß‚·
    }
}
