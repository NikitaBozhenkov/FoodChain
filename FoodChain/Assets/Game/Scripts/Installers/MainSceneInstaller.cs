using Game.Scripts.Animals;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MainSceneInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<AnimalFactory>().AsSingle();
        }
    }
}