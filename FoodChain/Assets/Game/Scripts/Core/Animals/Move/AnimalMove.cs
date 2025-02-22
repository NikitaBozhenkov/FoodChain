using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalMove : MonoBehaviour
    {
        private IMoveStrategy _strategy;

        [Inject]
        protected void Construct(IMoveStrategy strategy)
        {
            _strategy = strategy;
            _strategy.SetDirection(CalculateRandomDirection());
        }

        private void Start()
        {
            _strategy.StartMove();
        }

        private void OnDestroy()
        {
            _strategy.StopMove();
        }

        public async UniTaskVoid DisableMovement(float duration)
        {
            _strategy.StopMove();
            if (await UniTask.WaitForSeconds(duration, cancellationToken: destroyCancellationToken)
                    .SuppressCancellationThrow()) return;
            _strategy.StartMove();
        }
        
        private Vector3 CalculateRandomDirection()
        {
            var randomPoint = Random.insideUnitCircle.normalized;
            return new Vector3(randomPoint.x, 0, randomPoint.y);
        }
    }
}