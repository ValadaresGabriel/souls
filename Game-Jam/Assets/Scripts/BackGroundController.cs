using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject parentObject;  // O objeto pai
    public GameObject childObject;  // O objeto filho

    private void LateUpdate()
    {
        if(childObject.transform.position.x <= parentObject.transform.position.x){
            childObject.transform.SetParent(parentObject.transform);
        }
    }
}
