using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TB.Grid
{
    public class Tile : MonoBehaviour, IPointerDownHandler
    {
        private Image icon = null;


        private void Awake()
        {
            icon = GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (HeroDispatcher.anyCardOn)
            {
                HeroDispatcher.OnSpawnHeroEvent?.Invoke(this);
            }
        }

        public void Hide() => icon.enabled = false;
    }
}


