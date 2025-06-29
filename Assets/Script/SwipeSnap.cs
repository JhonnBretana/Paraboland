using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SwipeSnap : MonoBehaviour, IEndDragHandler
{
    public ScrollRect scrollRect;
    public float snapSpeed = 10f;
    public float scaleFactor = 1.2f;
    public float scaleSpeed = 5f;

    [HideInInspector] public int currentIndex = -1;

    void Update()
    {
        // Find the card closest to the center of the Viewport
        float centerX = scrollRect.viewport.position.x;
        float closestDistance = Mathf.Infinity;
        int focusedIndex = -1;

        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform card = scrollRect.content.GetChild(i).GetComponent<RectTransform>();
            float cardPos = card.position.x;
            float distance = Mathf.Abs(cardPos - centerX);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                focusedIndex = i;
            }
        }

        currentIndex = focusedIndex; // Track which card is centered

        // Scale only the focused (centered) card
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform card = scrollRect.content.GetChild(i).GetComponent<RectTransform>();
            float targetScale = (i == focusedIndex) ? scaleFactor : 1f;
            Vector3 newScale = new Vector3(targetScale, targetScale, 1f);
            card.localScale = Vector3.Lerp(card.localScale, newScale, Time.deltaTime * scaleSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Center of the viewport in world space
        float viewportCenter = scrollRect.viewport.position.x;

        float closest = Mathf.Infinity;
        int closestIndex = -1;

        // Find the card closest to center
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform card = scrollRect.content.GetChild(i).GetComponent<RectTransform>();
            float cardPos = card.position.x;

            float distance = Mathf.Abs(cardPos - viewportCenter);

            if (distance < closest)
            {
                closest = distance;
                closestIndex = i;
            }
        }

        // Move content so selected card is centered
        if (closestIndex >= 0)
        {
            CenterCard(closestIndex);
        }
    }

    private void CenterCard(int index)
    {
        RectTransform target = scrollRect.content.GetChild(index).GetComponent<RectTransform>();

        // Position of the card relative to content
        Vector3 cardLocalPos = scrollRect.content.InverseTransformPoint(target.position);
        Vector3 viewportLocalCenter = scrollRect.content.InverseTransformPoint(scrollRect.viewport.position);

        Vector3 difference = viewportLocalCenter - cardLocalPos;

        Vector3 newPos = scrollRect.content.localPosition + difference;

        // Start moving
        StartCoroutine(SmoothMoveContent(scrollRect.content.localPosition, newPos));
    }

    private IEnumerator SmoothMoveContent(Vector3 start, Vector3 end)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            scrollRect.content.localPosition = Vector3.Lerp(start, end, t);
            yield return null;
        }

        scrollRect.content.localPosition = end;
    }
}
