using Game.Scripts.Animals;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ConfigsInstaller: MonoInstaller
    {
        [SerializeField] private AnimalDatabase _animalDatabase;

        public override void InstallBindings()
        {
            Container.Bind<AnimalDatabase>().FromInstance(_animalDatabase).AsSingle();
        }
    }
}