using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour
{
    public Image iconImage;

    private RPSCard card;
    private System.Action<RPSCard> onClick;

    public void Setup(RPSCard newCard, System.Action<RPSCard> clickCallback)
    {
        card = newCard;
        iconImage.sprite = card.icon;
        onClick = clickCallback;
    }

    public void OnCardClicked()
    {
        onClick?.Invoke(card);
    }
}
