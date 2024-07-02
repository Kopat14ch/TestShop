using Sources.Extensions.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sources.Modules.Chair.Scripts
{
    public class ChairController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [field: SerializeField] public Image MyImage { get; private set; }
        [field: SerializeField] public TMP_Text NameText { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        [SerializeField] private TooltipController _tooltipController;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltipController.Enable();
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(eventData.position);
            
            UpdateToolTipLocalPosition(worldPosition);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltipController.Disable();
        }

        private void UpdateToolTipLocalPosition(Vector2 position)
        {
            Vector3 localPosition = _tooltipController.transform.parent.InverseTransformPoint(position);
            localPosition.z = 0;
            _tooltipController.transform.localPosition = localPosition;
            _tooltipController.transform.SetAsLastSibling();
        }
    }
}