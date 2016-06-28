using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	public void LoadSceneM(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void Pause()
    {
        if (Time.timeScale > 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
