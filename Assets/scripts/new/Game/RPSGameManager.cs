using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class RPSGameManager : MonoBehaviour
{
    public List<RPSCard> allCards;

    public Transform playerHandParent;
    public GameObject cardPrefab;

    public TMP_Text roundText;
    public TMP_Text scoreText;

    List<RPSCard> playerHand;
    List<RPSCard> aiHand;

    int round;
    int playerScore;
    int aiScore;

    bool isResolving;

    [Header("AI UI")]
    public Image aiCardImage;
    public Sprite aiCardBack;

    [Header("End Game")]
    public GameObject endPanel;
    public TMP_Text resultText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip cardPlay;
    public AudioClip winSound;
    public AudioClip loseSound;

    int coins;
    int winStreak;

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        winStreak = PlayerPrefs.GetInt("WinStreak", 0);
        StartGame();
    }

    void StartGame()
    {
        round = 1;
        playerScore = 0;
        aiScore = 0;
        isResolving = false;

        endPanel.SetActive(false);

        playerHand = DrawCards(7);
        aiHand = DrawCards(7);

        SpawnPlayerCards();

        aiCardImage.sprite = aiCardBack;
        aiCardImage.transform.localScale = Vector3.one;
        aiCardImage.enabled = true;

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
            Instantiate(cardPrefab, playerHandParent)
                .GetComponent<CardView>()
                .Setup(card, OnPlayerCardSelected);
        }
    }

    void OnPlayerCardSelected(RPSCard playerCard)
    {
        if (isResolving || round > 7) return;

        isResolving = true;

        audioSource.PlayOneShot(cardPlay);
        Handheld.Vibrate();

        StartCoroutine(AIDelay(playerCard));
    }


    IEnumerator AIDelay(RPSCard playerCard)
    {
        yield return new WaitForSeconds(0.4f);

        RPSCard aiCard = PickAICard();
        yield return StartCoroutine(ResolveRoundSequence(playerCard, aiCard));
    }


    IEnumerator ResolveRoundSequence(RPSCard playerCard, RPSCard aiCard)
    {
        isResolving = true;

        // Ensure AI card starts face-down & scale reset
        aiCardImage.sprite = aiCardBack;
        aiCardImage.transform.localScale = Vector3.one;

        yield return StartCoroutine(RevealAICard(aiCard));

        int result = ResolveRound(playerCard.cardType, aiCard.cardType);
        if (result == 1) playerScore++;
        else if (result == -1) aiScore++;

        playerHand.Remove(playerCard);
        aiHand.Remove(aiCard);

        round++;

        // END GAME CHECK FIRST
        if (round > 7)
        {
            UpdateUI();
            EndGame();
            yield break;
        }

        UpdateUI();
        SpawnPlayerCards();

        // Reset AI card back for next round
        aiCardImage.sprite = aiCardBack;
        aiCardImage.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(0.2f);
        isResolving = false;

    }
    IEnumerator RevealAICard(RPSCard aiCard)
    {
        aiCardImage.transform.localScale = Vector3.one;
        aiCardImage.sprite = aiCardBack;

        yield return new WaitForSeconds(0.05f);

        float t = 0f;
        while (t < 0.2f)
        {
            aiCardImage.transform.localScale =
                new Vector3(Mathf.Lerp(1, 0, t / 0.2f), 1, 1);
            t += Time.deltaTime;
            yield return null;
        }

        aiCardImage.sprite = aiCard.icon;

        t = 0f;
        while (t < 0.2f)
        {
            aiCardImage.transform.localScale =
                new Vector3(Mathf.Lerp(0, 1, t / 0.2f), 1, 1);
            t += Time.deltaTime;
            yield return null;
        }

        // GUARANTEE visibility
        aiCardImage.transform.localScale = Vector3.one;
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
        isResolving = true;

        endPanel.SetActive(true);

        if (playerScore > aiScore)
        {
            audioSource.PlayOneShot(winSound);
            winStreak++;
            int reward = 10 + (winStreak * 2);
            coins += reward;
            resultText.text = $"YOU WIN!\n+{reward} Coins";
        }
        else if (playerScore < aiScore)
        {
            audioSource.PlayOneShot(loseSound);
            winStreak = 0;
            resultText.text = "YOU LOSE";
        }
        else
        {
            resultText.text = "DRAW";
        }

        PlayerPrefs.SetInt("Coins", coins);
        PlayerPrefs.SetInt("WinStreak", winStreak);
    }

    public void PlayAgain()
    {
        StartGame();
    }
}
