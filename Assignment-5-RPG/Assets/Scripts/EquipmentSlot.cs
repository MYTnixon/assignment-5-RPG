using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : MonoBehaviour
{
    private PlayerController playerController;
    private Inventory inventory;
    private Equipment equipment;
    private ItemButton itemButton;

    private void Start()
    {
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (transform.childCount <= 0)
        {
            equipment.isFull[0] = false;
            equipment.isFull[1] = false;
        }
    }

    public void DropArmour()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ItemButton>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
            playerController.armourIsEquipped = false;
        }
    }

    public void DropWeapon()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<ItemButton>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
            playerController.weaponIsEquipped = false;
            playerController.bowIsEquipped = false;
            playerController.swordIsEquipped = false;
        }
    }
}
