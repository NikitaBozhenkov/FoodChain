using System;
using UnityEngine;

public class AnimalPredator : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Animal"))
        {
            if (other.gameObject.TryGetComponent(out IPrey prey))
            {
                prey.Die();
            }
        }
    }
}