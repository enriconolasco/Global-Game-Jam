using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class choseCharacter : MonoBehaviour
{
    public characterSelect cs;
    public manager mg;
    public int characterNumber;
    public int characterNumber2;

    void OnTriggerStay2D (Collider2D col)
    {
        if (col.gameObject.CompareTag("1") && Input.GetKeyDown(KeyCode.U) && !cs.ready1){
            mg.selection1 = characterNumber;
            cs.ready1 = true;
        }    
        if(col.gameObject.CompareTag("2") && Input.GetKeyDown(KeyCode.Q) && !cs.ready2)
        {
            mg.selection2 = characterNumber2;
            cs.ready2 = true;
        }  
    }
}
