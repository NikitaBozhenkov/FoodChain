using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Animals
{
    public class AnimalMoveLinear : AnimalMove 
    {

        private Rigidbody _rigidbody;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            transform.forward = CalculateRandomDirection();
        }

        private Vector3 CalculateRandomDirection()
        {
            var randomPoint = Random.insideUnitCircle.normalized;
            return new Vector3(randomPoint.x, 0, randomPoint.y);
        }

        protected override async UniTask Move(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                SetVelocity(transform.forward * Speed);
                await UniTask.WaitForFixedUpdate();
            }

            return;

            void SetVelocity(Vector3 amount) => _rigidbody.velocity = amount;
        }
    }
}
