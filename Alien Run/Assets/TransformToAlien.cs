using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformToAlien : MonoBehaviour
{
    public GameObject DisguisedAlien;
    public GameObject RegularAlien;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gate"))
        {
            RegularAlien.SetActive(false);
            DisguisedAlien.SetActive(true);

            transform.gameObject.tag = "DisguisedAlien";
        }
       

    }
}
