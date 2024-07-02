using UnityEngine;

namespace Sources.Extensions.Scripts
{
    public static class MonoBehaviourExtension
    {
        public static void Enable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(true);
        }

        public static void Disable(this MonoBehaviour monoBehaviour)
        {
            monoBehaviour.gameObject.SetActive(false);
        }
    }
}