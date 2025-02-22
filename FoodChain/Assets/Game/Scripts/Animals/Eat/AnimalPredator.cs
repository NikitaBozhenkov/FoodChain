namespace Game.Scripts.Animals
{
    public class AnimalPredator : Animal
    {
        public override void Interact(Animal animal)
        {
            if (!CanEat(animal)) return;
            
            animal.GetEaten();
            SignalBus.TryFire( new AnimalAteSignal(transform.position));
        }
        
        private bool CanEat(Animal animal)
        {
            return animal switch
            {
                AnimalPrey => true,
                AnimalPredator predator => Id > predator.Id,
                _ => false
            };
        }
    }
}