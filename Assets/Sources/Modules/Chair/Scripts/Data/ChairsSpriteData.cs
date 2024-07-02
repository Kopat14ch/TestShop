using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Chair.Scripts.Data
{
    public class ChairsSpriteData : MonoBehaviour
    {
        [SerializeField] private List<ChairSpriteData> _chairsSpriteData;

        public bool TryGetSprite(out Sprite sprite, int id)
        {
            sprite = null;

            foreach (var chairSprite in _chairsSpriteData)
            {
                if (chairSprite.Id == id)
                {
                    sprite = chairSprite.CurrentSprite;
                    return true;
                }
            }

            return false;
        }
    }
}