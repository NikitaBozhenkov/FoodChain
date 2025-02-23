using System;
using System.Collections.Generic;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalFactory : PlaceholderFactory<AnimalSettings, Animal>, IDisposable
    {
        private readonly Dictionary<string, List<Animal>> _allAnimals = new();

        public override Animal Create(AnimalSettings settings)
        {
            if (!_allAnimals.TryGetValue(settings.Name, out var animals))
            {
                _allAnimals.Add(settings.Name, animals = new List<Animal>());
            }

            var animal = animals.Find(a => !a.gameObject.activeSelf);

            if (animal == null)
            {
                animal = base.Create(settings);
                animal.OnEaten += ReturnToPool;
                animals.Add(animal);
            }
            else
            {
                GetFromPool(animal);
            }
        
            return animal;
        }

        private void GetFromPool(Animal animal)
        {
            animal.gameObject.SetActive(true);
        }

        private void ReturnToPool(Animal animal)
        {
            animal.gameObject.SetActive(false);
        }

        public void Dispose()
        {
            foreach (var animals in _allAnimals.Values)
            {
                foreach (var animal in animals)
                {
                    if (animal)
                    {
                        animal.OnEaten -= ReturnToPool;
                    }   
                }
                
                animals.Clear();
            }
            
            _allAnimals.Clear();
        }
    }
}