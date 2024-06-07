using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject builder, destroyer;
    public Vector3 destroyerSpawn;


    public void SwitchToDestroyer()
    {
        builder.SetActive(false);
        Destroy(builder, 1f);

        Instantiate(destroyer, destroyerSpawn, Quaternion.identity);
    }


}
