using System.Collections;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public string mainMenuLevel;

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(mainMenuLevel);
    }
}
