using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caballo : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp() //Si se suelta boton mouse sobre esta pieza
    {
        if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target)
        {
            //SELECCION
            if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.tag == transform.tag) //Son del mismo equipo
            {
                seleccionar();
            }
            else //Son equipo contrario, se procede a comer
            {
                GetComponent<BoxCollider>().enabled = false;
                if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pieza_n == 0) //Si es un peon
                {
                    if (GameObject.Find(GetComponent<pieza>().pos_act).GetComponent<casillero>().check_peon_come(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target))
                    {
                        comer();
                    }
                }
                else if (GameObject.Find(GetComponent<pieza>().pos_act).GetComponent<casillero>().check_move(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target))
                {
                    comer();
                }
                GetComponent<BoxCollider>().enabled = true;
            }
        }
        else //Si no seleccione nada, selecciono
        {
            if ((GameObject.Find("GameHandler").GetComponent<gamehandler>().turno && transform.tag == "E2") || (!GameObject.Find("GameHandler").GetComponent<gamehandler>().turno && transform.tag == "E1"))
            {
                seleccionar();
            }


        }
    }

    void comer()
    {
        //MOVIMIENTO
        if (GameObject.Find("select"))
        {
            Destroy(GameObject.Find("select"));
        }

        GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.transform.position = transform.position;

        GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pos_act = GetComponent<pieza>().pos_act;

        bool next_turn = true;

        if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pieza_n == 0)
        {
            if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().tag == "E1")
            {
                if (char.GetNumericValue(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pos_act[1]) == 8)
                {
                    //Reclamar pieza
                    next_turn = false;
                    GameObject.Find("GameHandler").GetComponent<gamehandler>().pieza_claim = true;
                }
            }
            else
            {
                if (char.GetNumericValue(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pos_act[1]) == 1)
                {
                    //Reclamar pieza
                    next_turn = false;
                    GameObject.Find("GameHandler").GetComponent<gamehandler>().pieza_claim = true;
                }
            }
        }

        if (next_turn)
            GameObject.Find("GameHandler").GetComponent<gamehandler>().cambiar_turno();

        GameObject.Find("Cementerio").GetComponent<cementerio>().llevar_cementerio(gameObject);
    }


    void seleccionar()
    {
        GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target = gameObject;

        if (GameObject.Find("select"))
        {
            Destroy(GameObject.Find("select"));
        }

        GameObject newSelect = Instantiate(GameObject.Find("GameHandler").GetComponent<gamehandler>().obj[7]);
        newSelect.name = "select";
        newSelect.transform.position = transform.position + new Vector3(0, 6.0f, 0);
    }
}
