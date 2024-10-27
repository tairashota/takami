using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SnapScroll : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float snapSpeed = 10f;
    private RectTransform content;
    private float[] panelPositions;
    void Start()
    {
        content = scrollRect.content;
        int panelCount = content.childCount;
        panelPositions = new float[panelCount];

        for (int i = 0; i < panelCount; i++)
        {
            panelPositions[i] = i * content.GetChild(i).GetComponent<RectTransform>().rect.width;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SnapToNearest();
        }
    }

    void SnapToNearest()
    {
        float closestDistance = Mathf.Infinity;
        float targetPosition = 0;

        for (int i = 0; i < panelPositions.Length; i++)
        {
            float distance = Mathf.Abs(content.anchoredPosition.x - panelPositions[i]);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPosition = panelPositions[i];
            }
        }

        StartCoroutine(SmoothSnap(targetPosition));
    }

    IEnumerator SmoothSnap(float target)
    {
        while (Mathf.Abs(content.anchoredPosition.x - target) > 0.1f)
        {
            content.anchoredPosition = Vector2.Lerp(content.anchoredPosition, new Vector2(target, content.anchoredPosition.y), Time.deltaTime * snapSpeed);
            yield return null;
        }
        content.anchoredPosition = new Vector2(target, content.anchoredPosition.y);
    }
}
