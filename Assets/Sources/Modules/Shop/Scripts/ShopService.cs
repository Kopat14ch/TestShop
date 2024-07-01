using Zenject;

namespace Sources.Modules.Shop.Scripts
{
    public class ShopService : IInitializable
    {
        private readonly ShopContainer _container;
        private readonly ShopFactory _factory;

        public ShopService(ShopFactory factory)
        {
            _factory = factory;
        }
        
        public async void Initialize()
        {
           await _factory.Create();
        }
    }
}