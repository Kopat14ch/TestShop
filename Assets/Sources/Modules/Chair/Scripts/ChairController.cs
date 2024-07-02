using System;
using Sources.Extensions.Scripts;
using Sources.Modules.Chair.Scripts.Data;
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
        
        private Transform _tooltipTempParent;

        public void Init(Transform tooltipTempParent, ChairData data)
        {
            _tooltipTempParent = tooltipTempParent;
            _tooltipController.Init(data);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tooltipController.Enable();
            _tooltipController.SetNewParent(_tooltipTempParent);
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)_tooltipController.transform.parent, 
                eventData.position, 
                eventData.pressEventCamera, 
                out var localPosition
            );
            
            UpdateToolTipLocalPosition(localPosition);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            _tooltipController.Disable();
            _tooltipController.SetDefaultParent();
        }

        private void UpdateToolTipLocalPosition(Vector2 position)
        {
            _tooltipController.transform.localPosition = position;
        }
    }
}