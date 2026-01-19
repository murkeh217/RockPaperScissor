using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RPSGameManager : MonoBehaviour
{
    public List<RPSCard> allCards;

    public Transform playerHandParent;
    public GameObject cardPrefab;

    public TMP_Text roundText;
    public TMP_Text scoreText;

    List<RPSCard> playerHand;
    List<RPSCard> aiHand;

    int round = 1;
    int playerScore = 0;
    int aiScore = 0;

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        round = 1;
        playerScore = 0;
        aiScore = 0;

        playerHand = DrawCards(7);
        aiHand = DrawCards(7);

        SpawnPlayerCards();
        UpdateUI();
    }

    List<RPSCard> DrawCards(int count)
    {
        List<RPSCard> temp = new List<RPSCard>(allCards);
        List<RPSCard> result = new List<RPSCard>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, temp.Count);
            result.Add(temp[index]);
            temp.RemoveAt(index);
        }
        return result;
    }

    void SpawnPlayerCards()
    {
        foreach (Transform t in playerHandParent)
            Destroy(t.gameObject);

        foreach (var card in playerHand)
        {
            GameObject go = Instantiate(cardPrefab, playerHandParent);
            go.GetComponent<CardView>()
              .Setup(card, OnPlayerCardSelected);
        }
    }

    void OnPlayerCardSelected(RPSCard playerCard)
    {
        if (round > 7) return;

        RPSCard aiCard = PickAICard();
        int result = ResolveRound(playerCard.cardType, aiCard.cardType);

        if (result == 1) playerScore++;
        else if (result == -1) aiScore++;

        playerHand.Remove(playerCard);
        aiHand.Remove(aiCard);

        round++;
        UpdateUI();
        SpawnPlayerCards();

        if (round > 7)
            EndGame();
    }

    RPSCard PickAICard()
    {
        return aiHand[Random.Range(0, aiHand.Count)];
    }

    int ResolveRound(CardType p, CardType a)
    {
        if (p == a) return 0;

        if (
            (p == CardType.Rock && a == CardType.Scissors) ||
            (p == CardType.Paper && a == CardType.Rock) ||
            (p == CardType.Scissors && a == CardType.Paper)
        )
            return 1;

        return -1;
    }

    void UpdateUI()
    {
        roundText.text = $"Round {round}/7";
        scoreText.text = $"You {playerScore} : {aiScore} AI";
    }

    void EndGame()
    {
        Debug.Log(playerScore > aiScore ? "YOU WIN" : "YOU LOSE");
    }
}
