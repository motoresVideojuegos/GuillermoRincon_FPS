using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartButton(){
        SceneManager.LoadScene(0);
    }
    
    public void menuButton(){

    }

    public void exitButton(){
        Application.Quit();
    }
}
