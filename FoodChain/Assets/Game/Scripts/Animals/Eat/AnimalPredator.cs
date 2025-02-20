using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalPredator : EatableAnimal
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void Eat(EatableAnimal animal)
        {
            animal.GetEaten();
        }

        public override void GetEaten()
        {
            _signalBus.TryFire<AnimalGotEatenSignal>();
            Destroy(gameObject);
        }
    }
}