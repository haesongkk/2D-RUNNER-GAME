using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteData
{
    public List<float> notes = new();
}

public class NoteMaker : MonoBehaviour
{

    public AudioClip[] audioClip;
    public int cur;

    float elapsed = 0.0f;

    NoteData cd;

    void Start()
    {
        cd = new NoteData();
        AudioSource ar = GetComponent<AudioSource>();
        ar.clip = audioClip[cur];
        ar.Play();
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            cd.notes.Add(elapsed);
            Debug.Log(elapsed);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string filename = Time.time.ToString();
            string dir = "./Assets/Notes/" + filename +".json";
            string json = JsonUtility.ToJson(cd, true);
            File.WriteAllText(dir, json, System.Text.Encoding.UTF8);
            Debug.Log($"Saved: {json}");
        }
    }
}
