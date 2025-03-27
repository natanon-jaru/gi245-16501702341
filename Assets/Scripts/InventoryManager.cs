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
        if (id < 0 || id >= itemData.Length)
        {
            Debug.LogError("Invalid item ID: " + id);
            return false;
        }

        if (character.InventoryItems == null || character.InventoryItems.Length < MAXSLOT)
        {
            character.InventoryItems = new Item[MAXSLOT];
        }

        Item item = new Item(itemData[id]);

        for (int i = 0; i < MAXSLOT; i++)
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
