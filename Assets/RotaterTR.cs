using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaterTR : MonoBehaviour
{
    public Vector3 R;
    public static int GraphicsID;

    private void FixedUpdate()
    {
        transform.Rotate(R);

        if (GetComponent<Light>())
        {
            var light = GetComponent<Light>();

            if (GraphicsID == 0 || GraphicsID == 1)
            {
                light.renderMode = LightRenderMode.ForceVertex;
                light.enabled = false;
            }
            else if (GraphicsID == 2 || GraphicsID == 3)
            {
                light.renderMode = LightRenderMode.ForceVertex;
                light.enabled = true;
            }
            else if (GraphicsID == 4)
            {
                light.renderMode = LightRenderMode.ForcePixel;
                light.enabled = true;
            }
        }
    }
}
