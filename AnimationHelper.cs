using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
using static Cinemachine.CinemachineOrbitalTransposer;

public class AnimationHelper
{
    public static IEnumerator ZoomIn(RectTransform Transform, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            Transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, easedTime);
            yield return null;
            time += Time.deltaTime;
        }

        Transform.localScale = Vector3.one;

        OnEnd?.Invoke();
    }

    public static IEnumerator ZoomOut(RectTransform Transform, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            Transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, easedTime);
            yield return null;
            time += Time.deltaTime;
        }

        Transform.localScale = Vector3.zero;
        OnEnd?.Invoke();
    }

    public static IEnumerator FadeIn(CanvasGroup CanvasGroup, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        Debug.Log("FadeIn");
        CanvasGroup.blocksRaycasts = true;
        CanvasGroup.interactable = true;

        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            CanvasGroup.alpha = Mathf.Lerp(0, 1, easedTime);
            yield return null;
            time +=  Time.deltaTime ;
        }

        CanvasGroup.alpha = 1;
        OnEnd?.Invoke();
    }

    public static IEnumerator FadeOut(CanvasGroup CanvasGroup, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        CanvasGroup.blocksRaycasts = false;
        CanvasGroup.interactable = false;

        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            CanvasGroup.alpha = Mathf.Lerp(1, 0, easedTime);
            yield return null;
            time += Time.deltaTime ;
        }

        CanvasGroup.alpha = 0;
        OnEnd?.Invoke();
    }

    public static IEnumerator SlideIn(RectTransform Transform, Direction Direction, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        Vector2 startPosition;
        switch (Direction)
        {
            case Direction.UP:
                startPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.RIGHT:
                startPosition = new Vector2(-Screen.width, 0);
                break;
            case Direction.DOWN:
                startPosition = new Vector2(0, Screen.height);
                break;
            case Direction.LEFT:
                startPosition = new Vector2(Screen.width, 0);
                break;
            default:
                startPosition = new Vector2(0, -Screen.height);
                break;
        }

        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            Transform.anchoredPosition = Vector2.Lerp(startPosition, Vector2.zero, easedTime);
            yield return null;
            time += Time.deltaTime;
        }

        Transform.anchoredPosition = Vector2.zero;
        OnEnd?.Invoke();
    }

    public static IEnumerator SlideOut(RectTransform Transform, Direction Direction, float AnimationDuration, UnityEvent OnEnd, Easings Easing)
    {
        Vector2 endPosition;
        switch (Direction)
        {
            case Direction.UP:
                endPosition = new Vector2(0, Screen.height);
                break;
            case Direction.RIGHT:
                endPosition = new Vector2(Screen.width, 0);
                break;
            case Direction.DOWN:
                endPosition = new Vector2(0, -Screen.height);
                break;
            case Direction.LEFT:
                endPosition = new Vector2(-Screen.width, 0);
                break;
            default:
                endPosition = new Vector2(0, Screen.height);
                break;
        }

        float time = 0;
        EasingFunction easingFunction = GetEasingFunction(Easing);

        while (time < AnimationDuration)
        {
            float t = time / AnimationDuration; // Normalize time to 0-1 range
            float easedTime = easingFunction(t); // Apply easing function

            Transform.anchoredPosition = Vector2.Lerp(Vector2.zero, endPosition, easedTime);
            yield return null;
            time += Time.deltaTime ;
        }

        Transform.anchoredPosition = endPosition;
        OnEnd?.Invoke();
    }

    public static float calculateDuration(RectTransform Transform, Vector2 endPosition, float Speed)
    {
        Vector2 startPosition = Transform.anchoredPosition;
        float distance = Vector2.Distance(startPosition, endPosition);
        float duration = distance / Speed; // Calculate duration based on distance and speed

        return duration;
    }

    public delegate float EasingFunction(float t);

    public static EasingFunction GetEasingFunction(Easings easing)
    {
        return easing switch
        {
            Easings.Linear => Linear,
            Easings.EaseInSin => EaseInSin,
            Easings.EaseInQuad => EaseInQuad,
            Easings.EaseInQuint => EaseInQuint,
            Easings.EaseInCubic => EaseInCubic,
            Easings.EaseInQuart => EaseInQuart,
            Easings.EaseInExpo => EaseInExpo,
            Easings.EaseInCirc => EaseInCirc,
            Easings.EaseInBack => EaseInBack,
            Easings.EaseInQuartic => EaseInQuartic,
            Easings.EaseInOutSine => EaseInOutSine,
            Easings.EaseInOutCubic => EaseInOutCubic,
            Easings.EaseInOutQuart => EaseInOutQuart,
            Easings.EaseInOutQuint => EaseInOutQuint,
            Easings.EaseInOutExpo => EaseInOutExpo,
            Easings.EaseInOutCirc => EaseInOutCirc,
            Easings.EaseInOutBack => EaseInOutBack,
            Easings.EaseInOutQuadratic => EaseInOutQuadratic,
            Easings.EaseInOutQuad => EaseInOutQuad,
            Easings.EaseInOutElastic => EaseInOutElastic,
            Easings.EaseInOutBounce => EaseInOutBounce,
            Easings.EaseOutElastic => EaseOutElastic,
            Easings.EaseOutBounce => EaseOutBounce,
            Easings.EaseOutCubic => EaseOutCubic,
            Easings.EaseOutQuint => EaseOutQuint,
            Easings.EaseOutSine => EaseOutSine,
            Easings.EaseOutQuad => EaseOutQuad,
            Easings.EaseOutQuart => EaseOutQuart,
            Easings.EaseOutExpo => EaseOutExpo,
            Easings.EaseOutCirc => EaseOutCirc,
            Easings.EaseOutBack => EaseOutBack,
            Easings.EaseOutQuadratic => EaseOutQuadratic,
            Easings.EaseOutQuartic => EaseOutQuartic,
            _ => Linear,
        };
    }


    public static float Linear(float t) => t;
    public static float EaseInSin(float t) => 1 - Mathf.Cos(t * Mathf.PI / 2);
    public static float EaseInQuad(float t) => t * t;
    public static float EaseInQuint(float t) => t * t * t * t * t;
    public static float EaseInCubic(float t) => t * t * t;
    public static float EaseInQuart(float t) => t * t * t * t;
    public static float EaseInExpo(float t) => t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
    public static float EaseInCirc(float t) => 1 - Mathf.Sqrt(1 - t * t);
    public static float EaseInBack(float t) { const float c1 = 1.70158f; return (c1 + 1) * t * t * t - c1 * t * t; }
    public static float EaseInQuartic(float t) => t * t * t * t;
    public static float EaseInOutSine(float t) => -0.5f * (Mathf.Cos(Mathf.PI * t) - 1);
    public static float EaseInOutCubic(float t) => t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    public static float EaseInOutQuart(float t) => t < 0.5f ? 8 * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
    public static float EaseInOutQuint(float t) => t < 0.5f ? 16 * t * t * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
    public static float EaseInOutExpo(float t) => t == 0 ? 0 : t == 1 ? 1 : t < 0.5f ? Mathf.Pow(2, 20 * t - 10) / 2 : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
    public static float EaseInOutCirc(float t) => t < 0.5f ? (1 - Mathf.Sqrt(1 - 4 * t * t)) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
    public static float EaseInOutBack(float t) { const float c1 = 1.70158f, c2 = c1 * 1.525f; return t < 0.5f ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2 : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (2 * t - 2) + c2) + 2) / 2; }
    public static float EaseInOutQuadratic(float t) => t < 0.5f ? 2 * t * t : -2 * t * t + 4 * t - 1;
    public static float EaseInOutQuad(float t) => t < 0.5f ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
    public static float EaseInOutElastic(float t) { const float c5 = (2 * Mathf.PI) / 4.5f; if (t == 0) return 0; if (t == 1) return 1; t *= 2; return t < 1 ? -0.5f * Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t - 1.1f) * c5) : Mathf.Pow(2, -10 * (t - 1)) * Mathf.Sin((t - 1.1f) * c5) * 0.5f + 1; }
    public static float EaseInOutBounce(float t) => t < 0.5f ? (1 - EaseOutBounce(1 - 2 * t)) * 0.5f : (1 + EaseOutBounce(2 * t - 1)) * 0.5f;
    public static float EaseOutElastic(float t) { const float c4 = (2 * Mathf.PI) / 3; return t == 0 ? 0 : t == 1 ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c4) + 1; }
    public static float EaseOutBounce(float t) => t < 1 / 2.75f ? 7.5625f * t * t : t < 2 / 2.75f ? 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f : t < 2.5 / 2.75f ? 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f : 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
    public static float EaseOutCubic(float t) => 1 - Mathf.Pow(1 - t, 3);
    public static float EaseOutQuint(float t) => 1 - Mathf.Pow(1 - t, 5);
    public static float EaseOutSine(float t) => Mathf.Sin(t * Mathf.PI / 2);
    public static float EaseOutQuad(float t) => 1 - (1 - t) * (1 - t);
    public static float EaseOutQuart(float t) => 1 - Mathf.Pow(1 - t, 4);
    public static float EaseOutExpo(float t) => t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
    public static float EaseOutCirc(float t) => Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
    public static float EaseOutBack(float t) { const float c1 = 1.70158f; return 1 + (c1 + 1) * Mathf.Pow(t - 1, 3) + c1 * Mathf.Pow(t - 1, 2); }
    public static float EaseOutQuadratic(float t) => -t * (t - 2);
    public static float EaseOutQuartic(float t) => 1 - Mathf.Pow(1 - t, 4);





}