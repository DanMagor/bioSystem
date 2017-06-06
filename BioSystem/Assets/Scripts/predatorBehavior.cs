using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class predatorBehavior : MonoBehaviour {


    Rigidbody2D m_Rigidbody2D;
    // Use this for initialization
    bool m_move = true;
    public float m_speed = 1f;
    GameObject m_target;
    GameObject oldTarget;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (checkEvent())
        {
        }
        else
        {
            if (m_move)
                moveToPoint();
        }

        // Update is called once per frame
    }

    void moveToPoint()
    {
        GameObject[] points = GameObject.FindGameObjectsWithTag("point");
        if (m_target == null)
        {
            m_target = points[Random.Range(0, points.Length)];
            while (m_target.Equals(oldTarget))
                m_target = points[Random.Range(0, points.Length)];
        }


        
        float step = m_speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, step);
       


    }

    void move()
    {

    }
    bool checkEvent()
    {
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float step = m_speed * Time.deltaTime;
        if (collision.gameObject.tag == "point")
        {
            Debug.Log("Point");
            oldTarget = collision.gameObject;
            m_target = null;
        }
        else
        {   
            if (m_target != null)
            transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, step);
        }
    }
}
