namespace Game.Scripts.Animals
{
    public interface IMoveStrategy
    {
        void ApplySettings(AnimalSettings animalSettings);
        void StartMove();
        void StopMove();
    }
}