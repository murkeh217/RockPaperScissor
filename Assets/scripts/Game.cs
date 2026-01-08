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

    private void Start()
    {
        SpawnForPlayer(player1Spawn);
        SpawnForPlayer(player2Spawn);
    }

    void SpawnForPlayer(Transform playerSpawn)
    {
        // Pick Rock / Paper / Scissors randomly
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

        if (prefab != null)
        {
            GameObject obj = Instantiate(prefab, playerSpawn);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;
            obj.transform.localScale = Vector3.one;
        }
    }
}
