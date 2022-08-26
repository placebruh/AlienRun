using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xray : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("xRay"))
        {
            if (!this.gameObject.tag.Equals("DisguisedAlien")) 
            {
                Gamemanager.GameManagerInstance.Aliens.Remove(this.gameObject.transform);
                Destroy(this.gameObject);
            }

           
        }


    }
}
