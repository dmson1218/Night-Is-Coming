using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeTime;

    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction.Normalize();
        this.rigid2D = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
        this.rigid2D.velocity = direction * 30;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}
