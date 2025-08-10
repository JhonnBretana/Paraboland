using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[DisallowMultipleComponent]
public class ScreenFader : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image blackoutImage;
    public bool startCovered = false;
    public float fadeInDuration = 0.45f;   // to black
    public float fadeOutDuration = 0.45f;  // from black
    public float holdBlack = 0.05f;
    public AnimationCurve ease = AnimationCurve.EaseInOut(0,0,1,1);

    void Awake() {
        if (!canvasGroup) canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = startCovered ? 1f : 0f;
        canvasGroup.blocksRaycasts = startCovered;
        canvasGroup.interactable = false;
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeToBlack(float? d=null) {
        canvasGroup.blocksRaycasts = true;
        float dur = d ?? fadeInDuration;
        yield return StartCoroutine(Fade(canvasGroup.alpha, 1f, dur));
        if (holdBlack > 0f) yield return new WaitForSecondsRealtime(holdBlack);
    }

    public IEnumerator FadeFromBlack(float? d=null) {
        float dur = d ?? fadeOutDuration;
        yield return StartCoroutine(Fade(canvasGroup.alpha, 0f, dur));
        canvasGroup.blocksRaycasts = false;
    }

    IEnumerator Fade(float a0, float a1, float d){
        if (Mathf.Approximately(a0,a1) || d <= 0f) { canvasGroup.alpha = a1; yield break; }
        float t = 0f;
        while (t < d) {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Lerp(a0, a1, ease.Evaluate(Mathf.Clamp01(t/d)));
            yield return null;
        }
        canvasGroup.alpha = a1;
    }
}
