using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] int fps = 60;
    [SerializeField] float defaultLength = 1f;
    [SerializeField] Image dark;

    void Start()
    {
        dark.color = new Color(dark.color.r, dark.color.g, dark.color.b, 1f);
        StartCoroutine(FadeIn());
    }

    // Fade in to light
    public IEnumerator FadeIn(float parameterLength = -1f)
    {
        dark.gameObject.SetActive(true);

        float length = parameterLength < 0f ? defaultLength : parameterLength;

        float secs = 0;
        float start = dark.color.a;
        while (dark.color.a > 0f)
        {
            dark.color = new Color(dark.color.r, dark.color.g, dark.color.b, Mathf.Lerp(start, 0f, secs / length));
            yield return new WaitForSeconds(1 / fps);

            secs += Time.deltaTime;
        }


        dark.gameObject.SetActive(false);
    }

    // Fade out to black
    public IEnumerator FadeOut(float parameterLength = -1f)
    {
        dark.gameObject.SetActive(true);

        float length = parameterLength < 0f ? defaultLength : parameterLength;

        float secs = 0;
        float start = dark.color.a;
        while (dark.color.a < 1f)
        {
            dark.color = new Color(dark.color.r, dark.color.g, dark.color.b, Mathf.Lerp(start, 1f, secs / length));
            yield return new WaitForSeconds(1 / fps);

            secs += Time.deltaTime;
        }

        dark.gameObject.SetActive(true);
    }
}
