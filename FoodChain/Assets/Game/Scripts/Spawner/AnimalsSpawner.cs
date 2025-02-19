using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalsSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> animalPrefabs;
    [SerializeField] private Transform spawnParent;
    [SerializeField] private float minTickTime;
    [SerializeField] private float maxTickTime;

    private Coroutine _spawnCoroutine;
    
    private void OnEnable()
    {
        _spawnCoroutine = StartCoroutine(SpawnAnimalsRoutine());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawnCoroutine);
    }

    private IEnumerator SpawnAnimalsRoutine()
    {
        while (true)
        {
            SpawnAnimal();
            yield return new WaitForSeconds(Random.Range(minTickTime, maxTickTime));
        }
    }

    private void SpawnAnimal()
    {
        var animalPrefab = animalPrefabs[Random.Range(0, animalPrefabs.Count)];
        Instantiate(animalPrefab, Vector3.zero, Quaternion.identity, spawnParent);
    }
}
