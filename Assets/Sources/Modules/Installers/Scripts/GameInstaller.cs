using Sources.Modules.Chair.Scripts;
using Sources.Modules.Shop.Scripts;
using Sources.Modules.Shop.Scripts.Api;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Installers.Scripts
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ChairController _chairPrefab;
        [SerializeField] private ShopContainer _shopContainer;
        [SerializeField] private string _charisApiUrl;
        [SerializeField] private string _basePath;

        public override void InstallBindings()
        {
            BindShop();
        }
        
        private void BindShop()
        {
            ChairsApiLoader chairsApiLoader = new ChairsApiLoader(_charisApiUrl);

            Container.Bind<ChairsApiLoader>().FromInstance(chairsApiLoader).AsSingle().NonLazy();
            
            ShopFactory shopFactory = new ShopFactory(chairsApiLoader,
                _chairPrefab,
                _shopContainer,
                Container,
                _basePath);

            ShopService shopService = new ShopService(shopFactory);

            Container
                .BindInterfacesTo<ShopService>()
                .FromInstance(shopService)
                .AsSingle()
                .NonLazy();
        }
    }
}