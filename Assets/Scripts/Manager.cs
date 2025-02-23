using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject coin, magnet, log, car;
    Transform child;
    List<GameObject> coins;
    List<GameObject> others;

    public TMPro.TextMeshProUGUI skor_txt;
    public int point = 0;

    void Start()
    {
        coins = new List<GameObject>();
        others = new List<GameObject>();
        child = GameObject.Find("Child").transform;

        Produce(coin, 10, coins);
        Produce(magnet, 1, others);
        Produce(log, 8, others);
        Produce(car, 4, others);

        InvokeRepeating("CoinProduce", 0, 1f);
        InvokeRepeating("ObstacleProduce", 1f, 3f);
    }

    void Update()
    {
        skor_txt.text = "SCORE " + point;
        if (point > PlayerPrefs.GetInt("high_score"))
            PlayerPrefs.SetInt("high_score", point);
    }

    void ObstacleProduce()
    {
        int rnd = Random.Range(0, others.Count);
        if (!others[rnd].activeSelf)
        {
            others[rnd].SetActive(true);

            int Randomrnd = Random.Range(0, 2);
            if (Randomrnd == 0)
            {
                others[rnd].transform.position = new Vector3(.4f, others[rnd].transform.position.y, child.position.z + 10f);
            }
            if (Randomrnd == 1)
            {
                others[rnd].transform.position = new Vector3(-.4f, others[rnd].transform.position.y, child.position.z + 10f);
            }
            if (others[rnd].tag == "magnet")
            {
                if (child.gameObject.GetComponent<ChildController>().magnetReceived)
                {
                    others[rnd].SetActive(false);
                } 
            }
        }
        else
        {
            foreach (GameObject obj in others)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                    int Randomrnd2 = Random.Range(0, 2);
                    if (Randomrnd2 == 0)
                    {
                        obj.transform.position = new Vector3(.4f, obj.transform.position.y, child.position.z + 10f);
                    }
                    if (Randomrnd2 == 1)
                    {
                        obj.transform.position = new Vector3(-.4f, obj.transform.position.y, child.position.z + 10f);
                    }
                    if (obj.tag == "magnet")
                    {
                        if (child.gameObject.GetComponent<ChildController>().magnetReceived)
                        {
                            obj.SetActive(false);
                        }
                    }
                    return;
                }
            }
        }
    }

    void CoinProduce()
    {
        foreach (GameObject coin in coins)
        {
            if (!coin.activeSelf)
            {
                coin.SetActive(true);
                int rnd = Random.Range(0, 2);
                if (rnd == 0)
                {
                    coin.transform.position = new Vector3(.4f, 3f, child.position.z + 10f);
                }
                if (rnd == 1)
                {
                    coin.transform.position = new Vector3(-.4f, 3f, child.position.z + 10f);
                }
                return;
            }
        }
    }

    void Produce(GameObject gameObject, int amount, List<GameObject> list)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject newObject = Instantiate(gameObject);
            newObject.SetActive(false);
            list.Add(newObject);
        }
    }

    public void PointAdd()
    {
        point ++;
    }
}
