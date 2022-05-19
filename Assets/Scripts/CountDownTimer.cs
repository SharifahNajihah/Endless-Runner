//using System.Collections;
//using UnityEngine;
//using UnityEngine.UI;

//public class CountDownTimer : MonoBehaviour {
//    public string levelToLoad;
//    public float timer = 10f;
//    private Text timerSeconds;
//    public DeathMenu theDeathScreen;

//    private GameManager theGameManager;

//    // Start is called before the first frame update
//    void Start() {
//        timerSeconds = GetComponent<Text>();
//        theGameManager = FindObjectOfType<GameManager>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        timer -= Time.deltaTime;

//        if(theGameManager.timerReset)
//        {
//            timer = 10;
//            theGameManager.timerReset = false;
//        }
//        timerSeconds.text = timer.ToString("f0");
//        if (timer <= 0)
//        {
//            Application.LoadLevel(levelToLoad);
//        }
//    }
//}
