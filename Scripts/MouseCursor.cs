using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    
    [SerializeField] private Camera cam;//reference to game camera object
    
    LayerMask layerMask;//mask to check in raycast

    // Start is called before the first frame update
    void Start()
    {
        //bit shift to get layermask as bit mask
        //layerMask = 1 << LayerMask.GetMask("Ground");
        layerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        //raycast from mouse position to ground
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, layerMask)) {
            //set position to raycast collision point
            transform.position = raycastHit.point;
        }
    }
}
