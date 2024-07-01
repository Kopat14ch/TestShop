using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sources.Modules.Chair.Scripts.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Sources.Modules.Shop.Scripts.Api
{
    public class ChairsApiLoader
    {
        private readonly string _apiUrl;

        public ChairsApiLoader(string apiUrl)
        {
            _apiUrl = apiUrl;
            GetChairsAsync().GetAwaiter();
        }

        public async UniTask<List<ChairData>> GetChairsAsync()
        {
            List<ChairData> chairsData = new List<ChairData>();
            
            UnityWebRequest request = UnityWebRequest.Get(_apiUrl);
            await request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                chairsData = JsonUtility.FromJson<ChairListData>(request.downloadHandler.text).chairs;
                
                foreach (ChairData chair in chairsData)
                {
                    Debug.Log($"ID:{chair.id}, NAME:{chair.name}");
                }
            }
            else
            {
                Debug.LogError($"Failed to load chairs: {request.error}");
            }

            return chairsData;
        }
    }
}