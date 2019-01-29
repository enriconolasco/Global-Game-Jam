using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.name == "greek statue(Clone)")
        {
            SceneManager.LoadScene(3);
        }
        if (col.gameObject.name == "plant(Clone)")
        {
            SceneManager.LoadScene(4);
        }
        if (col.gameObject.name == "Clifford(Clone)")
        {
            SceneManager.LoadScene(5);
        }
        if (col.gameObject.name == "Transcence(Clone)")
        {
            SceneManager.LoadScene(6);
        }
    }
}
