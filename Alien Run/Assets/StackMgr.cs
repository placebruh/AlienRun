using System;
using System.Linq;
using UnityEngine;
using System.Collections;

public class StackMgr : MonoBehaviour
{
    [SerializeField] GameObject particle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("alienIdle"))
        {
            other.transform.parent = null;          
           // other.gameObject.AddComponent<StackMgr>();
            other.gameObject.GetComponent<Collider>().isTrigger = true;
            other.tag = "Player";
            other.GetComponentInChildren<Renderer>().material = GetComponentInChildren<Renderer>().material;
            Gamemanager.GameManagerInstance.Aliens.Add(other.gameObject.transform);
        }

        if (other.CompareTag("Trap")) 
        {
            print($"{this.name} collided with trap");
            Gamemanager.GameManagerInstance.Aliens.Remove(this.gameObject.transform);
        Destroy(this.gameObject);

            Gamemanager.GameManagerInstance.checkGameOver();
            //Debug.Log()


        }

        if (other.CompareTag("finish"))
        {

            Gamemanager.GameManagerInstance.nextLevel.SetActive(true);
            foreach (Renderer r in this.GetComponentsInChildren<Renderer>()) 
            {
                r.enabled = false;
            }
        }
    }

    void OnDestroy()
    {
        Instantiate(particle, transform.position, Quaternion.identity);
    }

    
}