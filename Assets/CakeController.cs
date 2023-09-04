using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision) {
        GameObject director = GameObject.Find("GameDirector");
        if (collision.CompareTag("Player")) {
            director.GetComponent<GameDirector>().IncreaseHungry();
            Destroy(gameObject);
        }
    }
}
