using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    private Transform player;
    int key = 1;
    int life = 3;
    float knockbackSpeed = 1f;
    float destime;

    void Start()
    {
        // 태그로 플레이어 찾기
        this.player = GameObject.Find("Player").transform;
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    { 
        if (life <= 0) {
            if (destime < 0.2f) {
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1f - destime / 0.2f);
            } else {
                Destroy(gameObject);
            }
            destime += Time.deltaTime;
        }

        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        if (direction.x > 0) key = 1;
        if (direction.x < 0) key = -1;
        transform.localScale = new Vector3(0.5f * key, 0.5f, 1);

        if (((int)Time.time / 60) % 2 == 1) {
            this.rigid2D.velocity = direction * knockbackSpeed * 3.5f;
        } else {
            this.rigid2D.velocity = direction * knockbackSpeed * 2f;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Bullet")) {
            life--;
            StartCoroutine(Knockback());
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            life--;
            StartCoroutine(Knockback());
        } else if (collision.gameObject.tag != "Zombie" && ((int)Time.time / 60) % 2 == 1) {
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Knockback() {
        GetComponent<ParticleSystem>().Play();
        this.knockbackSpeed = -3f;
        yield return new WaitForSeconds(0.1f);
        this.knockbackSpeed = 1f;
    }
}
