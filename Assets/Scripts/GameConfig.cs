using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig
{
    public HeroConfig heroConfig = null;
    public Dictionary<int, LevelConfig> levelConfigs = null;
    public Dictionary<CostumeType, Dictionary<string, CostumeData>> costumesConfig = null;

    public GameConfig()
    {

    }
}

