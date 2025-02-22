using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Animals
{
    public class AnimalsSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnParent;
        [SerializeField] private float minTickTime;
        [SerializeField] private float maxTickTime;

        private Coroutine _spawnCoroutine;
        private int _spawnedAnimalsCount;
        private AnimalFactory _animalFactory;
        private SignalBus _signalBus;
        private AnimalDatabase _animalDatabase;

        [Inject]
        private void Construct(
            AnimalFactory animalFactory, 
            AnimalDatabase animalDatabase, 
            SignalBus signalBus)
        {
            _animalFactory = animalFactory;
            _animalDatabase = animalDatabase;
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
            var animal = _animalFactory.Create(GetRandomAnimalSettings());
            animal.Id = _spawnedAnimalsCount++;
            animal.gameObject.name += $"-{animal.Id}";
            _signalBus.TryFire<AnimalSpawnedSignal>();
            return;

            AnimalSettings GetRandomAnimalSettings() => 
                _animalDatabase.AnimalSettingsList[Random.Range(0, _animalDatabase.AnimalSettingsList.Count)];
        }
    }
}
