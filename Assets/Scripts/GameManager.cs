using UnityEngine.UI;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject player;
    public GameObject ground;
    public GameObject factory;
    public GameObject GameUI;
    public GameObject TitleUI;


    bool onTitle = true;

    Image[] lifeImage;
    Slider slider;
    Text text;

    float gameTime = 70.0f;
    float elapsed = 0.0f;

    bool bDebug = false;

    Player p;
    Color[] colors = new Color[2] {
        new Color(0.3f, 0.3f, 0.3f, 0.3f),
        new Color(1, 1, 1, 1)
    };

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        lifeImage = new Image[] {
            GameUI.transform.Find("Life1").GetComponent<Image>(),
            GameUI.transform.Find("Life2").GetComponent<Image>(),
            GameUI.transform.Find("Life3").GetComponent<Image>()
        };
        text = GameUI.transform.Find("Score").GetComponent<Text>();
        slider = GameUI.transform.Find("Slider").GetComponent<Slider>();
        p = player.GetComponent<Player>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) bDebug = !bDebug;

        if (onTitle)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                onTitle = false;
                TitleUI.SetActive(false);
                player.GetComponent<Player>().Init();
                factory.GetComponent<Factory>().Init();
                elapsed = 0.0f;
                HP(3);
                Score(0);
            }
        }
        else
        {
            elapsed += Time.deltaTime;
            slider.value = elapsed / gameTime;
        }

    }

    public void HP(int hp)
    {
        if (bDebug) return;
        if (hp <= 0) hp = 0;
        for (int i = 0; i < 3; i++)
            lifeImage[i].color = colors[i < hp ? 1 : 0];
        if (hp == 0) GameOver();

    }
    
    public void Score(int score)
    {
        text.text = score.ToString();
    }

    public void GameOver()
    {
        onTitle = true;
        TitleUI.SetActive(true);
        Text t = TitleUI.transform.Find("Value_Text").GetComponent<Text>();
        t.text = p.GetScore().ToString() + "/233";

        player.GetComponent<Player>().Final();
        factory.GetComponent<Factory>().Final();
    }

}
