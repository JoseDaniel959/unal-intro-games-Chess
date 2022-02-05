using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pieza : MonoBehaviour
{

    public string pos_act = " ";
    public int pieza_n; //0 Peon, 1 Torre, 2 Caballo, 3 Alfil, 4 Reina, 5 Rey
    public bool viva = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseUp() //Si se suelta boton mouse sobre esta pieza
    {
        if(!viva && GameObject.Find("GameHandler").GetComponent<gamehandler>().pieza_claim)
        {
            if((tag == "E1" && !GameObject.Find("GameHandler").GetComponent<gamehandler>().turno) || (tag == "E2" && GameObject.Find("GameHandler").GetComponent<gamehandler>().turno))
            {
                transform.position = GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.transform.position;
                Destroy(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target);
                GameObject.Find("GameHandler").GetComponent<gamehandler>().cambiar_turno();
                GameObject.Find("GameHandler").GetComponent<gamehandler>().pieza_claim = false;
            }
        }
    }
}
