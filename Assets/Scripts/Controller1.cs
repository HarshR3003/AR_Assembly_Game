using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller1 : MonoBehaviour
{
    public Button next, back, ready;
    public GameObject canva;
    public GameObject core;
    public GameObject[] faces;
    public GameObject[] edges;
    public GameObject[] vertices;

    int ptr;

    void Start()
    {
        next.onClick.AddListener(nextfunc);
        back.onClick.AddListener(backfunc);
        ready.onClick.AddListener(readyfunc);
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
                vertices[i].transform.position = Vector3.Lerp(vertices[i].transform.position, pos, 50);
            }
            ptr++;
        }
        else if (ptr == 1)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                Vector3 pos = edges[i].transform.position;
                pos = pos + pos;
                edges[i].transform.position = Vector3.Lerp(edges[i].transform.position, pos, 50);
            }
            ptr++;
        }
        else if (ptr == 2)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                Vector3 pos = faces[i].transform.position;
                pos = pos + pos;
                faces[i].transform.position = Vector3.Lerp(faces[i].transform.position, pos, 50);
            }
            ptr++;
        }
        else if(ptr == 3)
        {
            Vector3 pos = core.transform.position;
            pos.z = pos.z + 1;
            core.transform.position = Vector3.Lerp(core.transform.position, pos, 50);
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
                vertices[i].transform.position = Vector3.Lerp(vertices[i].transform.position, pos, 50);
            }
            ptr--;
        }
        else if (ptr == 2)
        {
            for (int i = 0; i < edges.Length; i++)
            {
                Vector3 pos = edges[i].transform.position;
                pos = pos/2;
                edges[i].transform.position = Vector3.Lerp(edges[i].transform.position, pos, 50);
            }
            ptr--;
        }
        else if (ptr == 3)
        {
            for (int i = 0; i < faces.Length; i++)
            {
                Vector3 pos = faces[i].transform.position;
                pos = pos/2;
                faces[i].transform.position = Vector3.Lerp(faces[i].transform.position, pos, 50);
            }
            ptr--;
        }
        else if (ptr == 4)
        {
            Vector3 pos = core.transform.position;
            pos.z = pos.z - 1;
            core.transform.position = Vector3.Lerp(core.transform.position, pos, 50);
            ptr--;
        }
    }

    void readyfunc()
    {
        while (ptr != 0)
        {
            backfunc();
        }
        canva.SetActive(false);
    }


    void Update()
    {
        
    }
}
