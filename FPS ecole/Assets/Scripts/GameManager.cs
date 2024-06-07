using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxBlocks;
    static int staticMaxBlocks;
    static int index = 0;
    static GameObject[] blocks;

    static InventoryManager inventoryManager;
    // Start is called before the first frame update
    void Awake()
    {
        blocks = new GameObject[maxBlocks];
        inventoryManager = GetComponent<InventoryManager>();
    }

    public static void AddBlock(GameObject block)
    {
        if(index >= 0 && index < blocks.Length)
        {
            blocks[index] = block;
            index++;
        }
    }

    public static void EnableBlockRbs()
    {
        for(int i = 0; i < blocks.Length; i++)
        {
            if(blocks[i] && blocks[i].TryGetComponent(out Rigidbody rb))
            {
                rb.isKinematic = false;
            }
        }
    }

    public static GameObject SelectBlock(int id)
    {
        return inventoryManager.SelectBlock(id);
    }
}
