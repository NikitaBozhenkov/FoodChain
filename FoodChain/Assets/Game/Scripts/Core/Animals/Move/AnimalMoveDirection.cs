using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalMoveDirection
    {
        private readonly Transform _transform;
        private readonly Vector3 _direction;
        
        public Vector3 GlobalDirection => _transform.TransformDirection(_direction);

        public AnimalMoveDirection(Transform transform, AnimalSettings settings)
        {
            _transform = transform;
            _direction = new Vector3(settings.MoveDirection.x, 0, settings.MoveDirection.y).normalized;
        }
    }
}