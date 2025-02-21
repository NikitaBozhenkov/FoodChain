using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalMoveJump : IMoveStrategy
    {
        private Rigidbody _rigidbody;
        private Transform _transform;
        private float _jumpForce;
        private float _jumpInterval;
        private CancellationTokenSource _moveCancellationTokenSource;

        [Inject]
        protected void Construct(Transform transform, Rigidbody rigidbody, float jumpForce, float jumpInterval)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _jumpForce = jumpForce;
            _jumpInterval = jumpInterval;
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