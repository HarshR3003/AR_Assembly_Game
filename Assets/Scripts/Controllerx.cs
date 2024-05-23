using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Controllerx : MonoBehaviour
{
    public Button next, back, ready, help;
    public GameObject canva;
    public GameObject htext;
    public GameObject core;
    public GameObject[] faces;
    public GameObject[] edges;
    public GameObject[] vertices;

    int ptr;
    bool move, rbool;
    bool b1, b2, b3, b4;
    bool vf, ef, ff, cf, vb, eb, fb, cb;
    Vector3[] vposo;
    Vector3[] vposf;
    Vector3[] eposo;
    Vector3[] eposf;
    Vector3[] fposo;
    Vector3[] fposf;
    Vector3 cposo;
    Vector3 cposf;
    float elapsedTime = 0f;
    float duration = 2f;
    float percentage;

    void Start()
    {
        this.GetComponent<Controller1>().enabled = false;

        Debug.Log("Hi fam x");
        ready.onClick.AddListener(readyfunc);
        help.onClick.AddListener(helpfunc);
        htext.SetActive(false);
        ptr = 0;

        vposo = new Vector3[8];
        vposf = new Vector3[8];
        eposo = new Vector3[12];
        eposf = new Vector3[12];
        fposo = new Vector3[6];
        fposf = new Vector3[6];

        move = true; rbool = false;
        b1 = true; b2 = true; b3 = true; b4 = true;
        vf = false; ef = false; ff = false; cf = false; vb = false; eb = false; fb = false; cb = false;

        for (int i = 0; i < vertices.Length; i++)
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

    public void firstfunc()
    {
        this.GetComponent<Controller1>().enabled = false;

        Debug.Log("Hi fam x");
        ready.onClick.AddListener(readyfunc);
        help.onClick.AddListener(helpfunc);
        htext.SetActive(false);
        canva.SetActive(true);
        ptr = 0;

        move = true; rbool = false;
        b1 = true; b2 = true; b3 = true; b4 = true;
        vf = false; ef = false; ff = false; cf = false; vb = false; eb = false; fb = false; cb = false;
    }

    void nextfunc()
    {
        elapsedTime = 0;
        if (ptr == 0)
        {
            vf = true;
        }
        else if (ptr == 1)
        {
            ef = true;
        }
        else if (ptr == 2)
        {
            ff = true;
        }
        else if (ptr == 3)
        {
            cf = true;
        }
    }

    void backfunc()
    {
        elapsedTime = 0;
        if (ptr == 1)
        {
            vb = true;
        }
        else if (ptr == 2)
        {
            eb = true;
        }
        else if (ptr == 3)
        {
            fb = true;
        }
        else if (ptr == 4)
        {
            cb = true;
        }
    }

    void readyfunc()
    {
        for(int i = 0; i < vertices.Length; i++) vertices[i].transform.position = vposf[i];
        for(int i = 0; i < edges.Length; i++) edges[i].transform.position = eposf[i];
        for (int i = 0; i < faces.Length; i++) faces[i].transform.position = fposf[i];
        core.transform.position = cposf;
        canva.SetActive(false);
        this.GetComponent<Controller1>().enabled = true;
        this.GetComponent<Controller1>().firstfuncA();
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

    void Update()
    {
        elapsedTime += Time.deltaTime;
        percentage = elapsedTime / duration;
        if (move)
        {
            next.onClick.AddListener(nextfunc);
            back.onClick.AddListener(backfunc);
        }
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
                ptr = 1;
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
                ptr = 2;
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
                ptr = 3;
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
                ptr = 4;
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
                ptr = 0;
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
                ptr = 1;
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
                ptr = 2;
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
                ptr = 3;
            }
        }
    }
}
