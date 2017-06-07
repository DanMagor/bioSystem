using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predatorBehavior : MonoBehaviour {



    public float maxHungryTime;
    public float maxRushTime;
    public float rushChance;
    float rushTime;
    // Use this for initialization
    int amountOfHerbivore;
    public float m_defaultSpeed;
    public float m_rushSpeed;
    float speed;
    public GameObject m_predator;
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
        amountOfHerbivore = 0;
        points = GameObject.FindGameObjectsWithTag("point");
        speed = m_defaultSpeed;

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
        rushTime = rushTime + Time.deltaTime;
        if (rushTime >= maxRushTime) speed = m_defaultSpeed;
        float step = speed * Time.deltaTime;
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
                amountOfHerbivore++;
                if (amountOfHerbivore == 3)
                {
                    levelController.spawnObject(Instantiate(m_predator), new Vector2(transform.position.x, transform.position.y));
                    amountOfHerbivore = 0;
                    levelController.amountOfHerbivore++; //TODO : DELETE AMOUNT

                }

                currentState = AnimalStates.MOVE;
                target = points[Random.Range(0, points.Length)];
                return;
            }
            if (currentState == AnimalStates.RUN)
            {
                Debug.Log("DEAD");
                return;
            }


        }
        else
        {
            if (collision.gameObject.tag == "herbivore")
            {
                Destroy(collision.gameObject);
                hungryTime = 0;
                amountOfHerbivore++;
                if (amountOfHerbivore == 5)
                {
                    levelController.spawnObject(Instantiate(m_predator), new Vector2(transform.position.x, transform.position.y));
                    amountOfHerbivore = 0;
                    

                }

                currentState = AnimalStates.MOVE;
                target = points[Random.Range(0, points.Length)];
                return;
            }
            
        }




    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "herbivore":
                if (currentState != AnimalStates.EAT)
                {
                    currentState = AnimalStates.EAT;
                    target = collision.gameObject;
                    if (Random.value <= rushChance)
                    {
                        speed = m_rushSpeed;
                        rushTime = 0;
                    }
                }
                break;
           

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "herbivore" && collision.gameObject == target)
        {
            
            speed = m_defaultSpeed;
            currentState = AnimalStates.MOVE;
            target = points[Random.Range(0, points.Length)];
        }
    }
}
