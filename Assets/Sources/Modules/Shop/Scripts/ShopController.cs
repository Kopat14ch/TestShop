using Sources.Modules.Purchases.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Modules.Shop.Scripts
{
    public class ShopController : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent(out PurchaseController chairController))
                chairController.RemoveAll();
        }
    }
}