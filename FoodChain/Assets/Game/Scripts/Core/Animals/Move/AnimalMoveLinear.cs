using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public interface IMoveStrategy
    {
        void SetDirection(Vector3 direction);
        void StartMove();
        void StopMove();
    }

    public class AnimalMoveLinear : IMoveStrategy
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly float _speed;

        private CancellationTokenSource _moveCancellationTokenSource;

        public AnimalMoveLinear(Transform transform, Rigidbody rigidbody, float speed)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void SetDirection(Vector3 direction)
        {
            _transform.forward = direction;
        }

        public void StartMove()
        {
            _moveCancellationTokenSource?.Cancel();
            _moveCancellationTokenSource = new CancellationTokenSource();
            Move(_moveCancellationTokenSource.Token).Forget();
        }

        public void StopMove()
        {
            _moveCancellationTokenSource?.Cancel();
            _moveCancellationTokenSource = null;
        }

        private async UniTask Move(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                SetVelocity(_transform.forward * _speed);
                await UniTask.WaitForFixedUpdate();
            }

            return;

            void SetVelocity(Vector3 amount) => _rigidbody.velocity = amount;
        }
    }
}