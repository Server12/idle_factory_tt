using Game.Configs;
using Game.Configs.Map;
using Game.Map;
using Game.Map.Controllers;
using Game.Map.Controllers.Factory;
using Game.Map.Data;
using Game.Map.Models;
using Game.Player.Models;
using Game.Tasks;
using Game.Tasks.Factory;
using Game.UI.Presenters;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scopes
{
    public class LevelScope : LifetimeScope
    {
        [SerializeField] private BuildingViewsSO _buildingViews;
        [SerializeField] private GameDataSO _gameData;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InventoryModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(_gameData.StartUpPlayer.ResourceItems);

            builder.Register<CurrencyBalanceModel>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf()
                .WithParameter(_gameData.StartUpPlayer.CurrencyItems);

            RegisterTasks(builder);

            RegisterUI(builder);

            RegisterMap(builder);
        }

        private void RegisterTasks(IContainerBuilder builder)
        {
            builder.Register<TaskModelsFactory>(Lifetime.Scoped);
            builder.RegisterEntryPoint<TasksPresenter>().As<ITasksPresenter>();
        }

        private void RegisterUI(IContainerBuilder builder)
        {
            builder.UseEntryPoints(Lifetime.Singleton, pointsBuilder =>
            {
                pointsBuilder.Add<ResourceFactoryPopupsPresenter>().As<IPopupPresenter<FactoryBuildingModel>>();
                pointsBuilder.Add<MarketPopupPresenter>().As<IPopupPresenter<IMarketBuildingModel>>();
                pointsBuilder.Add<CraftToolsPopupPresenter>().As<IPopupPresenter<ICraftBuildingModel>>();
                pointsBuilder.Add<MainScreenPresenter>();
            });
        }

        private void RegisterMap(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MapController>().As<IMapController>();

            foreach (var buildingData in _gameData.Buildings)
            {
                if (buildingData is FactoryBuildingData factoryBuildingData)
                {
                    builder.Register<FactoryBuildingModel>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf()
                        .WithParameter(factoryBuildingData);
                }
                else if (buildingData is MarketBuildingData marketBuildingData)
                {
                    builder.Register<MarketBuildingModel>(Lifetime.Scoped).WithParameter(marketBuildingData)
                        .AsImplementedInterfaces().AsSelf();
                }
                else if (buildingData is ToolsBuildingData toolsBuildingData)
                {
                    builder.Register<ToolsFactoryBuildingModel>(Lifetime.Scoped).AsImplementedInterfaces().AsSelf()
                        .WithParameter(toolsBuildingData);
                }
            }


            builder.Register<MapBuildingsFactory>(Lifetime.Scoped).WithParameter(_buildingViews);

            builder.RegisterEntryPoint<ToolsFactoryBuildingPresenter>();
            builder.RegisterEntryPoint<UpgradeableBuildingsPresenter>();
            builder.RegisterEntryPoint<MarketBuildingPresenter>();
        }
    }
}