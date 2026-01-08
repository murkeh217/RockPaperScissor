using UnityEngine;

[CreateAssetMenu(fileName = "Paper", menuName = "Scriptable Objects/Paper")]
public class Paper : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;

    public GameObject[] Prefabs => prefabs;
}
