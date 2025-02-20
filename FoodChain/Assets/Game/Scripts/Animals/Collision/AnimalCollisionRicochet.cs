using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionRicochet: IAnimalCollisionAction
    {
        private LayerMask _ricochetFrom;
        private Transform _transform;

        public AnimalCollisionRicochet(LayerMask layerMask, Transform transform)
        {
            _ricochetFrom = layerMask;
            _transform = transform;
        }

        public void OnCollision(Collision other)
        {
            if (((1 << other.gameObject.layer) & _ricochetFrom.value) == 0) return;
            
            var contactPoint = other.contacts[0].normal;
            _transform.forward = Vector3.Reflect(_transform.forward, contactPoint);
        }
    }
}