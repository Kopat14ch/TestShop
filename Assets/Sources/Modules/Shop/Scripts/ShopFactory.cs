using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Modules.Chair.Scripts;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Shop.Scripts.Api;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Shop.Scripts
{
    public class ShopFactory
    {
        private readonly ChairsApiLoader _chairsApiLoader;
        private readonly ChairController _prefab;
        private readonly ShopContainer _container;
        private readonly DiContainer _diContainer;
        private readonly string _basePath;

        public ShopFactory(ChairsApiLoader chairsApiLoader, 
            ChairController prefab,
            ShopContainer container,
            DiContainer diContainer, string basePath)
        {
            _chairsApiLoader = chairsApiLoader;
            _prefab = prefab;
            _container = container;
            _diContainer = diContainer;
            _basePath = basePath;
        }

        private async UniTask<List<ChairData>> GetChairsAsync()
        {
            List<ChairData> chairsData = await _chairsApiLoader.GetChairsAsync();

            return chairsData;
        }

        public async UniTask<List<ChairController>> Create()
        {
            List<ChairData> chairsData = await GetChairsAsync();
            List<ChairController> chairsController = new List<ChairController>();

            foreach (var chairData in chairsData)
            {
                ChairController chairController = Object.Instantiate(_prefab, _container.transform);
                chairsController.Add(chairController);

                ChairService chairService = new ChairService(chairController, chairData,_basePath);
                _diContainer
                    .BindInterfacesTo<ChairService>()
                    .FromInstance(chairService)
                    .AsTransient()
                    .NonLazy();
            }

            return chairsController;
        }
    }
}