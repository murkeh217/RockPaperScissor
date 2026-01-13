using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPSCardZoneManager : RPSCardZoneManager<RPSCardModel>
{

    [Header("--------------Data----------------")]
    [Tooltip("card zone manager to manage cards in the card zone")]
    [SerializeField] RPSCardZoneManager cardZoneManagerRed;
    [SerializeField] RPSCardZoneManager cardZoneManagerBlue;

    [Tooltip("Card model to use for cards")]
    [SerializeField] RPSCardModel[] cards;


    // --------------------------MONO methods------------------------

    void Start()
    {
        RPSCardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
        List<RPSCardModel> cards = new()
    {
        randomCard
    };
        cardZoneManagerRed.AddGroup(cards);
        cardZoneManagerRed.RefreshCardZone();
    }



    // --------------------------HELPER METHODS------------------------
    public void AddCardToRed()
    {
        RPSCardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
        List<RPSCardModel> cards = new(){
            randomCard,
        };
        cardZoneManagerRed.AddGroup(cards);
        cardZoneManagerRed.RefreshCardZone();
    }
    public void AddCardToBlue()
    {
        RPSCardModel randomCard = this.cards[Random.Range(0, this.cards.Length)];
        List<RPSCardModel> cards = new(){
            randomCard,
        };
        cardZoneManagerBlue.AddGroup(cards);
        cardZoneManagerBlue.RefreshCardZone();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("V2");
    }

}