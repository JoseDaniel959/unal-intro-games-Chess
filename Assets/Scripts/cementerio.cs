using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cementerio : MonoBehaviour {

    public GameObject s1; //Spawn1
    public GameObject s2; //Spawn2

    public int num_s1 = 0; //Numero de pieza
    public int num_s2 = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void llevar_cementerio(GameObject pieza)
    {
        quitar_script(pieza);

        if(pieza.tag == "E1")
        {
            if (num_s1 <= 7)
            {
                pieza.transform.position = s1.transform.position - new Vector3(GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * num_s1, 0, 0);
            }
            else
            {
                pieza.transform.position = s1.transform.position - new Vector3(GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * (num_s1 - 8), 0, GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * 3);
            }
            num_s1++;
        }
        else
        {
            if (num_s2 <= 7)
            {
                pieza.transform.position = s2.transform.position - new Vector3(GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * num_s2, 0, 0);
            }
            else
            {
                pieza.transform.position = s2.transform.position - new Vector3(GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * (num_s2-8), 0, GameObject.Find("GameHandler").GetComponent<gamehandler>().offset_cas * 3);
            }
            num_s2++;
        }
    }

    public void quitar_script(GameObject pieza)
    {
        pieza.GetComponent<pieza>().viva = false;
        switch (pieza.GetComponent<pieza>().pieza_n)
        {
            case 0:
                Destroy(pieza.GetComponent<peon>());
                break;

            case 1:
                Destroy(pieza.GetComponent<torre>());
                break;

            case 2:
                Destroy(pieza.GetComponent<caballo>());
                break;

            case 3:
                Destroy(pieza.GetComponent<alfil>());
                break;

            case 4:
                Destroy(pieza.GetComponent<reina>());
                break;

            case 5:
                Destroy(pieza.GetComponent<rey>());
                break;
        }
    }
}
