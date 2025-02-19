namespace Game.Scripts.Animals
{
    public class AnimalPrey : EatableAnimal
    {
        public override void GetEaten()
        {
            Destroy(gameObject);
        }
    }
}