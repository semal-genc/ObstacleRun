using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public float visibilityDistance = 10f; // Görünürlük mesafesi
    private int layerToTrack = 6; // HideObject layer'ýnýn indeksi
    private List<GameObject> objectsToShow = new List<GameObject>(); // Görünürlüðü kontrol edilecek nesneler listesi

    Transform childPosition;
    Vector3 distance;
    float speed = 4f;
    float fadeSpeed = 2f; // Opaklýk deðiþim hýzý

    void Start()
    {
        childPosition = GameObject.Find("Child").transform;
        FindObjectsInLayer();
    }

    void LateUpdate()
    {
        distance = new Vector3(childPosition.position.x, transform.position.y, childPosition.position.z - 1.5f);
        transform.position = Vector3.Lerp(transform.position, distance, speed * Time.deltaTime);

        foreach (GameObject obj in objectsToShow)
        {
            float distToPlayer = Vector3.Distance(childPosition.position, obj.transform.position);
            float alpha = Mathf.Clamp01(1 - (distToPlayer / visibilityDistance));
            FadeObject(obj, alpha);
        }
    }

    void FindObjectsInLayer()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == layerToTrack)
            {
                if (obj.GetComponent<Renderer>() != null)
                {
                    objectsToShow.Add(obj);
                    SetObjectAlpha(obj, 0f); // Baþlangýçta tüm nesneleri gizle
                }
            }
        }
    }

    void FadeObject(GameObject obj, float targetAlpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material mat in materials)
            {
                Color color = mat.color;
                float alpha = Mathf.Lerp(color.a, targetAlpha, fadeSpeed * Time.deltaTime);
                color.a = alpha;
                mat.color = color;
            }
        }
    }

    void SetObjectAlpha(GameObject obj, float alpha)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material[] materials = renderer.materials;
            foreach (Material mat in materials)
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }
    }
}
