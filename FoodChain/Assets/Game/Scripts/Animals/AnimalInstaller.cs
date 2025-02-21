using System;
using System.Collections.Generic;
using Game.Scripts.Common;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalInstaller : Installer<AnimalSettings, AnimalInstaller>
    {
        [Inject] private AnimalSettings _settings;
        [Inject] private AnimalDatabase _animalDatabase;

        public override void InstallBindings()
        {
            var animal = Container.InstantiatePrefab(_animalDatabase.DefaultAnimalPrefab);
            animal.name = _settings.Name;
            Container.InstantiatePrefab(_settings.Model, animal.transform);

            switch (_settings.FoodChainPosition)
            {
                case FoodChainPosition.Prey:
                    Container.Bind<EatableAnimal>().To<AnimalPrey>().FromNewComponentOn(animal).AsSingle().NonLazy();
                    Container
                        .Bind<IAnimalCollisionAction>()
                        .To<AnimalCollisionBounce>()
                        .AsSingle()
                        .WithArguments(7f, new List<string> { TagsManager.Animal })
                        .NonLazy();
                    break;
                case FoodChainPosition.Predator:
                    Container.Bind(typeof(EatableAnimal), typeof(AnimalPredator)).To<AnimalPredator>()
                        .FromNewComponentOn(animal).AsSingle()
                        .NonLazy();
                    Container
                        .Bind<IAnimalCollisionAction>()
                        .To<AnimalCollisionEat>()
                        .AsSingle()
                        .NonLazy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Container.Bind<AnimalMove>().FromNewComponentOn(animal).AsSingle().NonLazy();
            Container.Bind<IMoveStrategy>()
                .To<AnimalMoveLinear>()
                .WithArguments(_settings.Speed)
                .WhenInjectedInto<AnimalMove>();

            Container.Bind<AnimalCollision>().FromNewComponentOn(animal).AsSingle().NonLazy();
            Container.Bind<IAnimalCollisionAction>()
                .To<AnimalCollisionRicochet>()
                .AsSingle()
                .WithArguments((LayerMask)LayersManager.AnimalLayerMask)
                .NonLazy();

            Container.Bind<Rigidbody>().FromComponentOn(animal).AsSingle().NonLazy();
            Container.Bind<Transform>().FromComponentOn(animal).AsSingle().NonLazy();
        }
    }
}