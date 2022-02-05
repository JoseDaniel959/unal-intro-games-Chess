using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gamehandler : MonoBehaviour
{

    public List<GameObject> obj;
    public float offset_cas; //Distancia entre casilleros9
    public Color color_b; //White
    public Color color_n; //Black
    public bool turno = false; //False E1, True E2
    public bool pieza_claim = false; //Reclamar pieza
    public bool fin_del_juego = false;

    public GameObject p_target; //Pieza actual seleccionada


    // Use this for initialization
    void Start()
    {
        iniciar_partida();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void iniciar_partida()
    {
        //Creacion de casilleros
        bool p_color = false;
        for (int i = 0; i < 8; i++)
        {
            string nombre = "";
            switch (i)
            {
                case 0:
                    nombre += "A";
                    break;

                case 1:
                    nombre += "B";
                    break;

                case 2:
                    nombre += "C";
                    break;

                case 3:
                    nombre += "D";
                    break;

                case 4:
                    nombre += "E";
                    break;

                case 5:
                    nombre += "F";
                    break;

                case 6:
                    nombre += "G";
                    break;

                case 7:
                    nombre += "H";
                    break;
            }
            p_color = !p_color;
            for (int j = 0; j < 8; j++)
            {
                string nombre_provis = nombre + (j + 1).ToString();
                p_color = !p_color;
                GameObject newCas = Instantiate(obj[0]); //Se crea una copia de el casillero
                newCas.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(j * offset_cas, 0, i * offset_cas);
                if (p_color)
                {
                    newCas.GetComponent<MeshRenderer>().material.SetColor("_Color", color_n);
                }
                else
                {
                    newCas.GetComponent<MeshRenderer>().material.SetColor("_Color", color_b);
                }

                newCas.name = nombre_provis;
            }
        }
        crear_piezas();
    }


    void crear_piezas()
    {

        char letra = 'A';
        int num = 2;

        GameObject padre_juego = GameObject.Find("Juego");

        //Peones
        for (int i = 0; i < 8; i++)
        {
            GameObject newPeon = Instantiate(obj[1], padre_juego.transform);
            newPeon.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(offset_cas * 1, 0.02f, offset_cas * i);
            newPeon.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
            letra++;
        }

        letra = 'A';
        num = 7;

        for (int i = 0; i < 8; i++)
        {
            GameObject newPeon = Instantiate(obj[1], padre_juego.transform);
            newPeon.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(offset_cas * 6, 0.02f, offset_cas * i);
            newPeon.tag = "E2";
            newPeon.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
            letra++;
            foreach (Transform hijo in newPeon.transform)
            {
                foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
                {
                    mat.SetColor("_Color", color_n);
                }
            }
        }


        //Torres A
        letra = 'A';
        num = 1;
        GameObject newTorre = Instantiate(obj[2], padre_juego.transform);
        newTorre.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, 0);
        newTorre.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newTorre.name = "TorreDB";

        letra = 'H';
        newTorre = Instantiate(obj[2], padre_juego.transform);
        newTorre.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 7);
        newTorre.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newTorre.name = "TorreIB";

        //Torres B
        letra = 'A';
        num = 8;
        newTorre = Instantiate(obj[2], padre_juego.transform);
        newTorre.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, 0);
        newTorre.tag = "E2";
        newTorre.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newTorre.name = "TorreDN";
        foreach (Transform hijo in newTorre.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }
        letra = 'H';
        newTorre = Instantiate(obj[2], padre_juego.transform);
        newTorre.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 7);
        newTorre.tag = "E2";
        newTorre.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newTorre.name = "TorreIN";
        foreach (Transform hijo in newTorre.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }

        //Caballos A
        letra = 'B';
        num = 1;
        GameObject newCaballo = Instantiate(obj[3], padre_juego.transform);
        newCaballo.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 1);
        newCaballo.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newCaballo = Instantiate(obj[3], padre_juego.transform);
        letra = 'G';
        newCaballo.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 6);
        newCaballo.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        //Caballos B
        letra = 'B';
        num = 8;
        newCaballo = Instantiate(obj[3], padre_juego.transform);
        newCaballo.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 1);
        newCaballo.tag = "E2";
        newCaballo.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        foreach (Transform hijo in newCaballo.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }
        letra = 'G';
        newCaballo = Instantiate(obj[3], padre_juego.transform);
        newCaballo.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 6);
        newCaballo.tag = "E2";
        newCaballo.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        foreach (Transform hijo in newCaballo.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }

        //Alfiles A
        num = 1;
        letra = 'C';
        GameObject newAlfiles = Instantiate(obj[4], padre_juego.transform);
        newAlfiles.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 2);
        newAlfiles.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        letra = 'F';
        newAlfiles = Instantiate(obj[4], padre_juego.transform);
        newAlfiles.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 5);
        newAlfiles.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        //Alfiles B
        num = 8;
        letra = 'C';
        newAlfiles = Instantiate(obj[4], padre_juego.transform);
        newAlfiles.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 2);
        newAlfiles.tag = "E2";
        newAlfiles.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        foreach (Transform hijo in newAlfiles.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }

        letra = 'F';
        newAlfiles = Instantiate(obj[4], padre_juego.transform);
        newAlfiles.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 5);
        newAlfiles.tag = "E2";
        newAlfiles.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();


        foreach (Transform hijo in newAlfiles.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }

        //Reina A
        num = 1;
        letra = 'E';
        GameObject newReina = Instantiate(obj[6], padre_juego.transform);
        newReina.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 4);
        newReina.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        //Reina B
        num = 8;
        newReina = Instantiate(obj[6], padre_juego.transform);
        newReina.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 4);
        newReina.tag = "E2";
        newReina.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();

        foreach (Transform hijo in newReina.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }

        //Rey A
        num = 1;
        letra = 'D';
        GameObject newRey = Instantiate(obj[5], padre_juego.transform);
        newRey.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(0, 0.02f, offset_cas * 3);
        newRey.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newRey.name = "ReyB";

        //Rey B
        num = 8;
        newRey = Instantiate(obj[5], padre_juego.transform);
        newRey.transform.position = GameObject.Find("Spawn_C").transform.position + new Vector3(7 * offset_cas, 0.02f, offset_cas * 3);
        newRey.tag = "E2";
        newRey.GetComponent<pieza>().pos_act = letra.ToString() + num.ToString();
        newRey.name = "ReyN";

        foreach (Transform hijo in newRey.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", color_n);
            }
        }


    }

    public void cambiar_turno()
    {
        if(!check_rey_can_move()) //Chequea si el rey puede moverse
        {
            GameObject.Find("ganador").GetComponent<Text>().text = "AHOGADO";
            fin_juego();
        }
        check_jaque();
        turno = !turno; //Se le asigna el contrario valor
        check_jaque();
        p_target = null; //Vaciamos el target
    }


    public bool check_jaque()
    {
        GameObject rey;

        if (!turno)
        {
            rey = GameObject.Find("ReyN");
            rey.GetComponent<BoxCollider>().enabled = false;
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E1"))
            {
                if (check_j2(p, rey))
                {
                    StartCoroutine("cambiar_color", rey);
                    rey.GetComponent<BoxCollider>().enabled = true;
                    rey.GetComponent<rey>().jaque = true;
                    return true;
                }
            }
        }
        else
        {
            rey = GameObject.Find("ReyB");
            rey.GetComponent<BoxCollider>().enabled = false;
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E2"))
            {
                if (check_j2(p, rey))
                {
                    StartCoroutine("cambiar_color", rey);
                    rey.GetComponent<BoxCollider>().enabled = true;
                    rey.GetComponent<rey>().jaque = true;
                    return true;
                }
            }
        }

        rey.GetComponent<BoxCollider>().enabled = true;
        rey.GetComponent<rey>().jaque = false;
        return false;
    }

    public bool check_jaque_a()
    {
        GameObject rey;

        if (!turno)
        {
            rey = GameObject.Find("ReyN");
            rey.GetComponent<BoxCollider>().enabled = false;
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E1"))
            {
                if (check_j2(p, rey))
                {

                    rey.GetComponent<BoxCollider>().enabled = true;
                    rey.GetComponent<rey>().jaque = true;
                    return true;
                }
            }
        }
        else
        {
            rey = GameObject.Find("ReyB");
            rey.GetComponent<BoxCollider>().enabled = false;
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E2"))
            {
                if (check_j2(p, rey))
                {

                    rey.GetComponent<BoxCollider>().enabled = true;
                    rey.GetComponent<rey>().jaque = true;
                    return true;
                }
            }
        }

        rey.GetComponent<BoxCollider>().enabled = true;
        rey.GetComponent<rey>().jaque = false;
        return false;
    }

    public bool check_j2(GameObject p1, GameObject p2)
    {
        Vector3 t = new Vector3(p1.transform.position.x, p2.transform.position.y, p1.transform.position.z);
        Vector3 direccion = (p2.transform.position - t).normalized;
        float distancia = (p2.GetComponent<pieza>().transform.position - t).magnitude;
        RaycastHit[] col = Physics.RaycastAll(p2.GetComponent<pieza>().transform.position, -direccion, distancia);

        if (p1.GetComponent<pieza>().pieza_n == 2) //Si es caballo
        {
            return check_pieza_jaque_mov(p1, p2);
        }

        if (col.Length > 1)
        {
            return false;
        }
        else
        {
            return check_pieza_jaque_mov(p1, p2);
        }
    }


    public bool check_pieza_jaque_mov(GameObject p1, GameObject p2)
    {
        if (p1.GetComponent<pieza>().pieza_n == 0) //Es peon
            return GameObject.Find(p2.GetComponent<pieza>().pos_act).GetComponent<casillero>().check_peon_come(p1);
        else
            return GameObject.Find(p2.GetComponent<pieza>().pos_act).GetComponent<casillero>().check_move(p1);
    }

    public IEnumerator cambiar_color(GameObject rey)
    {
        Color c; //Color original
        if (rey.tag == "E1")
            c = color_b;
        else
            c = color_n;


        foreach (Transform hijo in rey.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", Color.red);
            }
        }

        yield return new WaitForSeconds(1.0f);

        foreach (Transform hijo in rey.transform)
        {
            foreach (Material mat in hijo.GetComponent<MeshRenderer>().materials)
            {
                mat.SetColor("_Color", c);
            }
        }
    }

    public void fin_juego()
    {
        fin_del_juego = true;
        Invoke("restart_game", 1.0f);
    }

    void restart_game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public bool check_rey_can_move()
    {

        if (!turno)
        {
            if (GameObject.FindGameObjectsWithTag("E1").Length == 1) //Solo queda el rey
            {
                GameObject r = GameObject.FindGameObjectWithTag("E1");

                return rey_can_move_mm(r);
            }
            else
            {
                return true;
            }

        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("E2").Length == 1)
            {
                GameObject r = GameObject.FindGameObjectWithTag("E2");

                return rey_can_move_mm(r);
            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public bool rey_can_move_mm(GameObject r)
    {
        double px;
        char py;
        char pyn;

        px = char.GetNumericValue(r.GetComponent<pieza>().pos_act[1]);
        py = r.GetComponent<pieza>().pos_act[0];

        double pxn = px;
        pyn = py;
        pxn++;

        if (r.GetComponent<pieza>().pieza_n == 5)
        {
            if (check_c_a(r, px, py, pxn, pyn))
                return true;
        }

        pxn = px;
        pxn--;

        if (r.GetComponent<pieza>().pos_act[1] != '1')
        {
            if (check_c_a(r, px, py, pxn, pyn))
                return true;
        }

        pxn = px;
        pyn--;

        if (r.GetComponent<pieza>().pos_act[0] != 'A')
        {
            if (check_c_a(r, px, py, pxn, pyn))
                return true;

            pxn++;

            if (check_c_a(r, px, py, pxn, pyn))
                return true;

            pxn--;
            pxn--;

            if (check_c_a(r, px, py, pxn, pyn))
                return true;
        }

        pxn = px;
        pyn = py;
        pyn++;

        if (r.GetComponent<pieza>().pos_act[0] != 'H')
        {
            if (check_c_a(r, px, py, pxn, pyn))
                return true;

            pxn++;

            if (check_c_a(r, px, py, pxn, pyn))
                return true;

            pxn--;
            pxn--;

            if (check_c_a(r, px, py, pxn, pyn))
                return true;
        }

        return false;
    }



    public bool check_c_a(GameObject r, double px, char py, double pxn, char pyn) //Chequeo de casillero de ahogado
    {


        //Chequeo arriba
        if (r.GetComponent<pieza>().pos_act[1] != '8')
        {
            
            GameObject p_p = null; //Pieza provisoria en casillero a mover
            bool cas_ocupado = false;

            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E1"))
            {
                if (p.GetComponent<pieza>().pos_act == pyn.ToString() + pxn.ToString())
                {
                    cas_ocupado = true;
                }
            }


            foreach (GameObject p in GameObject.FindGameObjectsWithTag("E2"))
            {
                if (p.GetComponent<pieza>().pos_act == pyn.ToString() + pxn.ToString())
                {
                    p_p = p;
                    p.GetComponent<BoxCollider>().enabled = false;
                }
            }

            if (!cas_ocupado && GameObject.Find(pyn.ToString() + pxn.ToString()).GetComponent<casillero>().check_move(r))
            {
                r.transform.position = GameObject.Find(pyn.ToString() + pxn.ToString()).transform.position;
                r.GetComponent<pieza>().pos_act = pyn.ToString() + pxn.ToString();

                if (!check_jaque_a())
                {
                    if (p_p != null)
                        p_p.GetComponent<BoxCollider>().enabled = true;
                    r.transform.position = GameObject.Find(py.ToString() + px.ToString()).transform.position;
                    r.GetComponent<pieza>().pos_act = py.ToString() + px.ToString();
                    return true; //Se puede mover
                }
                r.transform.position = GameObject.Find(py.ToString() + px.ToString()).transform.position;
                r.GetComponent<pieza>().pos_act = py.ToString() + px.ToString();
            }

            if (p_p != null)
                p_p.GetComponent<BoxCollider>().enabled = true;

            return false;
        }

        return false;
    }
}
