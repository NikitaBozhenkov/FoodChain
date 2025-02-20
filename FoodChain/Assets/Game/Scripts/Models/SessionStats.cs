using System;
using Game.Scripts.Animals;
using Zenject;

namespace Game.Scripts.Models
{
    public class SessionStats
    {
        public int AnimalsEaten { get; private set; }
        public int AnimalsAlive { get; private set; }
        
        public event Action StatsUpdated;

        public SessionStats(SignalBus signalBus)
        {
            signalBus.Subscribe<AnimalGotEatenSignal>(() =>
            {
                AnimalsEaten++;
                AnimalsAlive--;
                StatsUpdated?.Invoke();
            });
            signalBus.Subscribe<AnimalSpawnedSignal>(() =>
            {
                AnimalsAlive++;
                StatsUpdated?.Invoke();
            });
        }
    }
}