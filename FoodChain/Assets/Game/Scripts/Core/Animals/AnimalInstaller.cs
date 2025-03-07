using System;
using System.Collections.Generic;
using Game.Scripts.Common;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Animals
{
    public class AnimalInstaller : MonoInstaller
    {
        [Inject] private AnimalSettings _settings;
        [Inject] private AnimalDatabase _animalDatabase;
        
        private GameObject _animal;

        public override void InstallBindings()
        {
            Container.BindInstance(_settings).AsSingle().NonLazy();
            
            _animal = gameObject;
            _animal.name = _settings.Name;
            Container.InstantiatePrefab(_settings.Model, _animal.transform);

            BindFoodChainPosition();
            BindMovement();
            BindCollision();
            
            Container.Bind<Rigidbody>().FromComponentOn(_animal).AsSingle().NonLazy();
            Container.Bind<Transform>().FromComponentOn(_animal).AsSingle().NonLazy();
        }

        private void BindFoodChainPosition()
        {
            switch (_settings.FoodChainPosition)
            {
                case FoodChainPosition.Prey:
                    Container.Bind<Animal>().To<AnimalPrey>().FromNewComponentOn(_animal).AsSingle().NonLazy();
                    break;
                case FoodChainPosition.Predator:
                    Container.Bind(typeof(Animal), typeof(AnimalPredator)).To<AnimalPredator>()
                        .FromNewComponentOn(_animal).AsSingle()
                        .NonLazy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BindMovement()
        {
            Container.Bind<AnimalMoveDirection>().AsSingle().NonLazy();
            Container.Bind<AnimalMove>().FromComponentOn(_animal).AsSingle().NonLazy();
            switch (_settings.AnimalMoveType)
            {
                case AnimalMoveType.Linear:
                    Container.Bind<IMoveStrategy>()
                        .To<AnimalMoveLinear>()
                        .WhenInjectedInto<AnimalMove>();
                    break;
                case AnimalMoveType.Jump:
                    Container.Bind<IMoveStrategy>()
                        .To<AnimalMoveJump>()
                        .WhenInjectedInto<AnimalMove>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BindCollision()
        {
            switch (_settings.FoodChainPosition)
            {
                case FoodChainPosition.Prey:
                    Container
                        .Bind<IAnimalCollisionAction>()
                        .To<AnimalCollisionBounce>()
                        .AsSingle()
                        .WithArguments(7f, new List<string> { TagsManager.Animal })
                        .NonLazy();
                    break;
                case FoodChainPosition.Predator:
                    Container
                        .Bind<IAnimalCollisionAction>()
                        .To<AnimalCollisionInteract>()
                        .AsSingle()
                        .NonLazy();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            Container.Bind<IAnimalCollisionAction>()
                .To<AnimalCollisionRicochet>()
                .AsSingle()
                .WithArguments((LayerMask)LayersManager.AnimalLayerMask)
                .NonLazy();
            
            Container.Bind<AnimalCollision>().FromComponentOn(_animal).AsSingle().NonLazy();
        }
    }
}