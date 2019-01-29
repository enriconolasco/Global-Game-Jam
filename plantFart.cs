using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plantFart : MonoBehaviour
{
    public GameObject player;

    void OnCollisionEnter2D(Collision2D col)
    {
        bool destroy = true;
        if (col.gameObject.CompareTag("Player") && col.gameObject != player) //not this gameObject
        {
            col.gameObject.GetComponent<Player2DController>().hp -= 4;
            Destroy(this.gameObject);
        }
        if (col.gameObject.CompareTag("Player") && col.gameObject == player)
        {
            col.gameObject.GetComponent<Player2DController>().hp++;
            Destroy(this.gameObject);
        }
        else
        {
            if (destroy)
                Destroy(this.gameObject);
        }
    }
}
