using Game.Scripts.Animals;
using Game.Scripts.Models;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MainSceneInstaller: MonoInstaller
    {
        [Inject] private AnimalDatabase _animalDatabase;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SessionStats>().AsSingle();

            Container.BindFactory<AnimalSettings, Animal, AnimalFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab<AnimalInstaller>(_animalDatabase.DefaultAnimalPrefab);
        }
    }
}