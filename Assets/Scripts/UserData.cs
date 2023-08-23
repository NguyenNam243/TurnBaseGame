using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UserData : ICloneable
{
    public int userLevel = 1;
    public int exp = 0;
    public Dictionary<string, HeroData> heroes = new Dictionary<string, HeroData>();

    public UserData() { }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}
