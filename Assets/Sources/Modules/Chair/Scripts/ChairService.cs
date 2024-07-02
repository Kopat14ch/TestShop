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
        private readonly string _basePath;

        public ChairService(ChairController chairController, ChairData data, string basePath)
        {
            _chairController = chairController;
            _data = data;
            _basePath = basePath;
        }

        public void Init()
        {
            _chairController.NameText.text = _data.name;
            _chairController.PriceText.text = $"${_data.price}";
            _chairController.MyImage.sprite = Resources.Load<Sprite>($"{_basePath}/{Path.GetFileNameWithoutExtension(_data.filename)}");
        }
    }
}