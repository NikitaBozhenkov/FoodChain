using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalGotEatenSignal
    {
    }

    public class AnimalSpawnedSignal
    {
    }

    public class AnimalAteSignal
    {
        public Vector3 Position { get; }

        public AnimalAteSignal(Vector3 position)
        {
            Position = position;
        }
    }
}