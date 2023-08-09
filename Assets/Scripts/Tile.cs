using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TB.Grid
{
    public class Tile : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if (HeroDispatcher.anyCardOn)
            {
                HeroDispatcher.OnSpawnHeroEvent?.Invoke();
                Debug.Log("Spawn Hero");
            }
        }
    }
}


