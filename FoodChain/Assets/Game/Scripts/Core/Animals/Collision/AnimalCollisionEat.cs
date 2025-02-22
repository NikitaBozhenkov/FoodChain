using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionEat : IAnimalCollisionAction
    {
        private readonly Animal _animal;

        public AnimalCollisionEat(AnimalPredator animal)
        {
            _animal = animal;
        }
        
        public void OnCollision(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Animal animal))
            {
                _animal.Interact(animal);
            }
        }
    }
}