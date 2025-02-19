namespace Game.Scripts.Animals
{
    public class AnimalPredator : EatableAnimal
    {
        public void Eat(EatableAnimal animal)
        {
            animal.GetEaten();
        }

        public override void GetEaten()
        {
            Destroy(gameObject);
        }
    }
}