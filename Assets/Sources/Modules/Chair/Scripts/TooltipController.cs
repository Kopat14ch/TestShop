using System;
using Sources.Modules.Chair.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sources.Modules.Chair.Scripts
{
    public class TooltipController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _chairNameText;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private TMP_Text _descriptionText;
        
        private Transform _parent;
        
        private void Awake()
        {
            _parent = transform.parent;
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
            transform.SetParent(_parent, false);
            transform.SetAsLastSibling();
        }
    }
}
