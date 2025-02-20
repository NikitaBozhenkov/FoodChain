using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Animals
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
        private SignalBus _signalBus;
        
        [Inject]
        private void Construct(AnimalFactory animalFactory, SignalBus signalBus)
        {
            _animalFactory = animalFactory;
            _signalBus = signalBus;
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
            _signalBus.TryFire<AnimalSpawnedSignal>();
        }
    }
}
