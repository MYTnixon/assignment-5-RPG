using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    private Equipment equipment;
    public GameObject itemButton;
    private PlayerController playerController;

    public bool isArmour;
    public bool isWeapon;
    public bool isHealth;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        equipment = GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

   public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + -1);
        Instantiate(item, playerPos, Quaternion.identity);
    }

    public void EquipItem()
    {
        if (isArmour && playerController.armourIsEquipped == false)
        {
            for (int i = 0; i < equipment.equipSlot.Length; i++)
            {
                if (equipment.isFull[0] == false)
                {
                    equipment.isFull[0] = true;
                    Instantiate(itemButton, equipment.equipSlot[0].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
            playerController.armourIsEquipped = true;
        }

        if (isWeapon && playerController.weaponIsEquipped == false)
        {
            for (int i = 0; i < equipment.equipSlot.Length; i++)
            {
                if (equipment.isFull[1] == false)
                {
                    equipment.isFull[1] = true;
                    Instantiate(itemButton, equipment.equipSlot[1].transform, false);
                    Destroy(gameObject);
                    break;
                }
            }
            playerController.weaponIsEquipped = true;
        }

        if (isHealth && GameManager.Instance.hp < 5)
        {
            GameManager.Instance.GainHealth(1);
            Destroy(gameObject);
        }
    }
}
