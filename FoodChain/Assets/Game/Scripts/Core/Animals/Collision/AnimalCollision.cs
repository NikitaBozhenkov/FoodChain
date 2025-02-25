using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalCollision: MonoBehaviour
    {
        private List<IAnimalCollisionAction> _collisionActions = new();

        [Inject]
        private void Construct(List<IAnimalCollisionAction> collisionActions)
        {
            _collisionActions = new List<IAnimalCollisionAction>(collisionActions);
        }
        
        private void OnCollisionEnter(Collision other)
        {
            foreach (var collision in _collisionActions)
            {
                collision.OnCollisionEnter(other);
            }
        }
    }
}