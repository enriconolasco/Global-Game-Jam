using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject player;
    public int damage;

    public virtual void OnCollisionEnter2D(Collision2D col)
    {
        bool destroy = true;
        if (col.gameObject.CompareTag("Player") && col.gameObject != player)
        {
            col.collider.gameObject.GetComponent<Player2DController>().hp-= damage;
        }
        if(destroy)
        Destroy(this.gameObject);
    }
}
