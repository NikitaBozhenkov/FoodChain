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

            var collision = _container.InstantiateComponent<AnimalCollision>(animal);
            var ricochetCollision = new AnimalCollisionRicochet(LayersManager.AnimalLayerMask, animal.transform);
            collision.AddCollision(ricochetCollision);
            
            var move = _container.InstantiateComponent<AnimalMoveLinear>(animal);
            move.SetSpeed(animalSettings.Speed);
            
            EatableAnimal eatableAnimal;
            if (animalSettings.FoodChainPosition == FoodChainPosition.Predator)
            {
                eatableAnimal = _container.InstantiateComponent<AnimalPredator>(animal);
                var collisionEat = new AnimalCollisionEat((AnimalPredator)eatableAnimal);
                collision.AddCollision(collisionEat);
            }
            else
            {
                eatableAnimal = _container.InstantiateComponent<AnimalPrey>(animal);
                var collisionBounce = new AnimalCollisionBounce(
                    7, 
                    new List<string> { TagsManager.Animal },
                    animal.GetComponent<Rigidbody>(), 
                    move);
                collision.AddCollision(collisionBounce);
            }
            
            return eatableAnimal;
            
            AnimalSettings GetRandomAnimalSettings() => 
                _animalDatabase.AnimalSettingsList[Random.Range(0, _animalDatabase.AnimalSettingsList.Count)];
        }
    }
}