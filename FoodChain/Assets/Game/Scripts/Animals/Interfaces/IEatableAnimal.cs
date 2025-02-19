namespace Game.Scripts.Animals.Interfaces
{
    public interface IEatableAnimal
    {
        public int Id { get; set; }
        void GetEaten();
    }
}