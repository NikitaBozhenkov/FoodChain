using Game.Scripts.Animals;
using Game.Scripts.Models;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MainSceneInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SessionStats>().AsSingle();

            Container.BindFactory<AnimalSettings, Animal, AnimalFactory>()
                .FromSubContainerResolve()
                .ByInstaller<AnimalInstaller>();
        }
    }
}