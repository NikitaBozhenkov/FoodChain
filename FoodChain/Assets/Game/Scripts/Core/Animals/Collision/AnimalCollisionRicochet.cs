using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionRicochet: IAnimalCollisionAction
    {
        private readonly LayerMask _ricochetFrom;
        private readonly Transform _transform;
        private readonly AnimalMoveDirection _moveDirection;

        public AnimalCollisionRicochet(LayerMask layerMask, Transform transform, AnimalMoveDirection moveDirection)
        {
            _ricochetFrom = layerMask;
            _transform = transform;
            _moveDirection = moveDirection;
        }

        public void OnCollision(Collision other)
        {
            if (((1 << other.gameObject.layer) & _ricochetFrom.value) == 0) return;
            
            var contactPoint = other.contacts[0].normal;
            var oldMoveDirection = _moveDirection.GlobalDirection;
            var newMoveDirection = Vector3.Reflect(oldMoveDirection, contactPoint);
            _transform.Rotate(Vector3.up, Vector3.SignedAngle(oldMoveDirection, newMoveDirection, Vector3.up));
        }
    }
}