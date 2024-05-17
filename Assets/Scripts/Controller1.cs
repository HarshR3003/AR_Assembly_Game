using System.Collections;
using System.Collections.Generic;
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
    public float speed = 0.01f;

    int ptr;

    void Start()
    {
        next.onClick.AddListener(nextfunc);
        back.onClick.AddListener(backfunc);
        ready.onClick.AddListener(readyfunc);
        help.onClick.AddListener(helpfunc);
        htext.SetActive(false);
        ptr = 0;
    }

    void nextfunc()
    {
        if (ptr == 0)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 pos = vertices[i].transform.position;
                pos = pos + pos;
                vertices[i].transform.position = Vector3.MoveTowards(vertices[i].transform.position, pos, speed);
            }
            ptr++;
        }
        else if (ptr == 1)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                Vector3 pos = edges[i].transform.position;
                pos = pos + pos;
                edges[i].transform.position = Vector3.MoveTowards(edges[i].transform.position, pos, speed);
            }
            ptr++;
        }
        else if (ptr == 2)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                Vector3 pos = faces[i].transform.position;
                pos = pos + pos;
                faces[i].transform.position = Vector3.MoveTowards(faces[i].transform.position, pos, speed);
            }
            ptr++;
        }
        else if(ptr == 3)
        {
            Vector3 pos = core.transform.position;
            pos.z = pos.z + 1;
            core.transform.position = Vector3.MoveTowards(core.transform.position, pos, speed);
            ptr++;
        }
    }

    void backfunc()
    {
        if (ptr == 1)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 pos = vertices[i].transform.position;
                pos = pos/2;
                vertices[i].transform.position = Vector3.MoveTowards(vertices[i].transform.position, pos, speed);
            }
            ptr--;
        }
        else if (ptr == 2)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                Vector3 pos = edges[i].transform.position;
                pos = pos/2;
                edges[i].transform.position = Vector3.MoveTowards(edges[i].transform.position, pos, speed);
            }
            ptr--;
        }
        else if (ptr == 3)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                Vector3 pos = faces[i].transform.position;
                pos = pos/2;
                faces[i].transform.position = Vector3.MoveTowards(faces[i].transform.position, pos, speed);
            }
            ptr--;
        }
        else if (ptr == 4)
        {
            Vector3 pos = core.transform.position;
            pos.z = pos.z - 1;
            core.transform.position = Vector3.MoveTowards(core.transform.position, pos, speed);
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
        this.GetComponent<Controller2>().enabled=true;
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
        
    }
}
