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

        public IEnumerator SpawnAnimalsRoutine()
        {
            while (true)
            {
                SpawnAnimal();
                yield return new WaitForSeconds(Random.Range(minTickTime, maxTickTime));
            }
        }

        private void SpawnAnimal()
        {
            var animalSettings = GetRandomAnimalSettings();
            var animal = _animalFactory.Create(animalSettings);
            animal.transform.SetParent(spawnParent);
            animal.transform.localPosition = Vector3.zero;
            animal.Id = _spawnedAnimalsCount++;
            animal.gameObject.name = $"{animalSettings.Name}-{animal.Id}";
            _signalBus.TryFire<AnimalSpawnedSignal>();
            return;

            AnimalSettings GetRandomAnimalSettings() => 
                _animalDatabase.AnimalSettingsList[Random.Range(0, _animalDatabase.AnimalSettingsList.Count)];
        }
    }
}
