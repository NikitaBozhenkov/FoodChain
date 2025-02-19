using UnityEngine;

namespace Game.Scripts.Animals
{
    public abstract class EatableAnimal: MonoBehaviour
    {
        public int Id { get; set; }
        public abstract void GetEaten();
    }
}