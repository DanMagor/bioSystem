using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class herbivoreBehavior : MonoBehaviour {
    Rigidbody2D m_Rigidbody2D;
    // Use this for initialization
    bool m_move = true;
    float m_speed = 1f;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
        GameObject target = points[0];
        float t_x = target.transform.position.x;
        float t_y = target.transform.position.y;
        float dir_x, dir_y;
        if (t_x < m_Rigidbody2D.transform.position.x) dir_x = -1; else dir_x = 1;
        if (t_y < m_Rigidbody2D.transform.position.y) dir_y = -1; else dir_y = 1;
        m_Rigidbody2D.velocity = new Vector2(m_speed * dir_x, m_speed * dir_y);
        
       
       
    }

    void move() {

    } 
    bool checkEvent() {
        return false; 
    }
}

