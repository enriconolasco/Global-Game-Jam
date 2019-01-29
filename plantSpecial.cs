using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantSpecial : MonoBehaviour
{
    public GameObject plant;

    public void SpawnPlant()
    {
        Instantiate(plant, gameObject.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
