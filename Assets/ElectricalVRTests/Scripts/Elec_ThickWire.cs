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
    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = WireLenght;
        for (int i = 0; i < WireLenght; i++)
        {
            if (lines.Count == 0)
            {
                lines.Add(Instantiate(WirePiecePrefab, gameObject.transform.position + -gameObject.transform.up, gameObject.transform.rotation, gameObject.transform));
                lines[0].GetComponent<ConfigurableJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
            }
            else if (i == WireLenght - 1)
            {
                lines.Add(Instantiate(Endprefab, gameObject.transform.position + -gameObject.transform.up, gameObject.transform.rotation, gameObject.transform));
                lines[WireLenght-1].GetComponent<ConfigurableJoint>().connectedBody = lines[i - 1].GetComponent<Rigidbody>();
            }
            else
            {
                GameObject wirepiece = Instantiate(WirePiecePrefab, lines[i - 1].transform.position + -lines[i - 1].transform.up, gameObject.transform.rotation, gameObject.transform);
                lines.Add(wirepiece);
                wirepiece.GetComponent<ConfigurableJoint>().connectedBody = lines[i - 1].GetComponent<Rigidbody>();
            }           
        }
    }
}
