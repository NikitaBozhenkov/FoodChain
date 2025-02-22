using Game.Scripts.Animals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class AnimalEatEffectSpawner: MonoBehaviour
    {
        [SerializeField] private RectTransform _effectsContainer;
        [SerializeField] private AnimalEatEffect _eatEffectPrefab;
        
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<AnimalAteSignal>(OnAnimalEat);
        }

        private void OnAnimalEat(AnimalAteSignal details)
        {
            var effect = Instantiate(_eatEffectPrefab, _effectsContainer);
            
            var viewportPoint =  Camera.main.WorldToViewportPoint(details.Position);
            var effectTransform = effect.GetComponent<RectTransform>();
            effectTransform.anchorMax = viewportPoint;
            effectTransform.anchorMin = viewportPoint;
            
            effect.Play();
        }
    }
}