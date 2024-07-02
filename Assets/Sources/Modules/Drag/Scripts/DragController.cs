using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Drag.Scripts
{
    [RequireComponent(typeof(RectTransform),typeof(LayoutElement), typeof(CanvasGroup))]
    public class DragController : MonoBehaviour , IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private RectTransform _rectTransform;
        private LayoutElement _layoutElement;
        private Canvas _baseCanvas;
        private CanvasGroup _canvasGroup;
        private Transform _originalParent;
        private int _originalSiblingIndex;

        private static bool s_isDragging;

        [Inject]
        public void Construct(Canvas baseCanvas)
        {
            _baseCanvas = baseCanvas;
        }
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _layoutElement = GetComponent<LayoutElement>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            s_isDragging = true;
            _originalParent = _rectTransform.parent;
            _originalSiblingIndex = _rectTransform.GetSiblingIndex();
            
            _rectTransform.SetParent(_baseCanvas.transform, true);
            _rectTransform.SetAsLastSibling();
            
            _layoutElement.ignoreLayout = true;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _layoutElement.ignoreLayout = true;
            _rectTransform.anchoredPosition += eventData.delta / _baseCanvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _layoutElement.ignoreLayout = false;
            s_isDragging = false;
            _canvasGroup.blocksRaycasts = true;
            
            _rectTransform.SetParent(_originalParent, true);
            _rectTransform.SetSiblingIndex(_originalSiblingIndex);
        }

        public bool GetDragging() => s_isDragging;
    }
}
