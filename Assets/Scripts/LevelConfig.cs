using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfig
{
    public int levelIndex = 1;
    public string levelName = "";
    public Dictionary<string, MonsterConfig> monsters = new Dictionary<string, MonsterConfig>();

    public LevelConfig() { }
}
