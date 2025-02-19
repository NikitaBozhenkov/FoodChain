using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionRicochet: MonoBehaviour
    {
        [SerializeField] 
        private LayerMask _richochetFrom;
        
        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & _richochetFrom.value) == 0) return;
            
            var contactPoint = other.contacts[0].normal;
            transform.forward = Vector3.Reflect(transform.forward, contactPoint);
        }
    }
}