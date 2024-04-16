using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_FollowCurve : MonoBehaviour
{
    public MC_CurveDrawer _curveDrawer;
    private GameObject playerObject;
    [SerializeField]private float speed = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 closestPoint = _curveDrawer.GetClosestPointOnCurve(playerObject.transform.position);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, closestPoint, speed * Time.deltaTime);
    }

}
