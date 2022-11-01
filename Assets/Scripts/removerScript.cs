using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removerScript : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Head")
        {
            //do nothing
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
