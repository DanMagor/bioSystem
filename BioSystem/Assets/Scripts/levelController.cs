using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelController : MonoBehaviour {

    public int minXPos = -45;
    public int maxXPos = 20;
    public int minYPos = -12;
    public int maxYPos = 12;
    public GameObject m_herbivore;
    public static int amountOfHerbivore = 0; //TODO DELETE AMOUNT
    public GameObject m_predator;
    public GameObject m_bush;
    public int m_herbivoreAmount = 1;
    public int m_predatorAmount = 1;
    public int m_bushAmount = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	

    private void Update()
    {
        bool r = Input.GetKeyDown(KeyCode.R);
        if (r) {
            respawn();
        }
    }






    private void respawn() {
            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("herbivore");
        foreach (GameObject obj in objs) {
            Destroy(obj);
        }
            objs = GameObject.FindGameObjectsWithTag("predator");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
            objs = GameObject.FindGameObjectsWithTag("bush");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }

        for (int i = 0; i < m_herbivoreAmount; i++) {
            GameObject herbivore = Instantiate(m_herbivore) as GameObject;
            var x = (Random.value - 0.5f) * 60;
            var y = (Random.value - 0.5f) * 24;
            Vector2 pos = new Vector2(x, y);
            spawnObject(herbivore, pos);
        }

        for (int i = 0; i < m_predatorAmount; i++)
        {
            GameObject predator = Instantiate(m_predator) as GameObject;
            var x = (Random.value - 0.5f) * 60;
            var y = (Random.value - 0.5f) * 24;
            Vector2 pos = new Vector2(x, y);    
            spawnObject(predator, pos);
        }

        for (int i = 0; i < m_bushAmount; i++)
        {
            GameObject bush = Instantiate(m_bush) as GameObject;
            var x = (Random.value - 0.5f) * 60;
            var y = (Random.value - 0.5f) * 24;
            Vector2 pos = new Vector2(x, y);
            spawnObject(bush, pos);
        }
    }

    public static void spawnObject(GameObject obj, Vector2 pos) {
        obj.transform.position = pos;
    }
    public static Vector2 randomPosition() {
        var x = (Random.value - 0.5f) * 60;
        var y = (Random.value - 0.5f) * 24;
        Vector2 pos = new Vector2(x, y);
        return pos;
    }
}
