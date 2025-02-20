using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalFactory: IFactory<EatableAnimal>
    {
        private readonly DiContainer _container;
        private readonly List<EatableAnimal> _animals;
        
        public AnimalFactory(DiContainer container, AnimalDatabase animalDatabase)
        {
            _container = container;
            _animals = animalDatabase.Animals;
        }

        public EatableAnimal Create()
        {
            var animalPrefab = _animals[Random.Range(0, _animals.Count)];
            return _container.InstantiatePrefabForComponent<EatableAnimal>(animalPrefab);
        }
    }
}