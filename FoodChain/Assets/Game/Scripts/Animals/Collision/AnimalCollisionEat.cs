using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionEat : IAnimalCollisionAction
    {
        private readonly AnimalPredator _predator;

        public AnimalCollisionEat(AnimalPredator predator)
        {
            _predator = predator;
        }
        
        public void OnCollision(Collision other)
        {
            if (!other.gameObject.CompareTag("Animal")) return;
        
            if (other.gameObject.TryGetComponent(out EatableAnimal animal) && CanEat(animal))
            {
                _predator.Eat(animal);
            }

            return;

            bool CanEat(EatableAnimal eatableAnimal)
            {
                return eatableAnimal switch
                {
                    AnimalPrey => true,
                    AnimalPredator predator => _predator.Id > predator.Id,
                    _ => false
                };
            }
        }
    }
}