using System;
using Game.Scripts.Models;
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

            RefreshStats();
        }

        private void OnEnable()
        {
            _stats.StatsUpdated += RefreshStats;
        }
        
        private void OnDisable()
        {
            _stats.StatsUpdated -= RefreshStats;
        }

        private void RefreshStats()
        {
            _animalsEatenLabel.text = $"Animals eaten: {_stats.AnimalsEaten.ToString()}";
            _animalsAliveLabel.text = $"Animals alive: {_stats.AnimalsAlive.ToString()}";;
        }
    }
}