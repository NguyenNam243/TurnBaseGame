using UnityEngine;

public class HeroController : MonoBehaviour
{
    

    public HeroData HeroData { get; private set; }

    private SpriteRenderer avatar = null;
    private RectTransform rect = null;



    public void Initialized(HeroData HeroData)
    {
        this.HeroData = HeroData;
        avatar = GetComponent<SpriteRenderer>();
        rect = GetComponent<RectTransform>();
        avatar.sprite = Resources.Load<Sprite>(string.Format(GameConstants.CARDHERO, "Icon", HeroData.name));
        HeroSizeStore.HeroSize size = ConfigDataHelper.HeroSizeStore.sizes.Find(s => s.heroName.Equals(HeroData.name));
        if (size != null)
            transform.localScale = Vector3.one * size.size;
    }
}
