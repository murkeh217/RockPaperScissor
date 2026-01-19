using UnityEngine;

[CreateAssetMenu(fileName = "RPSCard", menuName = "RPS/Card")]
public class RPSCard : ScriptableObject
{
    public string cardName;
    public CardType cardType;
    public Sprite icon;
}
