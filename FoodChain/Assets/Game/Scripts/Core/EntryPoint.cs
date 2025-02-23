using Game.Scripts.Animals;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private AnimalsSpawner _animalsSpawner;

    private Coroutine _spawnCoroutine;
    
    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(_animalsSpawner.SpawnAnimalsRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }
}
