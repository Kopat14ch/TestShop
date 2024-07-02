using Sources.Modules.Chair.Scripts.Data;
using Zenject;

namespace Sources.Modules.Purchases.Scripts
{
    public class PurchaseFactory
    {
        private readonly PurchaseController _prefab;
        private readonly PurchasesContainer _container;
        private readonly ChairsSpriteData _chairsSpriteData;
        private readonly DiContainer _diContainer;

        public PurchaseFactory(PurchaseController prefab, PurchasesContainer container,ChairsSpriteData chairsSpriteData , DiContainer diContainer)
        {
            _prefab = prefab;
            _container = container;
            _chairsSpriteData = chairsSpriteData;
            _diContainer = diContainer;
        }

        public PurchaseController Create(ChairData data)
        {
            PurchaseController controller = _diContainer.InstantiatePrefabForComponent<PurchaseController>(_prefab, _container.transform);
            controller.Init(data, _chairsSpriteData);

            return controller;
        }
    }
}
