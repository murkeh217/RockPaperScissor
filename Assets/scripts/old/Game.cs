using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public Rock rock;
    public Paper paper;
    public Scissor scissors;

    [Header("Spawn Points")]
    public Transform player1Spawn;
    public Transform player2Spawn;

    [Header("UI Elements")]
    public TMP_Text matchText;
    public Button goButton;
    public TMP_Text P1;
    public TMP_Text P2;

    public enum RPS
    {
        Rock,
        Paper,
        Scissors
    }


    private RPS player1Choice;
    private RPS player2Choice;

    void Start()
    {
        matchText.text = "vs.";
    }

    public void GameOn()
    {
        ResetRound();

        player1Choice = SpawnForPlayer(player1Spawn);
        player2Choice = SpawnForPlayer(player2Spawn);

        DecideWinner();
    }

    void ResetRound()
    {
        matchText.text = "vs.";

        // Destroy previously spawned objects
        foreach (Transform child in player1Spawn)
            Destroy(child.gameObject);

        foreach (Transform child in player2Spawn)
            Destroy(child.gameObject);
    }

    RPS SpawnForPlayer(Transform playerSpawn)
    {
        int choice = Random.Range(0, 3);
        GameObject prefab = null;

        switch (choice)
        {
            case 0:
                prefab = rock.Prefabs[Random.Range(0, rock.Prefabs.Length)];
                break;
            case 1:
                prefab = paper.Prefabs[Random.Range(0, paper.Prefabs.Length)];
                break;
            case 2:
                prefab = scissors.Prefabs[Random.Range(0, scissors.Prefabs.Length)];
                break;
        }

        GameObject obj = Instantiate(prefab, playerSpawn);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        string objectName = obj.name.Replace("(Clone)", "");

        if (playerSpawn == player1Spawn)
            P1.text = objectName + " " + obj.tag;
        else
            P2.text = objectName + " " + obj.tag;

        return (RPS)choice;
    }

    void DecideWinner()
    {
        if (player1Choice == player2Choice)
        {
            matchText.text = "It's a Tie!";
            return;
        }

        if((player1Choice == RPS.Rock && player2Choice == RPS.Scissors) || 
        (player1Choice == RPS.Paper && player2Choice == RPS.Rock) || 
        (player1Choice == RPS.Scissors && player2Choice == RPS.Paper))
        {
            matchText.text = "Player 1 Wins! ";
        }
        else
        {
            matchText.text = "Player 2 Wins!";
        }
    }
}
