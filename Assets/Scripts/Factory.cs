using UnityEngine;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    public GameObject topNoteObj;
    public GameObject bottomNoteObj;

    public float noteOffset;
    public TextAsset noteJSON;


    List<float> notes;
    int nextIndex;
    float elapsed;

    Boss boss;
    Transform topTransform;
    Transform bottomTransform;
    AudioSource ar;

    bool bUpdate = false;

    void Start()
    {
        notes = JsonUtility.FromJson<NoteData>(noteJSON.text).notes;
        Debug.Log(notes.Count);

        boss = transform.Find("Boss").GetComponent<Boss>();
        topTransform = transform.Find("Top");
        bottomTransform = transform.Find("Bottom");
        ar = GetComponent<AudioSource>();
        
    }

    public void Init()
    {
        bUpdate = true;

        nextIndex = 0;
        elapsed = 0.0f;

        double t = AudioSettings.dspTime + noteOffset;
        ar.PlayScheduled(t);

        boss.Init();
    }
    
    public void Final()
    {
        bUpdate = false;
        boss.Final();

        ar.Stop();
    }

    void Update()
    {
        if (!bUpdate) return;

        elapsed += Time.deltaTime;

        if (elapsed > 20) boss.Wake();
        if (elapsed > 40) boss.Angry();
        if (elapsed > 75) boss.Die();

        if (nextIndex >= notes.Count) return;

        if (elapsed >= notes[nextIndex])
        {
            bool isTop = nextIndex++ % 2 == 0;
            Transform shot = isTop ? topTransform : bottomTransform;
            GameObject shotObj = isTop ? topNoteObj : bottomNoteObj;
            Instantiate(shotObj, shot.position, Quaternion.identity);
            boss.Fire(!isTop);
        }

        

    }
}
