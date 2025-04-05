using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int id;
    public int ID { get { return id;} set {id = value;} }
    
    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField] 
    private ItemType itemType;
    public ItemType ItemType { get { return itemType; } set { itemType = value; } }
    
    void Start()
    {
        inventoryManager = InventoryManager.instance;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        //(This instance is Slot B)
        //Get Item A
        GameObject objA = eventData.pointerDrag;
        ItemDrag itemDragA = objA.GetComponent<ItemDrag>();
        InventorySlot slotA = itemDragA.IconParent.GetComponent<InventorySlot>();

        if (itemType == ItemType.Shield)
        {
            if (itemDragA.Item.Type != itemType)
            {
                return;
            }
        }

        //There is an Item B in Slot B
        if (transform.childCount > 0)
        {
            //Get Item B
            GameObject objB = transform.GetChild(0).gameObject;
            ItemDrag itemDragB = objB.GetComponent<ItemDrag>();

            if (slotA.ItemType == ItemType.Shield)
            {
                if (itemDragB.Item.Type != slotA.itemType)
                {
                    return;
                }
            }
            
            //Remove Item A from Slot A
            inventoryManager.RemoveItemInBag(slotA.ID);
            
            //Set Item B on Slot A
            itemDragB.transform.SetParent(itemDragA.transform.parent);
            itemDragB.IconParent = itemDragA.IconParent;
            inventoryManager.SaveItemBag(slotA.ID, itemDragB.Item);
            
            //Remove Item B from Slot B
            inventoryManager.RemoveItemInBag(id);
        }
        else //Slot B in blank
        {
            //Remove Item A from Slot A
            inventoryManager.RemoveItemInBag(slotA.ID);
        }
        //Set Item A on Slot B
        itemDragA.IconParent = transform;
        inventoryManager.SaveItemBag(slotA.ID, itemDragA.Item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
