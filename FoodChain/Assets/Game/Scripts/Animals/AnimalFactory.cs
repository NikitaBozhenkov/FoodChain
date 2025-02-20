using System.Collections.Generic;
using Game.Scripts.Common;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalFactory: IFactory<EatableAnimal>
    {
        private readonly DiContainer _container;
        private readonly AnimalDatabase _animalDatabase;

        public AnimalFactory(DiContainer container, AnimalDatabase animalDatabase)
        {
            _container = container;
            _animalDatabase = animalDatabase;
        }

        public EatableAnimal Create()
        {
            var animalSettings = GetRandomAnimalSettings();
            var animal = _container.InstantiatePrefab(_animalDatabase.DefaultAnimalPrefab);
            animal.name = animalSettings.Name;
            _container.InstantiatePrefab(animalSettings.Model, animal.transform);
            
            _container
                .InstantiateComponent<AnimalMoveLinear>(animal)
                .SetSpeed(animalSettings.Speed);
            
            EatableAnimal eatableAnimal;
            if (animalSettings.FoodChainPosition == FoodChainPosition.Predator)
            {
                eatableAnimal = _container.InstantiateComponent<AnimalPredator>(animal);
                _container.InstantiateComponent<AnimalCollisionEat>(animal);
            }
            else
            {
                eatableAnimal = _container.InstantiateComponent<AnimalPrey>(animal);
                _container.InstantiateComponent<AnimalCollisionBounce>(animal)
                    .SetBounceForce(7)
                    .AddTag(TagsManager.Animal);
            }
            
            return eatableAnimal;
            
            AnimalSettings GetRandomAnimalSettings() => 
                _animalDatabase.AnimalSettingsList[Random.Range(0, _animalDatabase.AnimalSettingsList.Count)];
        }
    }
}