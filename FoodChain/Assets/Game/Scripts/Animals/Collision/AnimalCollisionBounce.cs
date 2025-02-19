using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Animals
{
    [RequireComponent(typeof(AnimalMove), typeof(Rigidbody))]
    public class AnimalCollisionBounce : MonoBehaviour
    {
        [SerializeField] private LayerMask _bounceFrom;
        [SerializeField] private float _bounceForce;

        private Rigidbody _rigidbody;
        private AnimalMove _animalMove;

        private void Awake()
        {
            _animalMove = GetComponent<AnimalMove>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (((1 << other.gameObject.layer) & _bounceFrom.value) == 0) return;

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