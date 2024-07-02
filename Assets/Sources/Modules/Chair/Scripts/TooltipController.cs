using System;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Shop.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Sources.Modules.Chair.Scripts
{
    public class TooltipController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _chairNameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _descriptionText;
        
        private ShopContainer _shopContainer;

        [Inject]
        public void Construct(ShopContainer shopContainer)
        {
            _shopContainer = shopContainer;
        }

        public void Init(ChairData chairData)
        {
            _chairNameText.text = chairData.name;
            _priceText.text = $"${chairData.price}";
            _descriptionText.text = chairData.description;
        }

        public void SetNewParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void SetDefaultParent()
        {
            transform.SetParent(_shopContainer.transform, false);
            transform.SetAsLastSibling();
        }
    }
}
