using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterConfig
{
    public class Position
    {
        public float x; 
        public float y;
        public float z;

        public Position() { }
    }

    public Position position = null;
    public HeroData heroData = null;

    public MonsterConfig() { }
}
