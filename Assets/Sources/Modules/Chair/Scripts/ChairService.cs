using System.IO;
using Sources.Modules.Chair.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Sources.Modules.Chair.Scripts
{
    public class ChairService
    {
        private readonly ChairController _chairController;
        private readonly ChairData _data;
        private readonly ChairsSpriteData _spriteData;
        private readonly string _basePath;

        public ChairService(ChairController chairController, ChairData data, ChairsSpriteData spriteData, string basePath)
        {
            _chairController = chairController;
            _data = data;
            _spriteData = spriteData;
            _basePath = basePath;
        }

        public void Init()
        {
            _chairController.NameText.text = _data.name;
            _chairController.PriceText.text = $"${_data.price}";

            if (_spriteData.TryGetSprite(out Sprite sprite, _data.id))
                _chairController.MyImage.sprite = sprite;
        }
    }
}