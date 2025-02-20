using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Animals
{
    [CreateAssetMenu(fileName = "AnimalDatabase", menuName = "AnimalDatabase")]
    public class AnimalDatabase: ScriptableObject
    {
        public List<EatableAnimal> Animals;
    }
}