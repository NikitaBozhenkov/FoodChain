using Game.Scripts.Animals;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AnimalsSpawner _animalsSpawner;

    private void OnEnable()
    {
        _animalsSpawner.StartSpawning();
    }

    private void OnDisable()
    {
        _animalsSpawner.StopSpawning();
    }
}
