using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalMoveLinear : IMoveStrategy
    {
        private readonly Rigidbody _rigidbody;
        private readonly AnimalMoveDirection _moveDirection;

        private float _speed;

        private CancellationTokenSource _moveCancellationTokenSource;

        public AnimalMoveLinear(Rigidbody rigidbody, AnimalMoveDirection moveDirection)
        {
            _rigidbody = rigidbody;
            _moveDirection = moveDirection;
        }

        public void ApplySettings(AnimalSettings animalSettings)
        {
            _speed = animalSettings.Speed;
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
                SetVelocity(_moveDirection.GlobalDirection * _speed);
                await UniTask.WaitForFixedUpdate();
            }

            return;

            void SetVelocity(Vector3 amount) => _rigidbody.velocity = amount;
        }
    }
}