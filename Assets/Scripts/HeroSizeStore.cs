using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSizeStore : ScriptableObject
{
    [System.Serializable]
    public class HeroSize
    {
        public string heroName;
        public float size;
    }

    public List<HeroSize> sizes;
}
