﻿using Code.Entities.EntitiesTransformation.Calculations;
using Code.Infrastructure.SceneManagement;
using Code.Infrastructure.Services.AssetsManagement;
using Code.Infrastructure.Services.Data;
using Code.Infrastructure.Services.GameFactory;
using Code.Infrastructure.Services.Input;
using UnityEngine;
// using Code.Infrastructure.Services.GameFactory;
using Zenject;

namespace Code.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<GameStateMachineInstaller>()
                .AsSingle();

            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();

            Container.Bind<IAssets>().To<AssetsProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

            Container
                .BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstrapper);
            
            Container
                .Bind<ICoroutineRunner>()
                .To<CoroutineRunnerObject>()
                .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
                .AsSingle();
        }
    }

    public class InfrastructureAssetPath
    {
        // public const string CoroutineRunnerPath = "Infrastructure/CoroutineRunner";
        // public const string HUDRoot = "Infrastructure/HUD Root";
        public const string GameBootstrapper = "Infrastructure/GameBootstrapper";
        public const string CoroutineRunnerPath = "Infrastructure/CoroutineRunner";
    }
}