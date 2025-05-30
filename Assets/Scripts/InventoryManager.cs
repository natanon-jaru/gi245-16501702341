using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemPrefabs;
    public GameObject[] ItemPrefabs
    {get{return itemPrefabs;} set{itemPrefabs = value;}}

    [SerializeField] 
    private ItemData[] itemData;
    public ItemData[] ItemData
    {get{return itemData;} set{itemData = value;}}

    public const int MAXSLOT = 18;
    
    public static InventoryManager instance;
    
    public bool AddItem(Character character, int id)
    {
        Item item = new Item(itemData[id]);

        for (int i = 0; i < character.InventoryItems.Length; i++)
        {
            if (character.InventoryItems[i] == null)
            {
                character.InventoryItems[i] = item;
                return true;
            }
        }

        Debug.Log("Inventory Full");
        return false;
    }
    
    public void SaveItemBag(int index, Item item)
    {
        if(PartyManager.instance.SelectChars.Count == 0)
            return;
        
        PartyManager.instance.SelectChars[0].InventoryItems[index] = item;

        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].EquipShield(item);
                break;
            case 17:
                Debug.Log("Put weapon");
                PartyManager.instance.SelectChars[0].EquipWeapon(item);
                break;
        }
    }

    public void RemoveItemInBag(int index)
    {
        if(PartyManager.instance.SelectChars.Count == 0)
            return;
        
        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;

        switch (index)
        {
            case 16:
                PartyManager.instance.SelectChars[0].UnEquipShield();
                break;
            case 17:
                Debug.Log("Not put weapon");
                PartyManager.instance.SelectChars[0].UnEquipWeapon();
                break;
        }
    }

    private void SpawnDropItem(Item item, Vector3 pos)
    {
        int id;

        switch (item.Type)
        {
            case ItemType.Consumable:
                id = 1;
                break;
            default:
                id = 0;
                break;
        }
        
        GameObject itemObj = Instantiate(itemPrefabs[id], pos + new Vector3(0,1,0), Quaternion.identity);
        itemObj.AddComponent<ItemPick>();
        
        ItemPick itemPick = itemObj.GetComponent<ItemPick>();
        itemPick.Init(item, instance, PartyManager.instance);
    }

    public void SpawnDropItem(Item[] item, Vector3 pos)
    {
        for (int i = 0; i < item.Length; i++)
        {
            if (item[i] != null)
            {
                SpawnDropItem(item[i], pos);
            }
        }
    }
    
    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddItemShopToNPC(1,0);
        AddItemShopToNPC(1,2);
        AddItemShopToNPC(1,9);
        AddItemShopToNPC(1,10);
        /*AddItemShopToNPC(1,11);
        AddItemShopToNPC(1,12);*/
        
        /*//Inventory of Member 0 = male
        AddItem(PartyManager.instance.Members[0], 0); //Health Potion
        AddItem(PartyManager.instance.Members[0], 1);
        AddItem(PartyManager.instance.Members[0], 2);
        AddItem(PartyManager.instance.Members[0], 3);
        AddItem(PartyManager.instance.Members[0], 7);
        AddItem(PartyManager.instance.Members[0], 10);
        AddItem(PartyManager.instance.Members[0], 11);
        AddItem(PartyManager.instance.Members[0], 12);

        //Inventory of Member 0 = female
        AddItem(PartyManager.instance.Members[1], 0); //Health Potion
        AddItem(PartyManager.instance.Members[1], 1); //Sword
        AddItem(PartyManager.instance.Members[1], 2); //Shield A
        AddItem(PartyManager.instance.Members[1], 3); //Shield B
        AddItem(PartyManager.instance.Members[1], 4);
        AddItem(PartyManager.instance.Members[1], 5);
        AddItem(PartyManager.instance.Members[1], 6);
        AddItem(PartyManager.instance.Members[1], 7);
        AddItem(PartyManager.instance.Members[1], 8);
        AddItem(PartyManager.instance.Members[1], 10);
        AddItem(PartyManager.instance.Members[1], 11);
        AddItem(PartyManager.instance.Members[1], 12);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrinkConsumableItem(Item item, int slotId)
    {
        string s = string.Format("Drink: {0}", item.ItemName);
        Debug.Log(s);

        if (PartyManager.instance.SelectChars.Count > 0)
        {
            PartyManager.instance.SelectChars[0].Recover(item.Power);
            RemoveItemInBag(slotId);
        }
    }

    public bool CheckPartyForItem(int id)
    {
        Item item = new Item(itemData[id]);
        Debug.Log(item.ItemName);

        List<Character> party = PartyManager.instance.Members;

        foreach (Character hero in party)
        {
            for (int i = 0; i < hero.InventoryItems.Length; i++)
            {
                Debug.Log(hero.InventoryItems[i].ItemName);
                if (hero.InventoryItems[i].ID == item.ID)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool RemoveItemFromParty(int id)
    {
        Item item = new Item(itemData[id]);
        Debug.Log($"Finding {item.ItemName}");
        
        List<Character> selectedHero = PartyManager.instance.SelectChars;

        foreach (Character hero in selectedHero)
        {
            for (int i = 0; i < hero.InventoryItems.Length; i++)
            {
                if (hero.InventoryItems[i].ID == item.ID)
                {
                     Debug.Log($"Removing {hero.InventoryItems[i].ItemName}");
                     hero.InventoryItems[i] = null;
                     Debug.Log($"Removing {hero.InventoryItems[i]}");

                     int questRewardExp = 10;
                     PartyManager.instance.DistributeTotalExp(questRewardExp);
                     
                     return true;
                }
               
            }
        }
        return false;
    }

    private void AddItemShopToNPC(int npcID, int itemId)
    {
        Item item = new Item(itemData[itemId]);
        QuestManager.instance.NPCPerson[npcID].ShopItems.Add(item);
    }
}
