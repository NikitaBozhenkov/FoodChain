using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalPredator : EatableAnimal
    {
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Animal")) return;
        
            if (other.gameObject.TryGetComponent(out EatableAnimal animal) && CanEat(animal))
            {
                Eat(animal);
            }

            return;

            bool CanEat(EatableAnimal eatableAnimal)
            {
                return eatableAnimal switch
                {
                    AnimalPrey => true,
                    AnimalPredator predator => Id > predator.Id,
                    _ => false
                };
            }
        }

        private void Eat(EatableAnimal animal)
        {
            animal.GetEaten();
        }

        public override void GetEaten()
        {
            Destroy(gameObject);
        }
    }
}