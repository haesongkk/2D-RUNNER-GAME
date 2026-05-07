using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    AudioSource asrc;
    ParticleSystem ps;


    bool onUpside = false;

    public int hp = 3;
    int score = 0;
    bool bUpdate = false;


    public int GetScore() { return score; }

    void Start()
    {
        anim = GetComponent<Animator>();
        asrc = GetComponent<AudioSource>();
        ps = transform.Find("BloodEffect").GetComponent<ParticleSystem>();
    }

    public void Init()
    {
        bUpdate = true;
        score = 0;
        hp = 3;
    }

    public void Final()
    {
        bUpdate = false;
    }


    void Update()
    {
        if (!bUpdate) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            asrc.Play();
            if (onUpside) anim.SetTrigger("DESCEND");
            else anim.SetTrigger("ASCEND");
            onUpside = !onUpside;
        }

    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!bUpdate) return;

        if (collision.tag == "Item")
        {
            Destroy(collision.gameObject);
            GameManager.Instance.Score(++score);
        } 
        if (collision.tag == "Obstacle")
        {
            ps.Play(true);
            Destroy(collision.gameObject);
            GameManager.Instance.HP(--hp);
        }
    }

}
