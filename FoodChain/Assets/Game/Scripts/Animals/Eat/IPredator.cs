namespace Game.Scripts.Animals
{
    public interface IPredator : IEatableAnimal
    {
        void Eat(IEatableAnimal animal);
    }
}