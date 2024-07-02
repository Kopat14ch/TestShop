using Sources.Modules.Chair.Scripts.Data;
using UnityEngine;

namespace Sources.Modules.Purchases.Scripts
{
    public class PurchaseFactory
    {
        private readonly PurchaseController _prefab;
        private readonly PurchasesContainer _container;

        public PurchaseFactory(PurchaseController prefab ,PurchasesContainer container)
        {
            _prefab = prefab;
            _container = container;
        }

        public PurchaseController Create(ChairData data)
        {
            PurchaseController controller = Object.Instantiate(_prefab, _container.transform);
            controller.Init(data);

            return controller;
        }
    }
}
