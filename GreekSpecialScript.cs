using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreekSpecialScript : MonoBehaviour
{

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.collider.gameObject.GetComponent<Player2DController>().hp-=2;
            Destroy(this.gameObject);
        }
        else if (col.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
    }
}
