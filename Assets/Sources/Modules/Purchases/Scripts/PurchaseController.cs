using System;
using System.Globalization;
using Sources.Extensions.Scripts;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Purchases.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.Modules.Purchases.Scripts
{
    public class PurchaseController : MonoBehaviour
    {
        [SerializeField] private Button _removeButton;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private TMP_Text _totalPriceText;
        [SerializeField] private Image _myImage;

        private ChairData _chairData;
        private ChairsSpriteData _spriteData;

        public event Action<int> Removed; 

        public PurchaseData CurrentPurchaseData { get; private set; }
        
        public void Init(ChairData chairData, ChairsSpriteData spriteData)
        {
            _spriteData = spriteData;
            _chairData = chairData;
            CurrentPurchaseData = new PurchaseData
            {
                Quantity = 1,
                Id = chairData.id
            };
            
            UpdateAll();
        }

        private void OnEnable()
        {
            _removeButton.onClick.AddListener(RemoveAll);
        }

        private void OnDisable()
        {
            _removeButton.onClick.RemoveListener(RemoveAll);
        }

        public void Add()
        {
            CurrentPurchaseData.Quantity++;
            UpdateAll();
        }

        public void Take()
        {
            CurrentPurchaseData.Quantity--;
            UpdateAll();
        }

        public void RemoveAll()
        {
            Removed?.Invoke(_chairData.id);
        }

        private void UpdateAll()
        {
            string[] words = _chairData.name.Split(' ');
            string firstWord = words[0];
            string remainingWords = string.Join(" ", words, 1, words.Length - 1);
            
            _nameText.text = $"{remainingWords}, {firstWord}";
            _priceText.text = $"${_chairData.price}";
            _quantityText.text = $"x{CurrentPurchaseData.Quantity}";
            _totalPriceText.text = $"${GetTotalPrice().FormatPrice()}";
            
            if (_spriteData.TryGetSprite(out Sprite sprite, _chairData.id))
                _myImage.sprite = sprite;
        }

        public int GetTotalPrice() => _chairData.price * CurrentPurchaseData.Quantity;
    }
}
