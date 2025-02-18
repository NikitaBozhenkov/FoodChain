using UnityEngine;

public class AnimalPrey : MonoBehaviour, IPrey
{
    public void Die()
    {
        Destroy(gameObject);
    }
}