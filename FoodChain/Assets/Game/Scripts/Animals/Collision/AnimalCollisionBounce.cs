using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalCollisionBounce : MonoBehaviour
    {
        private float _bounceForce;
        private readonly List<string> bounceTags = new();
        private Rigidbody _rigidbody;
        private AnimalMove _animalMove;

        public AnimalCollisionBounce SetBounceForce(float value)
        {
            _bounceForce = value;
            return this;
        }
        
        public AnimalCollisionBounce AddTag(string bounceFrom)
        {
            bounceTags.Add(bounceFrom);
            return this;
        }
        
        private void Awake()
        {
            _animalMove = GetComponent<AnimalMove>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!bounceTags.Contains(other.gameObject.tag)) return;

            Bounce(other);
            DisableMovement(destroyCancellationToken).Forget();
        }

        private void Bounce(Collision other)
        {
            var bounceDirection = (transform.position - other.transform.position).normalized;

            _rigidbody.AddForce(bounceDirection * _bounceForce, ForceMode.Impulse);
        }

        private async UniTaskVoid DisableMovement(CancellationToken cancellationToken)
        {
            _animalMove.StopMove();
            await UniTask.WaitForSeconds(.3f, cancellationToken: cancellationToken);
            _animalMove.StartMove();
        }
    }
}