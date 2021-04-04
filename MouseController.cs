using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Camera Ncamera;

    public GameObject TreeLayer;
    public GameObject ArrayTree;

    public Vector3 posv;
    
    public bool nowAdd;

    // Start is called before the first frame update
    void Start()
    {
        TreeLayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (nowAdd)
        {
            RaycastHit hit;
            Ray ray = Ncamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                //TreeLayer.transform.position = objectHit.position;

                // Do something with the object that was hit by the raycast.
                //Debug.Log(objectHit);
                Debug.Log("Point : " + hit.point);
                TreeLayer.transform.position = hit.point;

            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            nowAdd = !nowAdd;
            if (nowAdd)
            {
                TreeLayer.SetActive(true);
            }
            else
            {
                TreeLayer.SetActive(false);
            }
        }

    }
}
