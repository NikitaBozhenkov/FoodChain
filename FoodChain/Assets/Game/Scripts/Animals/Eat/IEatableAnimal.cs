namespace Game.Scripts.Animals
{
    public interface IEatableAnimal
    {
        public int Id { get; set; }
        void GetEaten();
    }
}