using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class herbivoreBehavior : MonoBehaviour {
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
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
        if (checkEvent())
        {
        }
        else {
            if(m_move)
            moveToPoint();
        }

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
      
        
        float t_x = m_target.transform.position.x;
        float t_y = m_target.transform.position.y;
        float step = m_speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, step);
        //float dir_x, dir_y;
        //if (t_x < m_Rigidbody2D.transform.position.x) dir_x = -1; else dir_x = 1;
        //if (t_y < m_Rigidbody2D.transform.position.y) dir_y = -1; else dir_y = 1;
        //m_Rigidbody2D.velocity = new Vector2(m_speed * dir_x, m_speed * dir_y);



    }
    void Normalize() {

    }

    void move() {

    } 
    bool checkEvent() {
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
        else {
            transform.position = Vector2.MoveTowards(transform.position, m_target.transform.position, step);
        }
    }
}

