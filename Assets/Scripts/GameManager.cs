using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heroPrefabs;
    public GameObject[] HeroPrefabs { get { return heroPrefabs; } }

    public static GameManager instance;

    private void WarpPlayer()
    {
        PartyManager.instance.LoadAllHeroData();
    }

    void Awake()
    {
        instance = this;
    }
    
    private void GeneratePlayerHero()
    {
        int i = Settings.playerPrefabId;

        GameObject heroObj = Instantiate(heroPrefabs[i],
            new Vector3(46f, 10f, 38f), Quaternion.identity);

        heroObj.tag = "Player";

        Character hero = heroObj.GetComponent<Character>();
        PartyManager.instance.Members.Add(hero);
        
        hero.CharInit(VFXManager.instance, UIManager.instance,
            InventoryManager.instance, PartyManager.instance);

        InventoryManager.instance.AddItem(hero, 0);
        InventoryManager.instance.AddItem(hero, 2);
        InventoryManager.instance.AddItem(hero, 7);
        InventoryManager.instance.AddItem(hero, 11);
        InventoryManager.instance.AddItem(hero, 12);
    }

    void Start()
    {
        if (Settings.isNewGame)
        {
            Settings.isNewGame = false;
            GeneratePlayerHero();
            AudioManager.instance.PlayBGM(1);
        }

        if (Settings.isWarping)
        {
            Settings.isWarping = false;
            WarpPlayer();
        }

    }


}
