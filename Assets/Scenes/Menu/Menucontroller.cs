using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menucontroller : MonoBehaviour
{
    public void ChangeScene(string _scene_julien)
    {
        SceneManager.LoadScene(_scene_julien);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
