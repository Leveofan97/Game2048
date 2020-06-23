using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 50.0f;
    private float maxSwipeTime = 1.5f;

    private GameManager qm;

    private void Awake()
    {
        qm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (qm.State == GameState.Playing && Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        isSwipe = true;
                        fingerStartTime = Time.time;
                        fingerStartPos = touch.position;
                        break;
                    case TouchPhase.Canceled:
                        isSwipe = false;
                        break;
                    case TouchPhase.Ended:
                        float gestureTime = Time.time - fingerStartTime;
                        float gestureDist = (touch.position - fingerStartPos).magnitude;

                        if(isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                        {
                            Vector2 direction = touch.position - fingerStartPos;
                            Vector2 swipeType = Vector2.zero;

                            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                            {
                                swipeType = Vector2.right * Mathf.Sign(direction.x);
                            }
                            else
                            {
                                swipeType = Vector2.up * Mathf.Sign(direction.y);
                            }

                            if(swipeType.x != 0.0f)
                            {
                                if(swipeType.x > 0.0f)
                                {
                                    //Движение вправо
                                    qm.Move(MoveDirection.Right);
                                }
                                else
                                {
                                    //Движение влево
                                    qm.Move(MoveDirection.Left);
                                }
                            }

                            if (swipeType.y!= 0.0f)
                            {
                                if (swipeType.y > 0.0f)
                                {
                                    //Движение вверх
                                    qm.Move(MoveDirection.Up);
                                }
                                else
                                {
                                    //Движение вниз
                                    qm.Move(MoveDirection.Down);
                                }
                            }
                        }
                        break;
                }
            }
        }
        // Выход из игры на андройде
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
