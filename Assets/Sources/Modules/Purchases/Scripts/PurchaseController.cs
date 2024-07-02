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
        [SerializeField] public Button _removeButton;
        [field: SerializeField] public TMP_Text NameText { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public TMP_Text QuantityText { get; private set; }
        [field: SerializeField] public TMP_Text TotalPriceText { get; private set; }
        [field: SerializeField] public Image MyImage { get; private set; }

        private ChairData _chairData;

        public event Action<int> RemoveButtonClicked; 

        public PurchaseData CurrentPurchaseData { get; private set; }
        
        public void Init(ChairData chairData)
        {
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

        private void RemoveAll()
        {
            RemoveButtonClicked?.Invoke(_chairData.id);
        }

        private void UpdateAll()
        {
            string[] words = _chairData.name.Split(' ');
            string firstWord = words[0];
            string remainingWords = string.Join(" ", words, 1, words.Length - 1);
            
            NameText.text = $"{remainingWords}, {firstWord}";
            PriceText.text = $"${_chairData.price}";
            QuantityText.text = $"x{CurrentPurchaseData.Quantity}";
            TotalPriceText.text = $"${GetTotalPrice().FormatPrice()}";
        }

        public int GetTotalPrice() => _chairData.price * CurrentPurchaseData.Quantity;
    }
}
