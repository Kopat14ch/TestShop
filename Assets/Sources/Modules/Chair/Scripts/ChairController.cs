using System;
using Cysharp.Threading.Tasks;
using Sources.Extensions.Scripts;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Drag.Scripts;
using Sources.Modules.Purchases.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.Modules.Chair.Scripts
{
    [RequireComponent(typeof(DragController))]
    public class ChairController : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler, IBeginDragHandler, IEndDragHandler
    {
        [field: SerializeField] public Image MyImage { get; private set; }
        [field: SerializeField] public TMP_Text NameText { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public int Price { get; private set; }

        [SerializeField] private TooltipController _tooltipController;
        
        private Transform _tooltipTempParent;
        private DragController _drag;
        
        public ChairData Data { get; private set; }

        public void Init(Transform tooltipTempParent, ChairData data)
        {
            Data = data;
            _tooltipTempParent = tooltipTempParent;
            _tooltipController.Init(Data);
            _drag = GetComponent<DragController>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DisableToolTip();
            
            if (_drag.GetDragging())
                return;
            
            _tooltipController.Enable();
            _tooltipController.SetNewParent(_tooltipTempParent);
        }
        
        public void OnPointerMove(PointerEventData eventData)
        {
            if (_drag.GetDragging())
                return;

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
            if (_drag.GetDragging())
                return;
            
            DisableToolTip();
        }
        
        private void UpdateToolTipLocalPosition(Vector2 position)
        {
            _tooltipController.transform.localPosition = position;
        }

        private void DisableToolTip()
        {
            _tooltipController.Disable();
            _tooltipController.SetDefaultParent();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            DisableToolTip();
        }

        public async void OnEndDrag(PointerEventData eventData)
        {
            await UniTask.WaitUntil(() => _drag.GetDragging());
            
            DisableToolTip();
        }
    }
}