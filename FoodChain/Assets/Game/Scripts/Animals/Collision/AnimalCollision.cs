using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public interface IAnimalCollisionAction
    {
        void OnCollision(Collision other);
    }

    public class AnimalCollision: MonoBehaviour
    {
        private List<IAnimalCollisionAction> _collisions = new();

        [Inject]
        private void Construct(List<IAnimalCollisionAction> collisionActions)
        {
            _collisions = new List<IAnimalCollisionAction>(collisionActions);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            foreach (var collision in _collisions)
            {
                collision.OnCollision(other);
            }
        }
    }
}