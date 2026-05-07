using UnityEngine;

public class Move : MonoBehaviour
{
    const float margin = 2f;

    public float speed;

    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        float halfW = cam.orthographicSize * cam.aspect;
        float leftEdge = cam.transform.position.x - halfW - margin;
        if (transform.position.x < leftEdge)
            Destroy(gameObject);
        
    }
}
