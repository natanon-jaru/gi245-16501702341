using UnityEngine;

public class Hero : Character
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (InventoryItems == null || InventoryItems.Length != InventoryManager.MAXSLOT)
        {
            InventoryItems = new Item[InventoryManager.MAXSLOT];
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            case CharState.WalkToEnemy:
                WalkToEnemyUpdate();
                break;
            case CharState.Attack:
                AttackUpdate();
                break;
            case CharState.WalkToMagicCast:
                WalkToMagicCastUpdate();
                break;
        }
    }
}
