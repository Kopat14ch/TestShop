using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Extensions.Scripts;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Preloader.Scripts;
using UnityEngine;
using UnityEngine.Networking;

namespace Sources.Modules.Shop.Scripts.Api
{
    public class ChairsApiLoader
    {
        private readonly PreloaderController _preloaderController;
        private readonly string _apiUrl;

        public ChairsApiLoader(PreloaderController preloaderController, string apiUrl)
        {
            _preloaderController = preloaderController;
            _apiUrl = apiUrl;
        }

        public async UniTask<List<ChairData>> GetChairsAsync()
        {
            _preloaderController.Enable();
            List<ChairData> chairsData = new List<ChairData>();
            
            UnityWebRequest request = UnityWebRequest.Get(_apiUrl);
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                chairsData = JsonUtility.FromJson<ChairListData>(request.downloadHandler.text).chairs;
            }
            else
            {
                Debug.LogError($"Failed to load chairs: {request.error}");
            }
            
            _preloaderController.Disable();

            return chairsData;
        }
    }
}