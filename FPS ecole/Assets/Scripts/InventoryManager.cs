using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] blocks = new GameObject[3];

    public Image[] slots = new Image[3];

    public float selectedScale = 1.3f;
    public float unselectedScale = 1;

    public GameObject SelectBlock(int id)
    {
        GlowSelectedTile(id);
        return blocks[id];
    }

    public void GlowSelectedTile(int id)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i == id)
                slots[i].rectTransform.localScale = Vector3.one * selectedScale;

            else
                slots[i].rectTransform.localScale = Vector3.one * unselectedScale;
        }
    }
}
