using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightOnHover : MonoBehaviour
{
    private Color originalColor;
    public Color highlightColor = Color.yellow; // 光る色

    private Renderer objRenderer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
    }

    void OnMouseEnter()
    {
        objRenderer.material.color = highlightColor; // 光る
    }

    void OnMouseExit()
    {
        objRenderer.material.color = originalColor; // 元の色に戻す
    }
}
