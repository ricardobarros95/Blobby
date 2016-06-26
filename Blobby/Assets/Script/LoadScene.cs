using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void LoadSceneM()
    {
        Application.LoadLevel(1);
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
