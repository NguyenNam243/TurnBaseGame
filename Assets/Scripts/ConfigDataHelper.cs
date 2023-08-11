
using Newtonsoft.Json;
using UnityEngine;

public static class ConfigDataHelper
{
    private static GameConfig gameConfig = null;
    public static GameConfig GameConfig
    {
        get
        {
            if (gameConfig == null)
                gameConfig = JsonConvert.DeserializeObject<GameConfig>(Resources.Load<TextAsset>("Config/GameConfig").text);
                
            return gameConfig;
        }
    }
    private static UserData userData = null;
    public static UserData UserData
    {
        get
        {
            if (!ES3.KeyExists(GameConstants.USERDATA))
            {
                userData = GetDefaultUserData();
                ES3.Save(GameConstants.USERDATA, userData);
            }
            else
                userData = ES3.Load<UserData>(GameConstants.USERDATA);
            return userData;
        }
    }

    private static HeroSizeStore heroSizeStore = null;
    public static HeroSizeStore HeroSizeStore 
    {
        get
        {
            if (heroSizeStore == null)
                heroSizeStore = Resources.Load<HeroSizeStore>("Config/HeroSizeConfig");
            return heroSizeStore;
        }
    }
    

    private static UserData GetDefaultUserData()
    {
        UserData user = new UserData();
        user.userLevel = 1;
        user.exp = 0;
        user.heroes.Add("Hero1", GetDefaultHeroData("Hero1"));
        user.heroes.Add("Hero3", GetDefaultHeroData("Hero3"));
        user.heroes.Add("Hero2", GetDefaultHeroData("Hero2"));
        return user;
    }

    private static HeroData GetDefaultHeroData(string heroName)
    {
        HeroData hero1 = new HeroData();
        hero1.name = heroName;
        hero1.level = 1;
        hero1.rarity = HeroRarity.Common;
        hero1.attributes = GameConfig.heroConfig.baseAttribute;
        return hero1;
    }

    public static LevelConfig GetLevelConfig(int level)
    {
        return GameConfig.levelConfigs[level];
    }
}
