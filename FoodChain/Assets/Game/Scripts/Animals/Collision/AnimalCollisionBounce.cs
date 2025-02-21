using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionBounce : IAnimalCollisionAction
    {
        private readonly float _bounceForce;
        private readonly List<string> _bounceTags;
        private readonly Rigidbody _rigidbody;
        private readonly AnimalMove _animalMove;
        
        public AnimalCollisionBounce(float bounceForce, List<string> bounceTags, Rigidbody rigidbody,
            AnimalMove animalMove)
        {
            _bounceForce = bounceForce;
            _bounceTags = bounceTags;
            _rigidbody = rigidbody;
            _animalMove = animalMove;
        }

        public void OnCollision(Collision other)
        {
            if (!_bounceTags.Contains(other.gameObject.tag)) return;

            Bounce(other);
            _animalMove.DisableMovement(.3f);
        }

        private void Bounce(Collision other)
        {
            var bounceDirection = (_rigidbody.transform.position - other.transform.position).normalized;

            _rigidbody.AddForce(bounceDirection * _bounceForce, ForceMode.Impulse);
        }
    }
}