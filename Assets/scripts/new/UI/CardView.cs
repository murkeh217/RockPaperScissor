using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardView : MonoBehaviour
{
    public Image iconImage;
    public Image background;

    private RPSCard card;
    private System.Action<RPSCard> onClick;
    private bool isUsed;

    public void Setup(RPSCard newCard, System.Action<RPSCard> clickCallback)
    {
        card = newCard;
        iconImage.sprite = card.icon;
        onClick = clickCallback;
        isUsed = false;
        background.color = Color.white;
        transform.localScale = Vector3.one;
    }

    public void OnCardClicked()
    {
        if (isUsed) return;

        isUsed = true;
        background.color = Color.gray;
        StartCoroutine(FlipAnimation());
        onClick?.Invoke(card);
    }

    IEnumerator FlipAnimation()
    {
        float time = 0f;
        float duration = 0.2f;

        while (time < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.zero;

        time = 0f;
        while (time < duration)
        {
            float scale = Mathf.Lerp(0f, 1f, time / duration);
            transform.localScale = new Vector3(scale, 1f, 1f);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }
}
