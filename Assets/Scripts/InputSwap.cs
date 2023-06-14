using UnityEngine;
using System;

public class InputSwap : MonoBehaviour
{
    private bool swapInput = false;
    private Vector2 swipeStartPos;
    private const float swipeThreshold = 50f;

    public event EventHandler OnSwipeUp;
    public event EventHandler OnSwipeDown;
    public event EventHandler OnSwipeLeft;
    public event EventHandler OnSwipeRight;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                swipeStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeEndPos = touch.position;
                Vector2 swipeDirection = swipeEndPos - swipeStartPos;

                if (swipeDirection.magnitude >= swipeThreshold)
                {
                    float angle = Mathf.Atan2(swipeDirection.y, swipeDirection.x) * Mathf.Rad2Deg;

                    if (angle > 45f && angle <= 135f)
                    {
                        // Up swipe
                        OnSwipeUp?.Invoke(this, EventArgs.Empty);
                    }
                    else if (angle < -45f && angle >= -135f)
                    {
                        // Down swipe
                        OnSwipeDown?.Invoke(this, EventArgs.Empty);
                    }
                    else if (angle >= -45f && angle <= 45f)
                    {
                        // Right swipe
                        OnSwipeRight?.Invoke(this, EventArgs.Empty);
                    }
                    else if (angle <= -135f || angle > 135f)
                    {
                        // Left swipe
                        OnSwipeLeft?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Toggle input swap
            swapInput = !swapInput;
        }
    }
}
