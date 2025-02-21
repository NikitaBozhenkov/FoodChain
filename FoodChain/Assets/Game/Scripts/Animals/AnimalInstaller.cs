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
        
        private GameObject _animal;

        public override void InstallBindings()
        {
            _animal = Container.InstantiatePrefab(_animalDatabase.DefaultAnimalPrefab);
            _animal.name = _settings.Name;
            Container.InstantiatePrefab(_settings.Model, _animal.transform);

            switch (_settings.FoodChainPosition)
            {
                case FoodChainPosition.Prey:
                    Container.Bind<EatableAnimal>().To<AnimalPrey>().FromNewComponentOn(_animal).AsSingle().NonLazy();
                    Container
                        .Bind<IAnimalCollisionAction>()
                        .To<AnimalCollisionBounce>()
                        .AsSingle()
                        .WithArguments(7f, new List<string> { TagsManager.Animal })
                        .NonLazy();
                    break;
                case FoodChainPosition.Predator:
                    Container.Bind(typeof(EatableAnimal), typeof(AnimalPredator)).To<AnimalPredator>()
                        .FromNewComponentOn(_animal).AsSingle()
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

            BindMovement();
            
            Container.Bind<IAnimalCollisionAction>()
                .To<AnimalCollisionRicochet>()
                .AsSingle()
                .WithArguments((LayerMask)LayersManager.AnimalLayerMask)
                .NonLazy();

            Container.Bind<AnimalCollision>().FromNewComponentOn(_animal).AsSingle().NonLazy();

            Container.Bind<Rigidbody>().FromComponentOn(_animal).AsSingle().NonLazy();
            Container.Bind<Transform>().FromComponentOn(_animal).AsSingle().NonLazy();
        }

        private void BindMovement()
        {
            Container.Bind<AnimalMove>().FromNewComponentOn(_animal).AsSingle().NonLazy();
            switch (_settings.AnimalMoveType)
            {
                case AnimalMoveType.Linear:
                    Container.Bind<IMoveStrategy>()
                        .To<AnimalMoveLinear>()
                        .WithArguments(_settings.Speed)
                        .WhenInjectedInto<AnimalMove>();
                    break;
                case AnimalMoveType.Jump:
                    Container.Bind<IMoveStrategy>()
                        .To<AnimalMoveJump>()
                        .WithArguments(_settings.JumpForce, _settings.JumpInterval)
                        .WhenInjectedInto<AnimalMove>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}