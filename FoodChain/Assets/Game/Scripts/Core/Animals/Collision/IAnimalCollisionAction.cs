using UnityEngine;

namespace Game.Scripts.Animals
{
    public interface IAnimalCollisionAction
    {
        void OnCollision(Collision other);
    }
}