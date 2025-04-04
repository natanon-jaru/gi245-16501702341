using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private int id;
    public int ID { get { return id;} set {id = value;} }
    
    [SerializeField]
    private InventoryManager inventoryManager;
    
    void Start()
    {
        inventoryManager = InventoryManager.instance;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        //Get Item A
        GameObject objA = eventData.pointerDrag;
        ItemDrag itemDragA = objA.GetComponent<ItemDrag>();
        InventorySlot slotA = itemDragA.IconParent.GetComponent<InventorySlot>();
        
        //Remove Item A form Slot A
        inventoryManager.RemoveItemBag(slotA.ID);
        
        //There is an Item B in Slot B
        if (transform.childCount > 0)
        {
            GameObject objB = transform.GetChild(0).gameObject;
            ItemDrag itemDragB = objB.GetComponent<ItemDrag>();
            
            //Set Item B on Slot A
            itemDragB.transform.SetParent(itemDragA.transform.parent);
            itemDragB.IconParent = itemDragA.IconParent;
            inventoryManager.SaveItemBag(slotA.ID, itemDragB.Item);
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
