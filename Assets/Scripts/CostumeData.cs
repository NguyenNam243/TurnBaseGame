using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CostumeData
{
    public CostumeType type;
    public string costumeName;

    public Dictionary<AttributeType, BaseAttribute> attributes = new Dictionary<AttributeType, BaseAttribute>();

    public CostumeData()
    {

    }
}
