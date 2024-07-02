using System;
using UnityEngine;

namespace Sources.Modules.Chair.Scripts
{
    public class TooltipController : MonoBehaviour
    {
        private Transform _parent;
        
        private void Awake()
        {
            _parent = transform.parent;
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
