using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Animals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Spawner
{
    public class AnimalsSpawner : MonoBehaviour
    {
        [SerializeReference] private List<EatableAnimal> animalPrefabs;
        [SerializeField] private Transform spawnParent;
        [SerializeField] private float minTickTime;
        [SerializeField] private float maxTickTime;

        private Coroutine _spawnCoroutine;
        private int _spawnedAnimalsCount;
        private AnimalFactory _animalFactory;
        
        [Inject]
        private void Construct(AnimalFactory animalFactory)
        {
            _animalFactory = animalFactory;
        }
    
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
            var animal = _animalFactory.Create();
            animal.Id = _spawnedAnimalsCount++;
        }
    }
}
