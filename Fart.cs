using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fart : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D  (Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && col.gameObject != player) //not this gameObject
        {
            col.gameObject.GetComponent<Player2DController>().hp -= 3;
        }
    }
}
