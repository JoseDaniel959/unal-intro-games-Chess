using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class casillero : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    private void OnMouseUp() //Si se suelta boton mouse sobre este objeto
    {
        if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target)
        {
            bool puede_mover = true;
            GameObject target = GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target;
            Vector3 pos_comp = transform.position + new Vector3(0, 0.02f, 0);
            foreach (Transform hijo in GameObject.Find("Juego").transform)
            {
                if (pos_comp == hijo.transform.position) //Ya había un objeto en el casillero al que me quiero mover
                {
                    if (target.tag == hijo.tag) //Pertenecen al mismo equipo
                    {
                        puede_mover = false;
                    }
                    else //Si hay un enemigo
                    {
                        Destroy(hijo.gameObject);
                        puede_mover = true;
                    }
                }
            }

            if(target.GetComponent<pieza>().pieza_n == 5) //Si es rey
            {
                check_enroque(target);
            }

            if (puede_mover)
            {
                if (check_move(target))
                {

                    //MOVIMIENTO
                    target.transform.position = transform.position + new Vector3(0, 0.02f, 0);

                    target.GetComponent<pieza>().pos_act = name;

                    if (target.GetComponent<pieza>().pieza_n == 5 || target.GetComponent<pieza>().pieza_n == 1)
                    {
                        if (target.GetComponent<pieza>().pieza_n == 5)
                        {
                            target.GetComponent<rey>().movio = true;
                        }
                        else
                        {
                            target.GetComponent<torre>().movio = true;
                        }
                    }

                    bool next_turn = true;

                    if (GameObject.Find("select"))
                    {
                        Destroy(GameObject.Find("select"));
                    }

                    if (GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().pieza_n == 0)
                    {
                        if(GameObject.Find("GameHandler").GetComponent<gamehandler>().p_target.GetComponent<pieza>().tag == "E1")
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

                    if(next_turn)
                       GameObject.Find("GameHandler").GetComponent<gamehandler>().cambiar_turno();


                }
            }
        }

    }


    public bool check_move(GameObject pe)
    {
        double px;
        char py;
        double cx;
        char cy;
        int dif;

        switch (pe.GetComponent<pieza>().pieza_n)
        {
            case 0: //Peon
                double pp = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                double cp = char.GetNumericValue(name[1]);

                if (pe.tag == "E1")
                {
                    if (cp == (pp + 1))
                    {
                        if (pe.GetComponent<pieza>().pos_act[0] == name[0])
                        {
                            return true;
                        }
                    }
                    else if (pp == 2 && (cp == pp + 2))
                    {
                        return check_casillero(pe);
                    }
                }
                else
                {
                    if (cp == (pp - 1))
                    {
                        if (pe.GetComponent<pieza>().pos_act[0] == name[0])
                        {
                            return true;
                        }
                    }
                    else if (pp == 7 && (cp == pp - 2))
                    {
                        return check_casillero(pe);
                    }
                }


                break;

            case 1: //Torre
                px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                py = (pe.GetComponent<pieza>().pos_act[0]);

                cx = char.GetNumericValue(name[1]);
                cy = name[0];

                if (((px == cx) && (py != cy)) || ((py == cy) && (px != cx)))//Un eje es igual
                {
                    return check_casillero(pe);
                }

                break;

            case 2: //Caballo
                px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                py = (pe.GetComponent<pieza>().pos_act[0]);

                cx = char.GetNumericValue(name[1]);
                cy = name[0];

                dif = (int)px - (int)cx;

                if (Mathf.Abs(dif) > 2 || Mathf.Abs(dif) == 0)
                {
                    return false;
                }
                else
                {
                    if (Mathf.Abs(dif) == 1)
                    {
                        if (Mathf.Abs(py - cy) == 2)
                        {
                            return check_casillero_c(pe);
                        }
                    }
                    else if (Mathf.Abs(dif) == 2)
                    {
                        if (Mathf.Abs(py - cy) == 1)
                        {
                            return check_casillero_c(pe);
                        }
                    }
                }



                break;

            case 3: //Alfil
                px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                py = (pe.GetComponent<pieza>().pos_act[0]);

                cx = char.GetNumericValue(name[1]);
                cy = name[0];

                if ((px != cx) && (py != cy))
                {
                    dif = (int)px - (int)cx;

                    if (Mathf.Abs(py - cy) == Mathf.Abs(dif))
                    {
                        return check_casillero(pe);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }


                break;

            case 4: //Reina

                px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                py = (pe.GetComponent<pieza>().pos_act[0]);

                cx = char.GetNumericValue(name[1]);
                cy = name[0];


                //Compruebo horizontal vertical
                if (((px == cx) && (py != cy)) || ((py == cy) && (px != cx)))//Un eje es igual
                {
                    return check_casillero(pe);
                }

                //Compruebo diagonales
                if ((px != cx) && (py != cy))
                {
                    dif = (int)px - (int)cx;

                    if (Mathf.Abs(py - cy) == Mathf.Abs(dif))
                    {
                        return check_casillero(pe);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                break;

            case 5: //Rey

                px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
                py = (pe.GetComponent<pieza>().pos_act[0]);

                cx = char.GetNumericValue(name[1]);
                cy = name[0];

                dif = (int)px - (int)cx;

                if (Mathf.Abs(dif) > 1 || Mathf.Abs(py - cy) > 1)
                {
                    return false;
                }

                double px2;
                char py2;

                if (pe.name == "ReyB")
                {
                    px2 = char.GetNumericValue(GameObject.Find("ReyN").GetComponent<pieza>().pos_act[1]);
                    py2 = GameObject.Find("ReyN").GetComponent<pieza>().pos_act[0];
                }
                else
                {
                    px2 = char.GetNumericValue(GameObject.Find("ReyB").GetComponent<pieza>().pos_act[1]);
                    py2 = GameObject.Find("ReyB").GetComponent<pieza>().pos_act[0];
                }

                int dif2 = (int)px2 - (int)cx;

                if (Mathf.Abs(dif2) < 2 && Mathf.Abs(py2 - cy) < 2)
                {
                   return false;
                }
                

                //Compruebo horizontal vertical
                if (((px == cx) && (py != cy)) || ((py == cy) && (px != cx)))//Un eje es igual
                {
                    return check_casillero(pe);
                }

                //Compruebo diagonales
                if ((px != cx) && (py != cy))
                {
                    if (Mathf.Abs(py - cy) == Mathf.Abs(dif))
                    {
                        return check_casillero(pe);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                break;
        }

        return false;
    }

    public bool check_peon_come(GameObject pe)
    {

        double px;
        char py;
        double cx;
        char cy;
        int dif;

        px = char.GetNumericValue(pe.GetComponent<pieza>().pos_act[1]);
        py = (pe.GetComponent<pieza>().pos_act[0]);

        cx = char.GetNumericValue(name[1]);
        cy = name[0];

        dif = (int)px - (int)cx;

        if (Mathf.Abs(dif) > 1 || Mathf.Abs(py - cy) > 1)
        {
            return false;
        }

        //Compruebo diagonales
        if ((px != cx) && (py != cy))
        {
            if (Mathf.Abs(py - cy) == Mathf.Abs(dif))
            {
                return check_casillero(pe);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return false;
    }

    public bool check_casillero(GameObject pe)
    {
        Vector3 t = new Vector3(transform.position.x, pe.GetComponent<pieza>().transform.position.y, transform.position.z);
        Vector3 direccion = (pe.transform.position - t).normalized;
        float distancia = (pe.GetComponent<pieza>().transform.position - t).magnitude;
        RaycastHit[] col = Physics.RaycastAll(pe.GetComponent<pieza>().transform.position, -direccion, distancia);
        if (col.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool check_casillero_c(GameObject pe)
    {
        Vector3 t = new Vector3(transform.position.x, pe.GetComponent<pieza>().transform.position.y, transform.position.z);
        Vector3 direccion = (pe.GetComponent<pieza>().transform.position - t).normalized;
        float distancia = (pe.GetComponent<pieza>().transform.position - t).magnitude;
        RaycastHit[] col = Physics.RaycastAll(transform.position, new Vector3(0,1,0), distancia);
        if (col.Length > 0)
        {

            return false;
        }
        else
        {
            return true;
        }
    }

    public bool check_casillero_e(GameObject pe)
    {
        Vector3 t = new Vector3(transform.position.x, pe.GetComponent<pieza>().transform.position.y, transform.position.z);
        Vector3 direccion = (pe.GetComponent<pieza>().transform.position - t).normalized;
        float distancia = (pe.GetComponent<pieza>().transform.position - t).magnitude;
        RaycastHit[] col = Physics.RaycastAll(transform.position - new Vector3(0,2,0), new Vector3(0, 1, 0), distancia);
        if (col.Length > 1)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public bool check_enroque(GameObject t)
    {
        if(t.GetComponent<rey>().movio || t.GetComponent<rey>().jaque)
        {
            return false;
        }

        if(t.tag == "E1")
        {
            if(name == "B1")
            {
                if(!GameObject.Find("TorreDB").GetComponent<pieza>().viva || (GameObject.Find("TorreDB").GetComponent <torre>().movio))
                {
                    return false;
                }
                else
                {
                    if(GameObject.Find("C1").GetComponent<casillero>().check_casillero_e(t))
                    {
                        enroque_process(t);

                        GameObject.Find("TorreDB").transform.position = GameObject.Find("C1").transform.position + new Vector3(0, 0.02f, 0);

                        GameObject.Find("TorreDB").GetComponent<pieza>().pos_act = "C1";
                    }
                }
            }
            else if(name == "F1")
            {
                if (!GameObject.Find("TorreIB").GetComponent<pieza>().viva || (GameObject.Find("TorreIB").GetComponent<torre>().movio))
                {
                    return false;
                }
                else
                {
                    if (GameObject.Find("E1").GetComponent<casillero>().check_casillero_e(t))
                    {
                        enroque_process(t);

                        GameObject.Find("TorreIB").transform.position = GameObject.Find("E1").transform.position + new Vector3(0, 0.02f, 0);

                        GameObject.Find("TorreIB").GetComponent<pieza>().pos_act = "E1";
                    }
                }
            }
            else
            {
                return false;
            }
        }
        else //Equipo 2
        {
            if (name == "B8")
            {
                if (!GameObject.Find("TorreDN").GetComponent<pieza>().viva || (GameObject.Find("TorreDN").GetComponent<torre>().movio))
                {
                    return false;
                }
                else
                {
                    if (GameObject.Find("C8").GetComponent<casillero>().check_casillero_e(t))
                    {
                        enroque_process(t);

                        GameObject.Find("TorreDN").transform.position = GameObject.Find("C8").transform.position + new Vector3(0, 0.02f, 0);

                        GameObject.Find("TorreDN").GetComponent<pieza>().pos_act = "C8";
                    }
                }
            }
            else if (name == "F8")
            {
                if (!GameObject.Find("TorreIN").GetComponent<pieza>().viva || (GameObject.Find("TorreIN").GetComponent<torre>().movio))
                {
                    return false;
                }
                else
                {
                    if (GameObject.Find("E8").GetComponent<casillero>().check_casillero_e(t))
                    {
                        enroque_process(t);

                        GameObject.Find("TorreIN").transform.position = GameObject.Find("E8").transform.position + new Vector3(0, 0.02f, 0);

                        GameObject.Find("TorreIN").GetComponent<pieza>().pos_act = "E8";
                    }
                }
            }
            else
            {
                return false;
            }
        }


        return false;
    }

    void enroque_process(GameObject t)
    {
        t.transform.position = transform.position + new Vector3(0, 0.02f, 0);

        t.GetComponent<pieza>().pos_act = name;

        bool next_turn = true;

        if (GameObject.Find("select"))
        {
            Destroy(GameObject.Find("select"));
        }

        if (next_turn)
            GameObject.Find("GameHandler").GetComponent<gamehandler>().cambiar_turno();
    }
}
