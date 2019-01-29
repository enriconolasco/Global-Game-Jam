using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class characterSelect : MonoBehaviour
{
    public GameObject cursor1;
    public GameObject cursor2;

    public bool ready1 = false;
    public bool ready2 = false;

    void Update()
    {
        manage();
        if(ready1 && ready2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        moveCursor1();
        moveCursor2();
    }

    void manage()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ready2 = false;
        }
        if (Input.GetKeyDown(KeyCode.Home))
        {
            ready1 = false;
        }
    }

    void moveCursor1()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            cursor1.transform.Translate(Vector3.up/10f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cursor1.transform.Translate(Vector3.down / 10f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            cursor1.transform.Translate(Vector3.right / 10f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            cursor1.transform.Translate(Vector3.left / 10f);
        }
    }

    void moveCursor2()
    {
        if (Input.GetKey(KeyCode.F))
        {
            cursor2.transform.Translate(Vector3.up / 10f);
        }
        if (Input.GetKey(KeyCode.V))
        {
            cursor2.transform.Translate(Vector3.down / 10f);
        }
        if (Input.GetKey(KeyCode.B))
        {
            cursor2.transform.Translate(Vector3.right / 10f);
        }
        if (Input.GetKey(KeyCode.C))
        {
            cursor2.transform.Translate(Vector3.left / 10f);
        }
    }
}
