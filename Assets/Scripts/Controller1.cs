using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Controller1 : MonoBehaviour
{
    public GameObject core;
    public GameObject[] faces;
    public GameObject[] edges;
    public GameObject[] vertices;

    public Button helpA;
    public GameObject CanvaA;
    public GameObject htextA;
    public GameObject warning;

    public Button again, quit;
    public GameObject canvaQ;

    Vector3[] vposo;
    Vector3[] vposf;
    Vector3[] eposo;
    Vector3[] eposf;
    Vector3[] fposo;
    Vector3[] fposf;
    Vector3 cposo;
    Vector3 cposf;

    bool move, rbool;
    bool b1, b2, b3, b4, bw;
    bool vf, ef, ff, cf, vb, eb, fb, cb;
    List<int> stat;

    float elapsedTime = 0f;
    float duration = 2f;
    float percentage;

    void Start()
    {
        this.GetComponent<Controllerx>().enabled = false;

        Debug.Log("Hi fam 1");
        helpA.onClick.AddListener(helpfunc2);
        again.onClick.AddListener(againfunc);
        quit.onClick.AddListener(quitfunc);
        CanvaA.SetActive(true);
        htextA.SetActive(false);
        warning.SetActive(false);
        canvaQ.SetActive(false);

        vposo = new Vector3[8];
        vposf = new Vector3[8];
        eposo = new Vector3[12];
        eposf = new Vector3[12];
        fposo = new Vector3[6];
        fposf = new Vector3[6];

        move = true; bw = false;
        b1 = false; b2 = false; b3 = false; b4 = false;
        vf = false; ef = false; ff = false; cf = false; vb = false; eb = false; fb = false; cb = false;

        stat = new List<int>();
        stat.Add(0);

        for(int i = 0; i < vertices.Length; i++)
        {
            vposo[i] = vertices[i].transform.position / 2;
            vposf[i] = vertices[i].transform.position;
        }

        for (int i = 0; i < edges.Length; i++)
        {
            eposo[i] = edges[i].transform.position / 2;
            eposf[i] = edges[i].transform.position;
        }

        for (int i = 0; i < faces.Length; i++)
        {
            fposo[i] = faces[i].transform.position / 2;
            fposf[i] = faces[i].transform.position;
        }

        cposo = core.transform.position;
        cposf = core.transform.position;
        cposo.y = core.transform.position.y - 1;
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

    public void firstfuncA()
    {
        this.GetComponent<Controllerx>().enabled = false;

        Debug.Log("Hi fam 1");
        helpA.onClick.AddListener(helpfunc2);
        again.onClick.AddListener(againfunc);
        quit.onClick.AddListener(quitfunc);
        CanvaA.SetActive(true);
        htextA.SetActive(false);
        warning.SetActive(false);
        canvaQ.SetActive(false);

        move = true; bw = false;
        b1 = false; b2 = false; b3 = false; b4 = false;
        vf = false; ef = false; ff = false; cf = false; vb = false; eb = false; fb = false; cb = false;
    }

    void againfunc()
    {
        this.GetComponent<Controllerx>().enabled = true;
        canvaQ.SetActive(false);
        CanvaA.SetActive(false);
        this.GetComponent<Controllerx>().firstfunc();
        stat.Clear();
        stat.Add(0);
    }

    void quitfunc()
    {
        UnityEngine.Device.Application.Quit();
    }

    void Update()
    {
        bw = false;
        for (int i = 0; i < stat.Count; i++)
        {
            if (stat[i] != i)
            {
                bw = true; break;
            }
        }
        if (bw)
        {
            warning.SetActive(true);
        }
        else
        {
            warning.SetActive(false);
            if (stat.Count == 5)
            {
                CanvaA.SetActive(false);
                canvaQ.SetActive(true);
            }
        }
        if (move)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 pos = hit.transform.position;
                    //Debug.Log(hit.transform.position + " " + b1 + " " + b2 + " " + b3 + " " + b4);
                    float sum = pos.x + pos.y + pos.z;
                    if (pos.y == 1 || (pos.x == 0 && pos.y == 0 && pos.z == 0))
                    {
                        elapsedTime = 0;
                        if (b4) cf = true;
                        else cb = true;
                    }
                    else if ((pos.x == 0 && pos.y == 0) || (pos.y == 0 && pos.z == 0) || (pos.z == 0 && pos.x == 0))
                    {
                        elapsedTime = 0;
                        if (b3) ff = true;
                        else fb = true;
                    }
                    else if (pos.x == 0 || pos.y == 0 || pos.z == 0)
                    {
                        elapsedTime = 0;
                        if (b2) ef = true;
                        else eb = true;
                    }
                    else if (pos.x != 0 && pos.y != 0 && pos.z != 0)
                    {
                        elapsedTime = 0;
                        if (b1) vf = true;
                        else vb = true;
                    }
                }
            }
        }
        elapsedTime += Time.deltaTime;
        percentage = elapsedTime / duration;
        if (vf)
        {
            move = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].transform.position = Vector3.Lerp(vposo[i], vposf[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                vf = false;
                b1 = false;
                stat.Remove(4);
            }
        }
        if (ef)
        {
            move = false;
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].transform.position = Vector3.Lerp(eposo[i], eposf[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                ef = false;
                b2 = false;
                stat.Remove(3);
            }
        }
        if (ff)
        {
            move = false;
            for (int i = 0; i < faces.Length; i++)
            {
                faces[i].transform.position = Vector3.Lerp(fposo[i], fposf[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                ff = false;
                b3 = false;
                stat.Remove(2);
            }
        }
        if (cf)
        {
            move = false;
            core.transform.position = Vector3.Lerp(cposo, cposf, percentage);
            if (percentage >= 1)
            {
                move = true;
                cf = false;
                b4 = false;
                stat.Remove(1);
            }
        }
        if (vb)
        {
            move = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i].transform.position = Vector3.Lerp(vposf[i], vposo[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                vb = false;
                b1 = true;
                stat.Add(4);
            }
        }
        if (eb)
        {
            move = false;
            for (int i = 0; i < edges.Length; i++)
            {
                edges[i].transform.position = Vector3.Lerp(eposf[i], eposo[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                eb = false;
                b2 = true;
                stat.Add(3);
            }
        }
        if (fb)
        {
            move = false;
            for (int i = 0; i < faces.Length; i++)
            {
                faces[i].transform.position = Vector3.Lerp(fposf[i], fposo[i], percentage);
            }
            if (percentage >= 1)
            {
                move = true;
                fb = false;
                b3 = true;
                stat.Add(2);
            }
        }
        if (cb)
        {
            move = false;
            core.transform.position = Vector3.Lerp(cposf, cposo, percentage);
            if (percentage >= 1)
            {
                move = true;
                cb = false;
                b4 = true;
                stat.Add(1);
            }
        }
    }
}
