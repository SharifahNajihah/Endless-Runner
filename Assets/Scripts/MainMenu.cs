using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public string playGameLevel;
    public string playGameInstruction;
    public void PlayGame()
    {
        Application.LoadLevel(playGameLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        Application.LoadLevel(playGameInstruction);
    }

}
