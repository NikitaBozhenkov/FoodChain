using UnityEngine;

namespace Game.Scripts.Animals
{
    public interface IMoveStrategy
    {
        void SetDirection(Vector3 direction);
        void StartMove();
        void StopMove();
    }
}