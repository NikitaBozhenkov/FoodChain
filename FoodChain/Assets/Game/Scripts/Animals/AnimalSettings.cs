using UnityEngine;

namespace Game.Scripts.Animals
{
    [CreateAssetMenu(fileName = "AnimalSettings", menuName = "AnimalSettings")]
    public class AnimalSettings: ScriptableObject
    {
        public string Name;
        public GameObject Model;
        public FoodChainPosition FoodChainPosition;
        public float Speed;
    }
}