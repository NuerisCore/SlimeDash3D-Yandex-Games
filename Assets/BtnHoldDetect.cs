using UnityEngine;
using UnityEngine.EventSystems;

public class BtnHoldDetect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isHold = false;
    public bool BOO;

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        isHold = true;
        Player.player.Agregate(BOO);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        isHold = false;
    }
}
