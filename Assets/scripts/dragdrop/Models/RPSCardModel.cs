using UnityEngine;

[System.Serializable]
public class RPSCardModel : MonoBehaviour
{
    public enum CardType
    {
        Rock,
        Paper,
        Scissor
    }
    public CardType cardType;
    public string cardName;
    public Sprite cardImage;

    public RPSCardModel(CardType card, string str, Sprite img)
    {
        this.cardType = card;
        this.cardName = str;
        this.cardImage = img;
    }
}
