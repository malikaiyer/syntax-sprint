using UnityEngine;
using TMPro;

public class PulseText : MonoBehaviour
{
    public float pulseDuration = 0.25f;   // Time to scale up/down
    public float maxScale = 1.5f;         // How large to grow
    public float minScale = 1.0f;         // Base scale

    private TextMeshProUGUI text;
    private RectTransform rectTransform;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void PlayPulse()
    {
        StopAllCoroutines();              // Reset if already pulsing
        StartCoroutine(Pulse());
    }

    private System.Collections.IEnumerator Pulse()
    {
        float time = 0f;

        // Scale up
        while (time < pulseDuration)
        {
            float scale = Mathf.Lerp(minScale, maxScale, time / pulseDuration);
            rectTransform.localScale = Vector3.one * scale;
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;

        // Scale back down
        while (time < pulseDuration)
        {
            float scale = Mathf.Lerp(maxScale, minScale, time / pulseDuration);
            rectTransform.localScale = Vector3.one * scale;
            time += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = Vector3.one * minScale; // Reset scale
    }
}
