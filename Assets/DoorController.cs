using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    int life = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (life == 0) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Bullet")) {
            life--;
        }
    }
}
