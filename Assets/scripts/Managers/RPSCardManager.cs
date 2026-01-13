using UnityEngine.UI;

public class RPSCardManager : RPSCardManager<RPSCardModel>
{
    Image cardImage;

    void Awake()
    {
        cardImage = GetComponent<Image>();
    }

    public override void SetData(RPSCardModel cardModel)
    {
        if (cardModel.cardImage != null)
        {
            this.cardImage.sprite = cardModel.cardImage;
        }
    }

    public override void UpdateSelection(bool isSelected)
    {
        this.isSelected = isSelected;
    }
}