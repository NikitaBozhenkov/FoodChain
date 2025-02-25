using System;
using Game.Scripts.Models;
using R3;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI
{
    public class MainSceneStatsView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _animalsEatenLabel;
        [SerializeField] private TextMeshProUGUI _animalsAliveLabel;
        
        private SessionStats _stats;

        [Inject]
        private void Construct(SessionStats stats)
        {
            _stats = stats;
        }

        private void Start()
        {
            _stats.AnimalsAlive.Subscribe(UpdateAnimalsAlive);
            _stats.AnimalsEaten.Subscribe(UpdateAnimalsEaten);
        }

        private void UpdateAnimalsAlive(int newValue)
        {
            _animalsAliveLabel.text = $"Animals alive: {newValue.ToString()}";;
        }

        private void UpdateAnimalsEaten(int newValue)
        {
            _animalsEatenLabel.text = $"Animals eaten: {newValue.ToString()}";
        }
    }
}