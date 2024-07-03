using System;
using Game.Data;
using Game.Extensions;
using UnityEngine;

namespace Game.Configs.UI
{
    [CreateAssetMenu(fileName = "UIIcons", menuName = "Create/UIIcons", order = 0)]
    public class UIIconsSO : ScriptableObject
    {
        private Sprite _emptySprite;

        [SerializeField] private UnityEnumObject<CurrencyType, Sprite>[] _currencyIcons;

        [SerializeField] private UnityEnumObject<ResourceItemType, Sprite>[] _allResourcesAndToolIcons;

        [SerializeField] private Sprite _cancelIcon;

        public Sprite CancelIcon => _cancelIcon;

        private Sprite EmptySprite
        {
            get
            {
                if (_emptySprite == null)
                {
                    Texture2D texture = new Texture2D(1, 1);
                    texture.SetPixel(0, 0, new Color(0, 0, 0, 0));
                    texture.Apply();
                    _emptySprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                        Vector2.one * 0.5f);
                }

                return _emptySprite;
            }
        }

        public Sprite GetCurrencyIcon(CurrencyType currencyType)
        {
            return _currencyIcons.Find(o => o.Key == currencyType)?.Value;
        }

        public Sprite GetResourceItemIcon(ResourceItemType itemType)
        {
            var icon = _allResourcesAndToolIcons.Find(o => o.Key == itemType)?.Value;
            return icon == null ? EmptySprite : icon;
        }

        public Sprite GetIcon<TEnum>(TEnum type) where TEnum : struct, Enum
        {
            if (type is CurrencyType currencyType)
            {
                return GetCurrencyIcon(currencyType);
            }

            if (type is ResourceItemType resourceItemType)
            {
                return GetResourceItemIcon(resourceItemType);
            }

            return null;
        }
    }
}