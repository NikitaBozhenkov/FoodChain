using UnityEngine;

namespace Game.Scripts.Animals
{
    [RequireComponent(typeof(AnimalPredator))]
    public class AnimalCollisionEat: MonoBehaviour
    {
        private AnimalPredator _animalPredator;
        
        private void Awake()
        {
            _animalPredator = GetComponent<AnimalPredator>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Animal")) return;
        
            if (other.gameObject.TryGetComponent(out EatableAnimal animal) && CanEat(animal))
            {
                _animalPredator.Eat(animal);
            }

            return;

            bool CanEat(EatableAnimal eatableAnimal)
            {
                return eatableAnimal switch
                {
                    AnimalPrey => true,
                    AnimalPredator predator => _animalPredator.Id > predator.Id,
                    _ => false
                };
            }
        }
    }
}