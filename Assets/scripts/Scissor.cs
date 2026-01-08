using UnityEngine;

[CreateAssetMenu(fileName = "Scissors", menuName = "Scriptable Objects/Scissors")]
public class Scissor : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;

    public GameObject[] Prefabs => prefabs;
}
