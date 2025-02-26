using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ChildController : MonoBehaviour
{
    public GameObject dust;
    Transform path1, path2;
    Rigidbody rb;
    Animator anim;

    float jumpPower = 5f;
    float runSpeed = 2f;

    bool right;
    bool left;
    bool isJump = false;
    public bool magnetReceived = false;
    float speedIncreaseFactor = 0.1f;
    float laneChangeSpeed = 10f;

    Manager manager;
    HighScore highScore;
    PanelManager panelManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        anim = GetComponent<Animator>();
        path1 = GameObject.Find("path_1").transform;
        path2 = GameObject.Find("path_2").transform;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        highScore = GameObject.Find("Manager").GetComponent<HighScore>();
        panelManager = GameObject.Find("Manager").GetComponent<PanelManager>();
        rb.drag = 0.5f; // Daha akýcý hareket için sürtünme azaltýldý
        rb.mass = 1f;   // Kütle optimize edildi
    }

    private void OnCollisionStay(Collision collision)
    {
        isJump = false;
        if (!dust.activeSelf)
        {
            dust.SetActive(true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isJump = true;
        if (dust.activeSelf)
        {
            dust.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            panelManager.finish_panel.SetActive(true);
            panelManager.menuPanel.SetActive(false);
            panelManager.menu.SetActive(false);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "path_1")
        {
            path2.position = new Vector3(path2.position.x, path2.position.y, path1.position.z + 10f);
        }
        if (other.gameObject.name == "path_2")
        {
            path1.position = new Vector3(path1.position.x, path1.position.y, path2.position.z + 10f);
        }
        if (other.gameObject.tag == "coin")
        {
            other.gameObject.SetActive(false);
            manager.PointAdd();
            runSpeed +=  speedIncreaseFactor;
        }
        if (other.gameObject.tag == "magnet")
        {
            other.gameObject.SetActive(false);
            magnetReceived = true;
            Invoke("MagnetFinish", 10f);
        }
    }

    void MagnetFinish()
    {
        magnetReceived = false;
    }

    void Update()
{
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);
        float swipeSensitivity = 10f;

        // Yatay kaydýrmalar
        if (touch.deltaPosition.x > swipeSensitivity)
        {
            right = false;
            left = true;
        }
        if (touch.deltaPosition.x < -swipeSensitivity)
        {
            right = true;
            left = false;
        }

        // Dikey kaydýrma (zýplama)
        if (touch.deltaPosition.y > 50 && !isJump)
        {
            Jump();
        }
    }

    // Hafif yumuþak ama hýzlý saða sola geçiþ
    float targetX = transform.position.x;
    if (right) targetX = -0.5f;
    if (left) targetX = 0.5f;

    transform.position = Vector3.Lerp(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), laneChangeSpeed * Time.deltaTime);

    // Karakterin sürekli ileri gitmesi
    transform.Translate(0, 0, runSpeed * Time.deltaTime);
}

void Jump()
{
    anim.SetTrigger("jump");

    // Sadece dikey hýzda deðiþiklik yaparak zýplama iþlemi
    rb.velocity = new Vector3(rb.velocity.x, jumpPower, rb.velocity.z);
}

}
