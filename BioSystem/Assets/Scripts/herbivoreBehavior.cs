using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class herbivoreBehavior : MonoBehaviour
{
    public float maxHungryTime;
    // Use this for initialization
    int amountOfBushes;
    public float m_speed = 1f;
    public GameObject m_herbivore;
    float hungryTime;
    AnimalStates currentState;
    GameObject target;
    GameObject[] points;
    enum AnimalStates
    {
        MOVE,
        RUN,
        EAT
    }
    private void Awake()
    {
        hungryTime = 0;
        amountOfBushes = 0;
        points = GameObject.FindGameObjectsWithTag("point");

    }

    void Start()
    {

        target = points[Random.Range(0, points.Length)];
        currentState = AnimalStates.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hungryTime >= maxHungryTime)
        {
            Destroy(gameObject);
        }
        else
        {
            moveToTarget();
        }
        
        hungryTime = hungryTime + Time.deltaTime;
        
    }


    void moveToTarget()
    {
        float step = m_speed * Time.deltaTime;
        switch (currentState)
        {
            case AnimalStates.MOVE:
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
                break;
            case AnimalStates.EAT:
                if (target == null)
                {
                    currentState = AnimalStates.MOVE;
                    target = points[Random.Range(0, points.Length)];
                }
                else

                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
                break;
            case AnimalStates.RUN:
                if (target == null)
                {
                    currentState = AnimalStates.MOVE;
                    target = points[Random.Range(0, points.Length)];
                }
                else
                    transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -step);
                break;

        }


  }





    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject == target)
        {

            if (currentState == AnimalStates.MOVE)
            {
                GameObject oldPoint = target;
                while (target == oldPoint)
                    target = points[Random.Range(0, points.Length)];
                return;
            }
            if (currentState == AnimalStates.EAT)
            {

                Destroy(collision.gameObject);
                amountOfBushes++;
                if (amountOfBushes == 3)
                {
                    levelController.spawnObject(Instantiate(m_herbivore), new Vector2(transform.position.x, transform.position.y));
                    amountOfBushes = 0;
                    levelController.amountOfHerbivore++; //TODO : DELETE AMOUNT

                }

                currentState = AnimalStates.MOVE;
                target = points[Random.Range(0, points.Length)];
                return;
            }
            if (currentState == AnimalStates.RUN)
            {
                Destroy(gameObject);
                return;
            }


        }
        else
        {
            if (collision.gameObject.tag == "bush")
            {
                Destroy(collision.gameObject);
                hungryTime = 0;
                amountOfBushes++;
                if (amountOfBushes == 3)
                {
                    levelController.spawnObject(Instantiate(m_herbivore), new Vector2(transform.position.x, transform.position.y));
                    amountOfBushes = 0;
                    levelController.amountOfHerbivore++; //TODO DELETE AMOUNT

                }

                currentState = AnimalStates.MOVE;
                target = points[Random.Range(0, points.Length)];
                return;
            }
            if (collision.gameObject.tag == "predator")
            {
                
                Destroy(gameObject);
            }
        }




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "bush":

                currentState = AnimalStates.EAT;
                target = collision.gameObject;
                break;
            case "predator":
                
                currentState = AnimalStates.RUN;
                target = collision.gameObject;
                break;

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "predator")
        {
            
            currentState = AnimalStates.MOVE;
            target = points[Random.Range(0, points.Length)];
        }
    }
}

