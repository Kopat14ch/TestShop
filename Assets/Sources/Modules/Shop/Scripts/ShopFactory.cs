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
        private readonly Transform _tooltipTempParent;
        private readonly string _basePath;

        public ShopFactory(ChairsApiLoader chairsApiLoader, 
            ChairController prefab,
            ShopContainer container,
            Transform tooltipTempParent, 
            string basePath)
        {
            _chairsApiLoader = chairsApiLoader;
            _prefab = prefab;
            _container = container;
            _tooltipTempParent = tooltipTempParent;
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
                chairController.Init(_tooltipTempParent);
                chairsController.Add(chairController);

                ChairService chairService = new ChairService(chairController, chairData,_basePath);
                chairService.Init();
            }

            return chairsController;
        }
    }
}