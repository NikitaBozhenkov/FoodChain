using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalPredator : MonoBehaviour, IPredator
    {
        public int Id { get; set; }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Animal")) return;
        
            if (other.gameObject.TryGetComponent(out IEatableAnimal animal) && CanEat(animal))
            {
                Eat(animal);
            }

            return;

            bool CanEat(IEatableAnimal eatableAnimal)
            {
                return eatableAnimal switch
                {
                    IPrey => true,
                    IPredator predator => Id > predator.Id,
                    _ => false
                };
            }
        }

        public void Eat(IEatableAnimal animal)
        {
            animal.GetEaten();
        }

        public void GetEaten()
        {
            Destroy(gameObject);
        }
    }
}