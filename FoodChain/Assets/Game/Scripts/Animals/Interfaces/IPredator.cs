namespace Game.Scripts.Animals.Interfaces
{
    public interface IPredator : IEatableAnimal
    {
        void Eat(IEatableAnimal animal);
    }
}