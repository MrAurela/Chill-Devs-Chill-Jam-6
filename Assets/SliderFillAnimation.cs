using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderFillAnimation : MonoBehaviour
{
    public Color fullColor;
    public Color emptyColor;
    private Image image;
    private void Start()
    {
        image = gameObject.GetComponent<Image>();
    }
    // Start is called before the first frame update
    public void UpdateFill()
    {
        if (image == null) return;
        
        float val = gameObject.GetComponentInParent<Slider>().value/ gameObject.GetComponentInParent<Slider>().maxValue;
        image.color = Color.Lerp(emptyColor, fullColor, val);

    }
}
