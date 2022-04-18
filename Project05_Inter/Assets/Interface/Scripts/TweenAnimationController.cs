using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimationController : MonoBehaviour
{
    [SerializeField, Range(0.001f, 1f)] public float AnimationTime;
    [SerializeField] public bool startFaded;
    [HideInInspector] public Color StartColor;
    [HideInInspector] public Color TransparentColor = new Color();

    private void Start()
    {
        CanvasGroup alphaRenderer = GetComponent<CanvasGroup>();
        alphaRenderer.alpha = startFaded ? 0f : 1f;
    }
    
    public IEnumerator FadeCoroutine(float fadeInTime, bool fadeIn)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        CanvasGroup alphaRenderer = GetComponent<CanvasGroup>();

        float counter = 0;
        float percentageComplete = counter / fadeInTime;

        while (fadeIn ? alphaRenderer.alpha < 1 : alphaRenderer.alpha > 0)
        {
            alphaRenderer.alpha = fadeIn ? percentageComplete : 1 - percentageComplete;

            counter += Time.deltaTime;
            percentageComplete = counter / fadeInTime;

            yield return wait;
        }

        alphaRenderer.alpha = fadeIn ? 1 : 0;

        yield break;
    }

    public IEnumerator Move(Vector3 direction, float distance, float time)
    {
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        Vector3 fromPos = transform.position;
        Vector3 toPos = transform.position + direction * distance;

        Debug.Log("From Pos: " + fromPos + " / To Pos: " + toPos);

        float enlapsedTime = 0;
        float percentageComplete = enlapsedTime / time;

        while (percentageComplete < 1)
        {
            transform.Translate(Vector3.Lerp(fromPos, toPos, Mathf.SmoothStep(0, 1, percentageComplete)));

            enlapsedTime += Time.deltaTime;
            percentageComplete = enlapsedTime / time;
            Debug.Log("Atual Pos: " + transform.position + " / To Pos: " + toPos);

            yield return wait;
        }

        yield return FadeCoroutine(AnimationTime, false);

        transform.position = fromPos;
    }

    public IEnumerator FadeInAndOut(float seconds)
    {
        yield return FadeCoroutine(AnimationTime, true);
        yield return new WaitForSeconds(seconds);
        yield return FadeCoroutine(AnimationTime, false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fadeInTime"> Sets the time of the animation.</param>
    /// <param name="fadeIn">If true, will Fade In. If False, will Fade Out.</param>
    /// <returns></returns>
    public void StartFade(float fadeTime, bool fadeIn)
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();

        if(canvas.alpha == 1 || canvas.alpha == 0)
            StartCoroutine(FadeCoroutine(fadeTime, fadeIn));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="timeInTotalFade"> Time twaiting in TotalFade. </param>
    public void StartFadeInAndFadeOut(float timeInTotalFade)
    {
        CanvasGroup canvas = GetComponent<CanvasGroup>();

        if (canvas.alpha == 1 || canvas.alpha == 0)
            StartCoroutine(FadeInAndOut(timeInTotalFade));
    }
}
