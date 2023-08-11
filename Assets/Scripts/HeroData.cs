using System.Collections.Generic;

public class HeroData
{
    public string name;
    public int level;
    public HeroRarity rarity;
    public Dictionary<AttributeType, BaseAttribute> attributes = new Dictionary<AttributeType, BaseAttribute>();
    public Dictionary<CostumeType, CostumeData> costumes = new Dictionary<CostumeType, CostumeData>();

    public HeroData() { }
}
