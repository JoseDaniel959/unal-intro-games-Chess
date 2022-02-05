using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    


     public void EscenaJuego(){
        SceneManager.LoadScene("SampleScene");
    }

    public void EscenaJuegoIA(){
        SceneManager.LoadScene("Menu");
    }
}

