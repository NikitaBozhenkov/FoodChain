using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalPrey : EatableAnimal
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public override void GetEaten()
        {
            _signalBus.TryFire<AnimalGotEatenSignal>();
            Destroy(gameObject);
        }
    }
}