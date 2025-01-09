using System.Collections;
using UnityEngine;

public class ScaleOnEnable : MonoBehaviour
{
    public Vector3 startScale;
    [SerializeField]
    private Vector3 _targetScale = Vector3.one;
    public float duration = 0.2f;
    public AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);  // Optional: for smooth easing


    private void OnEnable()
    {
        StartCoroutine(ScaleGameObject(GetComponent<RectTransform>()));
    }
    IEnumerator ScaleGameObject(RectTransform transform)
    {
        float elapsedTime = 0f;
        transform.localScale = startScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / duration;

            // Apply easing curve if specified, otherwise use linear interpolation
            float curveValue = scaleCurve.Evaluate(progress);

            transform.localScale = Vector3.Lerp(startScale, _targetScale, curveValue);
            yield return null;
        }

        // Ensure we end up exactly at the target scale
        transform.localScale = _targetScale;
    }
}
