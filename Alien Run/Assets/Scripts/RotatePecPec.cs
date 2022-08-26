using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePecPec : MonoBehaviour
{

    [SerializeField] Vector3 spd;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spd * Time.deltaTime, Space.World);       
    }
}
