using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera1 : MonoBehaviour
{
    public Transform FollowObject;
    public Transform thisTR;
    public Vector3 vector3;
    public float dsd = 100f;
    public bool lookAt;
    public bool onlyY;
    public bool if_Y_Z;
    float sdsdsd;

    void Start()
    {
        thisTR = transform;
    }

    void LateUpdate()
    {
        if (thisTR == null) return;
        if (FollowObject == null) return;
        if (lookAt) thisTR.LookAt(FollowObject);
        if (if_Y_Z)
        {
            if (onlyY)
            {
                sdsdsd = Vector3.Distance(thisTR.position, FollowObject.position);
                thisTR.localPosition = Vector3.MoveTowards(thisTR.localPosition, new Vector3(thisTR.localPosition.x, thisTR.localPosition.y, (FollowObject.localPosition + new Vector3(0f, 0f, vector3.y)).z), sdsdsd * Time.deltaTime * dsd);
            }
            else
            {
                sdsdsd = Vector3.Distance(thisTR.position, FollowObject.position);
                thisTR.position = Vector3.MoveTowards(thisTR.position, FollowObject.position + vector3, sdsdsd * Time.deltaTime * dsd);
            }
        }
        else
        {
            if (onlyY)
            {
                sdsdsd = Vector3.Distance(thisTR.position, FollowObject.position);
                thisTR.position = Vector3.MoveTowards(thisTR.position, FollowObject.position + new Vector3(0f, vector3.y, 0f), sdsdsd * Time.deltaTime * dsd);
            }
            else
            {
                sdsdsd = Vector3.Distance(thisTR.position, FollowObject.position);
                thisTR.position = Vector3.MoveTowards(thisTR.position, FollowObject.position + vector3, sdsdsd * Time.deltaTime * dsd);
            }
        }
    }
}
