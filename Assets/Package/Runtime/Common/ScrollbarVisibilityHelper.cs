using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BroWar.UI.Common
{
    /// <summary>
    /// Helper class used to hide <see cref="Scrollbar"/>s while linked <see cref="ScrollRect"/> is moving right after the OnEnable callback.
    /// Useful for dynamic Scroll Views inside animated layout.
    /// </summary>
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollbarVisibilityHelper : MonoBehaviour
    {
        private Coroutine visbilityCoroutine;

        private ScrollRect scrollRect;
        private Scrollbar cachedVScrollbar;
        private Scrollbar cachedHScrollbar;

        private ScrollRect ScrollRect
        {
            get
            {
                if (scrollRect == null)
                {
                    scrollRect = GetComponent<ScrollRect>();
                }

                return scrollRect;
            }
        }

        private void Awake()
        {
            cachedVScrollbar = ScrollRect.verticalScrollbar;
            cachedHScrollbar = ScrollRect.horizontalScrollbar;
        }

        private void OnEnable()
        {
            visbilityCoroutine = StartCoroutine(HandleScrollVisibility());
        }

        private void OnDisable()
        {
            RestoreScrollbars();
            if (visbilityCoroutine != null)
            {
                StopCoroutine(visbilityCoroutine);
            }
        }

        private IEnumerator HandleScrollVisibility()
        {
            Transform scrollViewTransform = ScrollRect.transform;
            ScrollRect.verticalScrollbar = null;
            ScrollRect.horizontalScrollbar = null;
            if (cachedVScrollbar != null)
            {
                cachedVScrollbar.gameObject.SetActive(false);
            }

            if (cachedHScrollbar != null)
            {
                cachedHScrollbar.gameObject.SetActive(false);
            }

            while (scrollViewTransform.hasChanged)
            {
                scrollViewTransform.hasChanged = false;
                yield return null;
            }

            RestoreScrollbars();
        }

        private void RestoreScrollbars()
        {
            ScrollRect.verticalScrollbar = cachedVScrollbar;
            ScrollRect.horizontalScrollbar = cachedHScrollbar;
        }
    }
}