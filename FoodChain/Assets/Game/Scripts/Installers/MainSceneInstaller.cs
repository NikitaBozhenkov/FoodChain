using Game.Scripts.Animals;
using Game.Scripts.Models;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MainSceneInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnimalFactory>().AsSingle();
            Container.Bind<SessionStats>().AsSingle();
        }
    }
}