using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public interface IAnimalCollisionAction
    {
        void OnCollision(Collision other);
    }

    public class AnimalCollision: MonoBehaviour
    {
        private readonly List<IAnimalCollisionAction> _collisions = new();

        public void AddCollision(IAnimalCollisionAction action)
        {
            _collisions.Add(action);
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