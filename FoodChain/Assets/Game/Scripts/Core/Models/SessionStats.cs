using System;
using Game.Scripts.Animals;
using R3;
using Zenject;

namespace Game.Scripts.Models
{
    public class SessionStats: IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        public ReactiveProperty<int> AnimalsEaten { get; }
        public ReactiveProperty<int> AnimalsAlive { get; }
        
        public SessionStats(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            AnimalsEaten = new ReactiveProperty<int>();
            AnimalsAlive = new ReactiveProperty<int>();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<AnimalGotEatenSignal>(OnAnimalGotEaten);
            _signalBus.Subscribe<AnimalSpawnedSignal>(OnAnimalSpawned);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<AnimalGotEatenSignal>(OnAnimalGotEaten);
            _signalBus.TryUnsubscribe<AnimalSpawnedSignal>(OnAnimalSpawned);
            AnimalsEaten.Dispose();
            AnimalsAlive.Dispose();
        }

        private void OnAnimalGotEaten()
        {
            AnimalsEaten.Value++;
            AnimalsAlive.Value--;
        }

        private void OnAnimalSpawned()
        {
            AnimalsAlive.Value++;   
        }
    }
}