using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaccoonExpressions : MonoBehaviour
{
    [SerializeField] Sprite happy, neutral, scared;
    [SerializeField] Image raccoonImage;

    // Start is called before the first frame update
    void Start()
    {
        raccoonImage.sprite = neutral;
    }

    public void UpdateExpression()
    {
        float random = Random.Range(0, 100);
        if (random > 75) raccoonImage.sprite = happy;
        else if (random > 50) raccoonImage.sprite = scared;
        else raccoonImage.sprite = neutral;
    }
}
