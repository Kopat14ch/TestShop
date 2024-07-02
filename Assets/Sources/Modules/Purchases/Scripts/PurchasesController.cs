using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Cysharp.Threading.Tasks;
using Sources.Extensions.Scripts;
using Sources.Modules.Chair.Scripts.Data;
using Sources.Modules.Purchases.Scripts.Data;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Zenject;

namespace Sources.Modules.Purchases.Scripts
{
    public class PurchasesController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _totalPriceText;
        [SerializeField] private Button _checkoutButton;
        [SerializeField] private string _sendUrl;
        
        private List<PurchaseController> _purchaseControllers;
        private PurchaseFactory _purchaseFactory;

        [Inject]
        public void Construct(PurchaseFactory purchaseFactory)
        {
            _purchaseFactory = purchaseFactory;
            _purchaseControllers = new List<PurchaseController>();
            
            UpdateTotalPrice();
        }

        private void OnEnable()
        {
            _checkoutButton.onClick.AddListener(CheckoutButtonClick);
        }

        private void OnDisable()
        {
            foreach (var purchaseController in _purchaseControllers)
                purchaseController.RemoveButtonClicked -= RemoveAllId;
            
            _checkoutButton.onClick.RemoveListener(CheckoutButtonClick);
        }

        public void AddPurchase(ChairData data)
        {
            foreach (var currentPurchaseController in _purchaseControllers)
            {
                if (currentPurchaseController.CurrentPurchaseData.Id == data.id)
                {
                    currentPurchaseController.Add();
                    UpdateTotalPrice();
                    return;
                }
            }
            
            PurchaseController controller = _purchaseFactory.Create(data);
            controller.RemoveButtonClicked += RemoveAllId;
            _purchaseControllers.Add(controller);
            UpdateTotalPrice();
        }

        private void CheckoutButtonClick()
        {
            CheckoutData data = new CheckoutData();
            List<PurchaseData> purchasesData = new List<PurchaseData>();
            
            foreach (var purchaseController in _purchaseControllers)
                purchasesData.Add(purchaseController.CurrentPurchaseData);

            data.PurchasesData = purchasesData.ToArray();
            
            SendCheckoutRequest(JsonUtility.ToJson(data)).Forget();
        }

        private async UniTaskVoid SendCheckoutRequest(string jsonData)
        {
            using UnityWebRequest unityWebRequest = new UnityWebRequest(_sendUrl, "POST");
            unityWebRequest.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));
            unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
            unityWebRequest.SetRequestHeader("Content-Type", "application/json");

            try
            {
                await unityWebRequest.SendWebRequest().ToUniTask();

                if (unityWebRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Checkout Response: " + unityWebRequest.downloadHandler.text);
                }
                else
                {
                    Debug.LogError("Checkout Error: " + unityWebRequest.error);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        private void RemoveAllId(int id)
        {
            foreach (var purchaseController in _purchaseControllers)
            {
                if (purchaseController.CurrentPurchaseData.Id == id)
                {
                    purchaseController.RemoveButtonClicked -= RemoveAllId;
                    _purchaseControllers.Remove(purchaseController);
                    Destroy(purchaseController.gameObject);
                    UpdateTotalPrice();
                    return;
                }
            }
        }

        private int GetTotalPrice()
        {
            int result = 0;
            
            foreach (var purchaseController in _purchaseControllers)
            {
                result += purchaseController.GetTotalPrice();
            }

            return result;
        }

        private void UpdateTotalPrice()
        {
            _totalPriceText.text = $"${GetTotalPrice().FormatPrice()}";
        }
    }
}