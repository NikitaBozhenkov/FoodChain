using UnityEngine;

namespace Game.Scripts.Animals
{
    public interface IMoveStrategy
    {
        void ApplySettings(AnimalSettings animalSettings);
        void SetDirection(Vector3 direction);
        void StartMove();
        void StopMove();
    }
}