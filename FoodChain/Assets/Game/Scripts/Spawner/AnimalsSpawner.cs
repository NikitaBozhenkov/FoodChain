using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Animals.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Spawner
{
    public class AnimalsSpawner : MonoBehaviour
    {
        [SerializeReference] private List<GameObject> animalPrefabs;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private float minTickTime;
        [SerializeField] private float maxTickTime;

        private Coroutine _spawnCoroutine;
        private int _spawnedAnimalsCount;
    
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
            var animal = Instantiate(animalPrefab, Vector3.zero, Quaternion.identity, spawnParent);
        
            if (animal.TryGetComponent(out IEatableAnimal eatableAnimal))
            {
                eatableAnimal.Id = _spawnedAnimalsCount;
            }
        
            ++_spawnedAnimalsCount;
        }
    }
}
