using UnityEngine;

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

    public const int MAXSLOT = 16;
    
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
    }

    public void RemoveItemBag(int index)
    {
        if(PartyManager.instance.SelectChars.Count == 0)
            return;
        
        PartyManager.instance.SelectChars[0].InventoryItems[index] = null;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
