using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public abstract class Animal: MonoBehaviour
    {
        public int Id { get; set; }
        protected SignalBus SignalBus;
        
        public event Action<Animal> OnEaten;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
        
        public abstract void Interact(Animal animal);

        public void GetEaten()
        {
            SignalBus.TryFire<AnimalGotEatenSignal>();
            OnEaten?.Invoke(this);
        }
    }
}