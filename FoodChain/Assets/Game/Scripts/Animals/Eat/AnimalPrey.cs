using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalPrey : MonoBehaviour, IPrey
    {
        public int Id { get; set; }

        public void GetEaten()
        {
            Destroy(gameObject);
        }
    }
}