using UnityEngine;

public class Ground : MonoBehaviour
{
    public float scrollSpeed = 5f; 
    Transform[] seg;  
    float width;     
    int leftIndex;
    int rightIndex;

    void Start()
    {
        seg = new Transform[] {
            transform.Find("Ground_01"),
            transform.Find("Ground_02"),
            transform.Find("Ground_03"),
            transform.Find("Ground_04")
        };
        width = seg[1].position.x - seg[0].position.x;
        leftIndex = 0;
        rightIndex = 3;
    }

    void Update()
    {
        float dx = scrollSpeed * Time.deltaTime;
        for (int i = 0; i < seg.Length; i++)
            seg[i].Translate(new Vector3(-dx, 0, 0));

        if (seg[leftIndex].position.x < -width)
        {
            seg[leftIndex].position = new Vector3(seg[rightIndex].position.x + width, seg[leftIndex].position.y, seg[leftIndex].position.z);
            leftIndex = (leftIndex + 1) % seg.Length;
            rightIndex = (rightIndex + 1) % seg.Length;
        }
    }
    

}
