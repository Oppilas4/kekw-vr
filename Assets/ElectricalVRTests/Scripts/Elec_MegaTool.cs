using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elec_MegaTool : MonoBehaviour
{
    public GameObject EndPrefab;
    public GameObject SpawnPos;
    GameObject WirePiece;
    public Elec_ToolWireRenderer ToolWireREnderer;
    public void MakeWireEnd()
    {
       WirePiece = Instantiate(EndPrefab, SpawnPos.transform.position, SpawnPos.transform.rotation,SpawnPos.transform);
       
    }
    public void WireUnconnect()
    {
        WirePiece.GetComponent<Rigidbody>().useGravity = true;
        WirePiece.GetComponent<Rigidbody>().isKinematic = false;
        ToolWireREnderer.WireComponents.Add(WirePiece);
        WirePiece.transform.parent = null;
    }
}
