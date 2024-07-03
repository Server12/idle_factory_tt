using Game.Configs;
using Game.Configs.UI;
using Game.Level;
using Game.Saves;
using Game.UI.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game.Scopes
{
    public class ProjectScope : LifetimeScope
    {
        [SerializeField] private UIIconsSO _icons;
        [SerializeField] private GameDataSO _gameData;
        [SerializeField] private UIViewsSO uiViewsSo;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_icons);
            
            builder.Register<UIFactory>(Lifetime.Singleton).As<IUIFactory>().WithParameter(uiViewsSo);

            builder.RegisterEntryPoint<LevelsManager>().As<ILevelsManager>().WithParameter(_gameData.Levels).WithParameter(_gameData.StartScreenScene);

            builder.RegisterEntryPoint<SavesManager>().As<ISavesManager>();
        }
    }
}