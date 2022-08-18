using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private bool MoveByTouch;
    private Vector3 Direction;
    public List<Rigidbody> Rblst = new List<Rigidbody>();
    [SerializeField] private float runSpeed, velocity, swipeSpeed, roadSpeed;
    [SerializeField] private Transform road;
    public static PlayerManager PlayerManagerCls;


   
    void Start()
    {
        Application.targetFrameRate = 60;
        PlayerManagerCls = this;
        Rblst.Add(transform.GetChild(0).GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            MoveByTouch = true;
          
        }
        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false; 
        }
        if (MoveByTouch)
        {
            Direction = new Vector3(Mathf.Lerp(Direction.x, Input.GetAxis("Mouse X"), runSpeed * Time.deltaTime), 0f);

            Direction = Vector3.ClampMagnitude(Direction, 1f);

            road.position = new Vector3(0f, 0f, Mathf.SmoothStep(road.position.z, -100f, Time.deltaTime * roadSpeed));

            foreach (var Alien_Anim in Rblst) 
            {
                Alien_Anim.GetComponent<Animator>().SetFloat("run", 1f);
            }
           
        }
        else 
        {
            foreach (var Alien_Anim in Rblst)
            {
                Alien_Anim.GetComponent<Animator>().SetFloat("run", 0f);
            }
        }
        foreach (var alien_rb in Rblst)
        {
            if (alien_rb.velocity.magnitude > 0.5f)
            {
                alien_rb.rotation = Quaternion.Slerp(alien_rb.transform.rotation, Quaternion.LookRotation(alien_rb.velocity, Vector3.up), Time.deltaTime * velocity);
               // alien_rb.transform.LookAt(transform.position);
            }
            else
            {
                alien_rb.rotation = Quaternion.Slerp(alien_rb.transform.rotation, Quaternion.identity, Time.deltaTime * velocity);
            }
        }
       

    }

    private void FixedUpdate()
    {
        if (MoveByTouch)
        {
            Vector3 Displacement = new Vector3(Direction.x * Time.fixedDeltaTime * swipeSpeed, 0f, 0f) * Time.fixedDeltaTime;
            foreach (var alien_rb in Rblst)
                alien_rb.velocity = new Vector3(Direction.x * Time.deltaTime * swipeSpeed, 0f, 0f) + Displacement;
        }
        else 
        {
            foreach (var alien_rb in Rblst)
               alien_rb.velocity = Vector3.zero;
           
        }
    }
}
