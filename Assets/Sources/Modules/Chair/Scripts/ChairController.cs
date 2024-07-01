using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Chair.Scripts
{
    public class ChairController : MonoBehaviour
    {
        [field: SerializeField] public Image MyImage { get; private set; }
        [field: SerializeField] public TMP_Text NameText { get; private set; }
        [field: SerializeField] public TMP_Text PriceText { get; private set; }
        [field: SerializeField] public int Price { get; private set; }
    }
}