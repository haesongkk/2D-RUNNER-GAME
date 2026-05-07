using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public GameObject topObs;
    public GameObject bottomObs;
    public float posXOffset;
    public float topPosYOffset;
    public float bottomPosYOffset;
    public float noteOffset;
    public Color angry;

    Animator anim;
    Camera cam;
    bool isSleep = true;

    float prob;

    Color[] background = new Color[2]
    {
        new Color(0.45f, 0.7f, 0.77f, 1f),
        new Color(0.0416f, 0.0856431f, 0.2264151f, 1f)
    };


    public void Final()
    {
    }
    
    public void Wake()
    {
        if (!isSleep) return;
        gameObject.SetActive(true);

        anim.SetTrigger("WALK");
        isSleep = false;
        prob = 0.4f;

    }
    public void Angry()
    {
        GetComponent<SpriteRenderer>().color = angry;
        prob = 0.9f;
        cam.backgroundColor = background[1];
    }

    public void Init()
    {
        isSleep = true;
        gameObject.SetActive(false);
        cam.backgroundColor = background[0];
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
    
    void Start()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main;
    }

    
    public void Fire(bool isTop) 
    {
        if (isSleep) return;
        var st = anim.GetCurrentAnimatorStateInfo(0);
        if (st.IsName("Base Layer.Walk")) return;

        if (UnityEngine.Random.value > prob) return;
        anim.SetTrigger("ATTACK");

        StartCoroutine(RunLater(isTop)); 
    }

    
    IEnumerator RunLater(bool isTop)
    {
        yield return new WaitForSeconds(noteOffset);

        GameObject note = isTop ? topObs : bottomObs;
        float posYOffset = isTop ? topPosYOffset : bottomPosYOffset;
        Vector2 pos = transform.position;
        pos.x = posXOffset;
        pos.y = posYOffset;
        Instantiate(note, pos, Quaternion.identity);
    }

    public void Die()
    {
        anim.SetTrigger("DIE");
        GameManager.Instance.GameOver();

    }
}
