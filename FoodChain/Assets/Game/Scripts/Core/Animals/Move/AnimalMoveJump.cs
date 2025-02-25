using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public class AnimalMoveJump : IMoveStrategy
    {
        private readonly Transform _transform;
        private readonly Rigidbody _rigidbody;

        private float _jumpForce;
        private float _jumpInterval;
        
        private CancellationTokenSource _moveCancellationTokenSource;
        
        public AnimalMoveJump(Transform transform, Rigidbody rigidbody)
        {
            _transform = transform;
            _rigidbody = rigidbody;
        }

        public void ApplySettings(AnimalSettings animalSettings)
        {
            _jumpForce = animalSettings.JumpForce;
            _jumpInterval = animalSettings.JumpInterval;
        }

        public void SetDirection(Vector3 direction)
        {
            _transform.forward = direction;
        }

        public void StartMove()
        {
            _moveCancellationTokenSource?.Cancel();
            _moveCancellationTokenSource = new CancellationTokenSource();
            JumpLoop(_moveCancellationTokenSource.Token).Forget();
        }

        public void StopMove()
        {
            _moveCancellationTokenSource?.Cancel();
            _moveCancellationTokenSource = null;
        }

        private async UniTask JumpLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Jump();
                await UniTask.WaitForSeconds(_jumpInterval, cancellationToken: cancellationToken);
            }
        }

        private void Jump()
        {
            _rigidbody.velocity = _transform.forward * _jumpForce + Vector3.up * (_jumpForce / 2);
        }
    }
}