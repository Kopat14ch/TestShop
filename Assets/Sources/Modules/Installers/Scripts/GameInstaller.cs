using Sources.Modules.Chair.Scripts;
using Sources.Modules.Preloader.Scripts;
using Sources.Modules.Purchases.Scripts;
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
        [SerializeField] private PreloaderController _preloaderController;
        [SerializeField] private Transform _tooltipTempParent;
        [SerializeField] private PurchasesController _purchasesController;
        [SerializeField] private PurchaseController _purchasePrefab;
        [SerializeField] private PurchasesContainer _purchasesContainer;
        [SerializeField] private string _charisApiUrl;
        [SerializeField] private string _basePath;

        public override void InstallBindings()
        {
            BindShop();
            BindPurchase();
        }
        
        private void BindShop()
        {
            ChairsApiLoader chairsApiLoader = new ChairsApiLoader(_preloaderController,_charisApiUrl);

            Container.Bind<ChairsApiLoader>().FromInstance(chairsApiLoader).AsSingle().NonLazy();
            
            ShopFactory shopFactory = new ShopFactory(chairsApiLoader,
                _chairPrefab,
                _shopContainer,
                _tooltipTempParent,
                _purchasesController,
                _basePath);

            ShopService shopService = new ShopService(shopFactory);

            Container
                .BindInterfacesTo<ShopService>()
                .FromInstance(shopService)
                .AsSingle()
                .NonLazy();
        }

        private void BindPurchase()
        {
            PurchaseFactory factory = new PurchaseFactory(_purchasePrefab,_purchasesContainer);

            Container
                .Bind<PurchaseFactory>()
                .FromInstance(factory)
                .AsSingle()
                .NonLazy();
        }
    }
}