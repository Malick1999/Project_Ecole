using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSetter : MonoBehaviour
{
    public GameObject selectedBlock;
    public Camera cam;
    public float reach;
    public LayerMask layerMask;

    Vector3Int invalidPos = Vector3Int.one * -100;

    void Start()
    {
        selectedBlock = GameManager.SelectBlock(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            SpawnBlock();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            selectedBlock = GameManager.SelectBlock(0);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            selectedBlock = GameManager.SelectBlock(1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            selectedBlock = GameManager.SelectBlock(2);
    }

    void SpawnBlock()
    {
        Vector3 pos = GetPos();

        if (pos == invalidPos)
            return;

        GameObject blockClone = Instantiate(selectedBlock, pos, Quaternion.identity);
        GameManager.AddBlock(blockClone);
    }

    Vector3Int GetPos()
    {
        Vector3Int returnedPos = invalidPos;

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, reach, layerMask))
        {
            Vector3 pos = hit.point;
            pos += hit.normal * 0.5f;

            returnedPos.x = (int)pos.x;
            returnedPos.y = (int)pos.y;
            returnedPos.z = (int)pos.z;
        }

        return returnedPos;
    }
}
