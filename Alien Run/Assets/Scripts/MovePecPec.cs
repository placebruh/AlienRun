using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePecPec : MonoBehaviour
{
    enum Direction{
        Vertical, Horizontal
    }
    [Header("Limits")]
    [SerializeField] float min,max;
    [SerializeField] float spd;
    [SerializeField] Direction direction;
    
    [SerializeField] float dir = 1;
    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case Direction.Vertical:
                VerticalMovement();
            break;

            case Direction.Horizontal:
                HorizontalMovement();
            break;
        }
    }

    void VerticalMovement() {
        ChangeDirections(transform.position.y);
        transform.Translate(new Vector3(0, spd * dir * Time.deltaTime, 0));
    }

    void HorizontalMovement() {
        ChangeDirections(transform.position.x);
        transform.Translate(new Vector3(spd * dir * Time.deltaTime, 0, 0));
    }

    void ChangeDirections(float value) {
        if(dir > 0) {
            if(value >= max)
                dir = -1;
        } else {
            if(value <= min)
                dir = 1;
        }
    }
}
