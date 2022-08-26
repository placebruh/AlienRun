using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class Gamemanager : MonoBehaviour
{
    [HideInInspector] public bool MoveByTouch, StartTheGame;
    private Vector3 _mouseStartPos, PlayerStartPos;
    [SerializeField] private float RoadSpeed, SwipeSpeed, Distance;
    [SerializeField] private GameObject Road;
    public static Gamemanager GameManagerInstance;

    private Camera mainCam;
    public bool isGameOver;
    public bool end = false;

    public List<Transform> Aliens = new List<Transform>();
    public GameObject Newball;
    public GameObject retryButton;
    public GameObject startButton;
    public GameObject nextLevel;


    [SerializeField] float xOffset;
    void Start()
    {
        isGameOver = false;
        Time.timeScale = 1;
        Aliens.Clear();
        nextLevel.SetActive(false);
        retryButton.SetActive(false);
        startButton.SetActive(true);

        Application.targetFrameRate = 60;
        GameManagerInstance = this;
        mainCam = Camera.main;
        Aliens.Add(gameObject.transform);
    }

    void Update()
    {
        if (end)
            return;

        if (startButton.activeSelf == true)
        {
            Time.timeScale = 0;
           retryButton.SetActive(false);
            nextLevel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartTheGame = MoveByTouch = true;

            Plane newPlan = new Plane(Vector3.up, 0f);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (newPlan.Raycast(ray, out var distance))
            {
                _mouseStartPos = ray.GetPoint(distance);
                PlayerStartPos = transform.position;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            MoveByTouch = false;
            print(MoveByTouch);
        }

        if (MoveByTouch)
        {

            var plane = new Plane(Vector3.up, 0f);

            float distance;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out distance))
            {
                Vector3 mousePos = ray.GetPoint(distance);
                Vector3 desirePso = mousePos - _mouseStartPos;
                Vector3 move = PlayerStartPos + desirePso;

                move.x = Mathf.Clamp(move.x, -2.2f, 2.2f);
                move.z = -7f;

                var player = transform.position;

                player = new Vector3(Mathf.Lerp(player.x, move.x, Time.deltaTime * (SwipeSpeed + 10f)), player.y, player.z);

                transform.position = player;
            }
        }

        if (StartTheGame && MoveByTouch)

        {
            Road.transform.Translate(Vector3.forward * (RoadSpeed * -1 * Time.deltaTime));
            foreach (var Alien_Anim in Aliens)
            {
                //Alien_Anim.GetComponent<Animator>().SetFloat("run", 1f);
                foreach (Animator draza in Alien_Anim.GetComponentsInChildren<Animator>())
                {
                    draza.SetFloat("run", 1f);
                }
            }

        }
        else
        {
            foreach (var Alien_Anim in Aliens)
            {
                Alien_Anim.GetComponent<Animator>().SetFloat("run", 0f);
                foreach (Animator draza in Alien_Anim.GetComponentsInChildren<Animator>())
                {
                    draza.SetFloat("run", 0f);
                }
            }
        }

        if (Aliens.Count > 1)
        {


            for (int i = 1; i < Aliens.Count; i++)
            {
                var FirstAlien = Aliens.ElementAt(i - 1);
                var SectAlien = Aliens.ElementAt(i);

                var DesireDistance = Vector3.Distance(FirstAlien.position, SectAlien.position); //

                if (DesireDistance <= Distance)//
                {
                    float xsranje, ysranje, zsranje;


                    xsranje = Mathf.Lerp(SectAlien.position.x, FirstAlien.position.x, SwipeSpeed * Time.deltaTime);
                    ysranje = SectAlien.position.y;
                    zsranje = Mathf.Lerp(SectAlien.position.z, FirstAlien.position.z + 0.5f, SwipeSpeed * Time.deltaTime);


                    SectAlien.position = new Vector3(xsranje, ysranje, zsranje);
                }
            }

        }
        if (Aliens.Count == 1)
        {

            Time.timeScale = 0.1f;
            retryButton.SetActive(true);
        }
        else
        {

        }


    }

    private void LateUpdate()
    {
        if (StartTheGame)
            mainCam.transform.position = new Vector3(Mathf.Lerp(mainCam.transform.position.x, transform.position.x, (SwipeSpeed - 5f) * Time.deltaTime),
                    mainCam.transform.position.y, mainCam.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("alienIdle"))
        {
            other.transform.parent = null;
            //other.gameObject.AddComponent<Rigidbody>().isKinematic = true;
            //other.gameObject.AddComponent<StackMgr>();
            other.gameObject.GetComponent<Collider>().isTrigger = true;
            other.tag = this.gameObject.tag;
            other.GetComponentInChildren<Renderer>().material = GetComponentInChildren<Renderer>().material;
            Aliens.Add(other.gameObject.transform);

        }

        if(other.CompareTag("finish"))
        {
            end = true;
        }
    }

    public void NextLevel()
    {
        LevelManager.GoToNextLevel();
    }
    public void TapToStart()
    {
        startButton.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        retryButton.SetActive(false);
        LevelManager.RestartLevel();

    }

    public void checkGameOver() 
    {
        print($"{Aliens.Count} is alien count");
        if (Aliens.Count == 1)
        {
            isGameOver = true;
        }
        else 
        {
            isGameOver = false;
            retryButton.SetActive(false);
        }
    }
}