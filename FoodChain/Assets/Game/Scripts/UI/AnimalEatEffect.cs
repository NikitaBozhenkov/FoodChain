using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class AnimalEatEffect: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI effectText;

        public void Play()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMoveY(transform.position.y - 40f, 0.8f).SetEase(Ease.OutQuad));
            sequence.Join(effectText.DOFade(0, 0.8f));
            sequence.OnComplete(() => Destroy(gameObject));
        }
    }
}