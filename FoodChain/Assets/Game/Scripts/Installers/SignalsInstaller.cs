using Game.Scripts.Animals;
using Zenject;

namespace Game.Scripts.Installers
{
    public class SignalsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            BindSignals();
        }

        private void BindSignals()
        {
            Container.DeclareSignal<AnimalGotEatenSignal>().OptionalSubscriber();
            Container.DeclareSignal<AnimalSpawnedSignal>().OptionalSubscriber();
        }
    }
}