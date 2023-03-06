using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScenaUnJugador()
    {
        SceneManager.LoadScene("ScenaUnJugador");
    }
    public void ScenaMultiJugador()
    {
        SceneManager.LoadScene("ScenaMultiJugador");
    }

    public void SceneSalir()
    {
        Application.Quit();
    }
}
