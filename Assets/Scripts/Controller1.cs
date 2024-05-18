using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class Controller1 : MonoBehaviour
{
    public Button next, back, ready, help;
    public GameObject canva;
    public GameObject htext;
    public GameObject core;
    public GameObject[] faces;
    public GameObject[] edges;
    public GameObject[] vertices;
    public float speed = 1.0f;

    public Button helpA;
    public GameObject CanvaA;
    public GameObject htextA;
    public GameObject warning;

    public Button again, quit;
    public GameObject canvaQ;

    int ptr;
    bool nextStage;
    Vector3[] vposo;
    Vector3[] vposf;
    Vector3[] eposo;
    Vector3[] eposf;
    Vector3[] fposo;
    Vector3[] fposf;
    Vector3 cposo;
    Vector3 cposf;

    bool b1, b2, b3, b4, bw;
    List<int> stat;

    void Start()
    {
        nextStage = false;

        Debug.Log("Hi fam");
        next.onClick.AddListener(nextfunc);
        back.onClick.AddListener(backfunc);
        ready.onClick.AddListener(readyfunc);
        help.onClick.AddListener(helpfunc);
        helpA.onClick.AddListener(helpfunc2);
        again.onClick.AddListener(againfunc);
        quit.onClick.AddListener(quitfunc);
        htext.SetActive(false);
        CanvaA.SetActive(false);
        htextA.SetActive(false);
        warning.SetActive(false);
        canvaQ.SetActive(false);
        ptr = 0;

        vposo = new Vector3[8];
        vposf = new Vector3[8];
        eposo = new Vector3[12];
        eposf = new Vector3[12];
        fposo = new Vector3[6];
        fposf = new Vector3[6];

        b1 = true;
        b2 = true;
        b3 = true;
        b4 = true;
        bw = false;

        stat = new List<int>();
        stat.Add(0); stat.Add(1); stat.Add(2); stat.Add (3); stat.Add(4);

        for(int i = 0; i < vertices.Length; i++)
        {
            vposo[i] = 1 * vertices[i].transform.position;
            vposf[i] = 2 * vertices[i].transform.position;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            eposo[i] = 1 * edges[i].transform.position;
            eposf[i] = 2 * edges[i].transform.position;
        }

        for (int i = 0; i < faces.Length; i++)
        {
            fposo[i] = 1 * faces[i].transform.position;
            fposf[i] = 2 * faces[i].transform.position;
        }

        cposo = core.transform.position;
        cposf = core.transform.position;
        cposf.y = core.transform.position.y + 1;
    }

    void fvertices()
    {
        //Debug.Log("fvertices");
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].transform.position = vposf[i];
        }
        b1 = false;
        stat.Remove(4);
    }

    void fedges()
    {
        //Debug.Log("fedges");
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].transform.position = eposf[i];
        }
        b2 = false;
        stat.Remove(3);
    }

    void ffaces()
    {
        //Debug.Log("ffaces");
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].transform.position = fposf[i];
        }
        b3 = false;
        stat.Remove(2);
    }

    void fcore()
    {
        //Debug.Log("fcore");
        core.transform.position = cposf;
        b4 = false;
        stat.Remove(1);
    }

    void bvertices()
    {
        //Debug.Log("bvertices");
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].transform.position = vposo[i];
        }
        b1 = true;
        stat.Add(4);
    }

    void bedges()
    {
        //Debug.Log("bedges");
        for (int i = 0; i < edges.Length; i++)
        {
            edges[i].transform.position = eposo[i];
        }
        b2 = true;
        stat.Add(3);
    }

    void bfaces()
    {
        //Debug.Log("bfaces");
        for (int i = 0; i < faces.Length; i++)
        {
            faces[i].transform.position = fposo[i];
        }
        b3 = true;
        stat.Add(2);
    }

    void bcore()
    {
        //Debug.Log("bcore");
        core.transform.position = cposo;
        b4 = true;
        stat.Add(1);
    }

    void nextfunc()
    {
        Debug.Log(ptr);
        if (ptr == 0)
        {
            fvertices();
            ptr++;
        }
        else if (ptr == 1)
        {
            fedges();
            ptr++;
        }
        else if (ptr == 2)
        {
            ffaces();
            ptr++;
        }
        else if(ptr == 3)
        {
            fcore();
            ptr++;
        }
    }

    void backfunc()
    {
        if (ptr == 1)
        {
            bvertices();
            ptr--;
        }
        else if (ptr == 2)
        {
            bedges();
            ptr--;
        }
        else if (ptr == 3)
        {
            bfaces();
            ptr--;
        }
        else if (ptr == 4)
        {
            bcore();
            ptr--;
        }
    }

    void readyfunc()
    {
        while (ptr < 4)
        {
            nextfunc();
        }
        canva.SetActive(false);
        CanvaA.SetActive(true);
        nextStage = true;
    }

    void helpfunc()
    {
        if (htext.active)
        {
            htext.SetActive(false);
        }
        else
        {
            htext.SetActive(true);
        }
    }

    void helpfunc2()
    {
        if (htextA.active)
        {
            htextA.SetActive(false);
        }
        else
        {
            htextA.SetActive(true);
        }
    }

    void againfunc()
    {
        canva.SetActive(true);
        canvaQ.SetActive(false);
        nextStage = false;
    }

    void quitfunc()
    {
        Application.Quit();
    }

    void Update()
    {
        if(nextStage)
        {
            //Debug.Log("yeeeeeee");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                bw = false;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 pos = hit.transform.position;
                    //Debug.Log(hit.transform.position + " " + b1 + " " + b2 + " " + b3 + " " + b4);
                    float sum = pos.x + pos.y + pos.z; 
                    if (pos.y == 1 || (pos.x == 0 && pos.y == 0 && pos.z ==0))
                    {
                        if(b4) fcore();
                        else bcore();
                    }
                    else if ((pos.x == 0 && pos.y == 0) || (pos.y == 0 && pos.z == 0) || (pos.z == 0 && pos.x == 0))
                    {
                        if(b3) ffaces();
                        else bfaces();
                    }
                    else if(pos.x == 0 || pos.y == 0 || pos.z == 0)
                    {
                        if(b2) fedges();
                        else bedges();
                    }
                    else if(pos.x != 0 && pos.y != 0 && pos.z != 0)
                    {
                        if(b1) fvertices();
                        else bvertices();
                    }
                    for(int i = 0; i < stat.Count; i++)
                    {
                        if (stat[i] != i) bw = true;
                    }
                    if(bw)
                    {
                        warning.SetActive(true);
                    }
                    else
                    {
                        warning.SetActive (false);
                        if(stat.Count == 5)
                        {
                            CanvaA.SetActive(false);
                            canvaQ.SetActive(true);
                        }
                    }
                    //hit.transform.position = hit.transform.position / 2;
                    /*if(vertices.Contains(hit.transform.name))
                    {
                        Debug.Log("Mazaaaaa");
                    }
                    Debug.Log(hit.transform.name + " " + hit.distance);*/
                }
            }
        }
    }
}
