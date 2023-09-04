using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject player;
    public GameObject zombie;
    GameObject hpGauge;
    GameObject hungryGauge;
    GameObject ammoGauge;
    GameObject timer;
    GameObject gameMap;
    GameObject gameMapNight;
    public AudioClip ZombieSound;

    private float spawnTime;
    private float hungryTime;
    private float totalTime;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.Find("Player");
        this.hpGauge = GameObject.Find("hpGauge");
        this.hungryGauge = GameObject.Find("hungryGauge");
        this.ammoGauge = GameObject.Find("ammoGauge");
        this.timer = GameObject.Find("timer");
        this.gameMap = GameObject.Find("gameMap");
        this.gameMapNight = GameObject.Find("gameMapNight");

    }

    // Update is called once per frame
    void Update()
    {   
        if (this.hpGauge.GetComponent<Image>().fillAmount <= 0.001f) SceneManager.LoadScene("GameOver");

        if (Time.time > spawnTime) {
            Instantiate(zombie, GetRandomPosition(), Quaternion.identity);
            if (((int)totalTime / 60) % 2 == 1) {
                spawnTime = Time.time + 1f;
            } else {
                spawnTime = Time.time + 5f;
            }
        }

        if (Time.time > hungryTime) {
            DecreaseHungry();
            hungryTime = hungryTime + 1f;
        }

        totalTime += Time.deltaTime;
        this.timer.GetComponent<Text>().text = ((int)totalTime / 60).ToString() + " : " + ((int)totalTime % 60).ToString();

        if (((int)totalTime / 60) % 2 == 1) {
            if (gameMap.GetComponent<SpriteRenderer>().sortingOrder == -1) {
                GetComponent<AudioSource>().PlayOneShot(ZombieSound);
            }
            gameMap.GetComponent<SpriteRenderer>().sortingOrder = -2;
            gameMapNight.GetComponent<SpriteRenderer>().sortingOrder = -1;
        } else {
            gameMap.GetComponent<SpriteRenderer>().sortingOrder = -1;
            gameMapNight.GetComponent<SpriteRenderer>().sortingOrder = -2;
        }
    }

    public Vector3 GetRandomPosition()
    {
        float radius = 10f;
        Vector3 playerPosition = this.player.transform.position;
 
        float a = playerPosition.x;
        float b = playerPosition.y;
 
        float x = Random.Range(-radius + a, radius + a);
        float y_b = Mathf.Sqrt(Mathf.Pow(radius, 2) - Mathf.Pow(x - a, 2));
        y_b *= Random.Range(0, 2) == 0 ? -1 : 1;
        float y = y_b + b;
 
        Vector3 randomPosition = new Vector3(x, y, 0);
 
        return randomPosition;
    }

    public void IncreaseHp() {
        this.hpGauge.GetComponent<Image>().fillAmount += 0.1f;
    }

    public void IncreaseHungry() {
        this.hungryGauge.GetComponent<Image>().fillAmount += 0.5f;
    }

    public void IncreaseAmmo() {
        this.ammoGauge.GetComponent<Image>().fillAmount = 1;
    }

    public void DecreaseHp() {
        this.hpGauge.GetComponent<Image>().fillAmount -= 0.2f;
    }

    public void DecreaseHungry() {
        if (this.hungryGauge.GetComponent<Image>().fillAmount > 0) {
            this.hungryGauge.GetComponent<Image>().fillAmount -= 0.01f;
        } else {
            this.hpGauge.GetComponent<Image>().fillAmount -= 0.01f;
        }
    }

    public void DecreaseAmmo() {
        this.ammoGauge.GetComponent<Image>().fillAmount -= 0.01f;
    }

    public float Ammo() {
        return this.ammoGauge.GetComponent<Image>().fillAmount;
    }
}
