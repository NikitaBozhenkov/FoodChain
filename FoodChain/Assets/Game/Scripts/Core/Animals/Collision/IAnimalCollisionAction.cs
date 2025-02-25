using UnityEngine;

namespace Game.Scripts.Animals
{
    public interface IAnimalCollisionAction
    {
        void OnCollisionEnter(Collision other);
    }
}