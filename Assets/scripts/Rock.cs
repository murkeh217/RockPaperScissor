using UnityEngine;

[CreateAssetMenu(fileName = "Rock", menuName = "Scriptable Objects/Rock")]
public class Rock : ScriptableObject
{
    [SerializeField] private GameObject[] prefabs;

    public GameObject[] Prefabs => prefabs;
}
