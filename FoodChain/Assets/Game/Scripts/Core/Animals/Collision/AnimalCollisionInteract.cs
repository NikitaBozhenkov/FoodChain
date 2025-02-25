using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionInteract : IAnimalCollisionAction
    {
        private readonly Animal _animal;

        public AnimalCollisionInteract(AnimalPredator animal)
        {
            _animal = animal;
        }
        
        public void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out Animal animal))
            {
                _animal.Interact(animal);
            }
        }
    }
}