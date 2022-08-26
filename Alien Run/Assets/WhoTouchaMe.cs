using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhoTouchaMe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       

        if (other.CompareTag("DisguisedAlien"))
        {
        Destroy(this.gameObject);
            print("nabo me cigan");
        }

        if (other.CompareTag("Player"))
        {
            print("nabo sam cigana");
            Gamemanager.GameManagerInstance.Aliens.Remove(other.gameObject.transform);
            Destroy(other.gameObject);
        }


    }
}
