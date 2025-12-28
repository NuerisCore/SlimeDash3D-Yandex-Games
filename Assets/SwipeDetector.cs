using UnityEngine;
using UnityEngine.Events;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 mouseDownPosition;
    private Vector2 mouseUpPosition;

    [SerializeField] private bool detectSwipeOnlyAfterRelease = false;
    [SerializeField] private float minDistanceForSwipe = 20f;

    private bool hasSwipedUp = false;
    private bool hasSwipedDown = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
            mouseUpPosition = Input.mousePosition;
            hasSwipedDown = false;
            hasSwipedUp = false;
        }

        if (Input.GetMouseButton(0) && !detectSwipeOnlyAfterRelease)
        {
            mouseUpPosition = Input.mousePosition;
            DetectSwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseUpPosition = Input.mousePosition;
            DetectSwipe();
        }
    }

    void DetectSwipe()
    {
        if (hasSwipedUp || hasSwipedDown) return; // Если уже был свайп, то выходим из метода

        if (VerticalMove() > minDistanceForSwipe)
        {
            if (IsVerticalSwipeUp())
            {
                hasSwipedUp = true;
            }
            else if (IsVerticalSwipeDown())
            {
                hasSwipedDown = true;
            }
        }
    }

    float VerticalMove()
    {
        return Mathf.Abs(mouseDownPosition.y - mouseUpPosition.y);
    }

    bool IsVerticalSwipeUp()
    {
        return mouseDownPosition.y - mouseUpPosition.y > minDistanceForSwipe;
    }

    bool IsVerticalSwipeDown()
    {
        return mouseUpPosition.y - mouseDownPosition.y > minDistanceForSwipe;
    }

}

