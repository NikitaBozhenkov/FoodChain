using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Animals
{
    public abstract class AnimalMove : MonoBehaviour
    {
        protected float Speed;
        
        private CancellationTokenSource _moveCancellationTokenSource;

        public void SetSpeed(float value)
        {
            Speed = value;
        }
        
        private void OnEnable()
        {
            StartMove();
        }

        private void OnDisable()
        {
            StopMove();
        }
        
        public void StartMove()
        {
            _moveCancellationTokenSource?.Cancel();
            _moveCancellationTokenSource = new CancellationTokenSource();
            Move(_moveCancellationTokenSource.Token).SuppressCancellationThrow();
        }

        public void StopMove()
        {
            _moveCancellationTokenSource?.Cancel();
        }

        protected virtual async UniTask Move(CancellationToken cancellationToken)
        {
            await UniTask.CompletedTask;
        }
    }
}