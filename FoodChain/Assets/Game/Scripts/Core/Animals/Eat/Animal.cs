using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public abstract class Animal: MonoBehaviour
    {
        public int Id { get; set; }
        protected SignalBus SignalBus;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
        
        public abstract void Interact(Animal animal);

        public void GetEaten()
        {
            SignalBus.TryFire<AnimalGotEatenSignal>();
            Destroy(gameObject);
        }
    }
}