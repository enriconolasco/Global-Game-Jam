using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.CompareTag("projectile"))
        {
            Destroy(col.gameObject);
        }
    }
}
