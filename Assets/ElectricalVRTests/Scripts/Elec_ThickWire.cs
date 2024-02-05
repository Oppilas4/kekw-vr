using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Elec_ThickWire : MonoBehaviour
{
    LineRenderer lineRenderer;
    List<GameObject> lines = new List<GameObject>();
    public int WireLenght;
    public GameObject WirePiecePrefab,Endprefab;
    public float inbetweenSpace = 1.5f;
    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = WireLenght;
        lineRenderer.SetPosition(0,gameObject.transform.position);

        for (int i = 0; i < WireLenght; i++)
        {
            Vector3 offset = new Vector3(0f, -inbetweenSpace, 0f);
            if (lines.Count == 0)
            {
                lines.Add(Instantiate(WirePiecePrefab, gameObject.transform.position + offset, gameObject.transform.rotation, gameObject.transform));
                lines[0].GetComponent<ConfigurableJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            }
            else if (i == WireLenght - 1)
            {
                lines.Add(Instantiate(Endprefab, lines[i - 1].transform.position + offset, gameObject.transform.rotation, gameObject.transform));
                lines[WireLenght-1].GetComponent<ConfigurableJoint>().connectedBody = lines[i - 1].GetComponent<Rigidbody>();
            }
            else
            {
                GameObject wirepiece = Instantiate(WirePiecePrefab, lines[i - 1].transform.position + offset, gameObject.transform.rotation, gameObject.transform);
                lines.Add(wirepiece);
                wirepiece.GetComponent<ConfigurableJoint>().connectedBody = lines[i - 1].GetComponent<Rigidbody>();
            }           
        }
    }
    private void Update()
    {
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i] != null) { lineRenderer.SetPosition(i, lines[i].transform.position); }
        }
    }
}
