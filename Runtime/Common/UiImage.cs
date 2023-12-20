using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Common
{
    /// <summary>
    /// Custom overlay on <see cref="Image"/>.
    /// </summary>
    [AddComponentMenu("BroWar/UI/Common/UI Image")]
    public class UiImage : UiObject
    {
        [Title("Content")]
        [SerializeField]
        private Image image;

        public void HideIfEmpty(bool immediate = false)
        {
            Image image = Image;
            if (image == null || image.sprite == null)
            {
                Hide(immediate);
            }
        }

        public Sprite Sprite
        {
            get => Image.sprite;
            set => Image.sprite = value;
        }

        public Color Color
        {
            get => Image.color;
            set => Image.color = value;
        }

        private Image Image
        {
            get
            {
                if (image == null)
                {
                    image = GetComponent<Image>();
                }

                return image;
            }
        }
    }
}
