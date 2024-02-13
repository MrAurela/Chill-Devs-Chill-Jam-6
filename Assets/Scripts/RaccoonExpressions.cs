using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaccoonExpressions : MonoBehaviour
{
    [SerializeField] Sprite happy, neutral, scared;
    [SerializeField] Image raccoonImage;

    private enum Expression { Happy, Neutral, Scared };
    private Expression expression;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        this.expression = Expression.Neutral;
        SetExpression(this.expression);
        score = 1;
    }

    public void UpdateExpression()
    {
        int currentScore = FindObjectOfType<Grid>().UpdateScore();

        if (FindObjectOfType<Hand>().GetCardCount() == 0)
        {
            if (currentScore >= 110) this.expression = Expression.Happy;
            else if (currentScore >= 60) this.expression = Expression.Neutral;
            else this.expression = Expression.Scared;
        } else
        {
            if (currentScore >= score + 2 ) this.expression = Expression.Happy;
            else if (currentScore >= score) this.expression = Expression.Neutral;
            else this.expression = Expression.Scared;
        }

        SetExpression(this.expression);

        score = currentScore;
    }

    private void SetExpression(Expression expression)
    {
        switch (expression)
        {
            case Expression.Happy:
                raccoonImage.sprite = happy;
                break;
            case Expression.Neutral:
                raccoonImage.sprite = neutral;
                break;
            case Expression.Scared:
                raccoonImage.sprite = scared;
                break;
        }
    }
}
