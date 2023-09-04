using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject Bullet;
    public Transform sPoint;
    private float shotTime;
    private float runStartTime;
    private float hungryTime;
    public AudioClip gun;

    Animator animator;

    Rigidbody2D rigid2D;
    Vector2 vector;
    int key = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject director = GameObject.Find("GameDirector");

        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");

        if (vector.x > 0) key = 1;
        if (vector.x < 0) key = -1;
        transform.localScale = new Vector3(0.5f * key, 0.5f, 1);

        this.animator.speed = 0;
        if (Input.GetKey(KeyCode.LeftShift)) {
            this.rigid2D.velocity = vector * 4.5f;
            if (Time.time > hungryTime) {
                director.GetComponent<GameDirector>().DecreaseHungry();
                director.GetComponent<GameDirector>().DecreaseHungry();

                hungryTime += 1f;
            }
        } else {
            this.rigid2D.velocity = vector * 3;
            if (Time.time > hungryTime) {
                hungryTime += 1f;
            }
        }
        if (Mathf.Abs(this.rigid2D.velocity.x) + Mathf.Abs(this.rigid2D.velocity.y) > 0) {
            this.animator.speed = 2;
        }

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - sPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButton(0)) {
            if(Time.time >= shotTime && director.GetComponent<GameDirector>().Ammo() > 0) {
                Instantiate(Bullet, sPoint.position, Quaternion.AngleAxis(angle, Vector3.forward));
                shotTime = Time.time + 0.15f;
                director.GetComponent<GameDirector>().DecreaseAmmo();
                GetComponent<AudioSource>().PlayOneShot(gun);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        GameObject director = GameObject.Find("GameDirector");
        if (collision.gameObject.tag == "Zombie") director.GetComponent<GameDirector>().DecreaseHp();
    }
}
