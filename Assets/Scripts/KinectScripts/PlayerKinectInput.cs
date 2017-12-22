using System;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Kinect/Kinect Input Module")]
[RequireComponent(typeof(EventSystem))]
[RequireComponent(typeof(GameObject))]
public class PlayerKinectInput : BaseInputModule, KinectGestures.GestureListenerInterface
{

    PointerEventData _handPointerData;

    private bool swipeLeft;
    private bool swipeRight;
    private bool click;
    private bool tPose;
    private bool jump;
    private bool squat;
    private bool swipeDown;
    private bool swipeUp;

    KinectManager manager;
    // the joint we want to track
    public KinectWrapper.NuiSkeletonPositionIndex joint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;

    public float selectTime = 0.5f;

    // joint position at the moment, in Kinect coordinates
    private Vector3 HandPosition;
    private GameObject OldselectedObj;
    private float staticTime;
    private bool selected;
    private GameObject selectedObj;
    private GameObject selectedObjOld;
    public GameObject cursor1;

    private Vector3 HandPosition2;
    private float staticTime2;
    private bool selected2;
    private GameObject selectedObj2;
    private GameObject selectedObjOld2;
    public GameObject cursor2;
    public float accX;
    public float decXP1;
    public float decXP2;
    public float accY;
    public float decY;

    public bool IsClick()
    {
        if (click)
        {
            click = false;
            return true;
        }
        return false;
    }

    public bool IsTPose()
    {
        if (tPose)
        {
            tPose = false;
            return true;
        }
        return false;
    }

    public bool IsJump()
    {
        if (jump)
        {
            jump = false;
            return true;
        }
        return false;
    }
    public bool IsSquat()
    {
        if (squat)
        {
            squat = false;
            return true;
        }

        return false;
    }

    public bool IsSwipeRight()
    {
        if (swipeRight)
        {
            swipeRight = false;
            return true;
        }

        return false;
    }
    public bool IsSwipeLeft()
    {
        if (swipeLeft)
        {
            swipeLeft = false;
            return true;
        }

        return false;
    }

    // get a pointer event data for a screen position
    private PointerEventData GetLookPointerEventData(Vector3 componentPosition)
    {
        if (_handPointerData == null)
        {
            _handPointerData = new PointerEventData(EventSystem.current);
        }
        _handPointerData.Reset();
        _handPointerData.delta = Vector2.zero;
        _handPointerData.position = Camera.main.WorldToScreenPoint(componentPosition);
        _handPointerData.scrollDelta = Vector2.zero;
        EventSystem.current.RaycastAll(_handPointerData, m_RaycastResultCache);
        _handPointerData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_RaycastResultCache.Clear();
        return _handPointerData;
    }

    public override void Process()
    {
        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected())
            {
                uint userId = manager.GetPlayer1ID();
                uint user2Id = manager.GetPlayer2ID();

                if (manager.IsJointTracked(userId, (int)joint))
                {
                    // output the joint position for easy tracking
                    Vector3 jointPos = manager.GetJointPosition(userId, (int)joint);
                    HandPosition.x = accX * (jointPos.x * Screen.width) + decXP1;
                    HandPosition.y = accY * (jointPos.y * Screen.height) + decY;
                    HandPosition.z = 743;
                    //Debug.Log("HandTracking, x: " + jointPos.x.ToString() + "y :" + jointPos.y.ToString());
                    cursor1.gameObject.transform.position = HandPosition;

                    PointerEventData lookData = GetLookPointerEventData(HandPosition);
                    eventSystem.SetSelectedGameObject(null);
                    if (lookData.pointerCurrentRaycast.gameObject != null)
                    {
                        selectedObjOld = selectedObj;
                        selectedObj = lookData.pointerCurrentRaycast.gameObject;
                        Debug.Log("Object selected : " + selectedObj.name);
                        //ExecuteEvents.ExecuteHierarchy(selectedObj, lookData, ExecuteEvents.submitHandler);
                        //ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.pointerUpHandler);
                    }

                    if (selectedObjOld == selectedObj)
                    {
                        if (staticTime + Time.deltaTime < selectTime)
                        {
                            staticTime += Time.deltaTime;
                        }
                        else
                        {
                            lookData = GetLookPointerEventData(HandPosition);
                            eventSystem.SetSelectedGameObject(null);
                            if (lookData.pointerCurrentRaycast.gameObject != null)
                            {
                                selectedObjOld = selectedObj;
                                selectedObj = lookData.pointerCurrentRaycast.gameObject;
                                Debug.Log("Object selected : " + selectedObj.GetType());
                                ExecuteEvents.ExecuteHierarchy(selectedObj, lookData, ExecuteEvents.submitHandler);
                                //ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.pointerUpHandler);
                            }
                        }// TODO: Add SwipeUp and Down support : test selectedObj for slider
                    } else
                    {
                        staticTime = 0;
                    }
                    
                }

                if (user2Id != 0)
                {
                    if (manager.IsJointTracked(user2Id, (int)joint))
                    {
                        // output the joint position for easy tracking
                        Vector3 jointPos = manager.GetJointPosition(user2Id, (int)joint);
                        HandPosition2.x = accX * (jointPos.x * Screen.width) + decXP2;
                        HandPosition2.y = accY * (jointPos.y * Screen.height) + decY;
                        HandPosition2.z = 743;
                        //Debug.Log("HandTracking, x: " + jointPos.x.ToString() + "y :" + jointPos.y.ToString());
                        cursor2.gameObject.transform.position = HandPosition2;

                        PointerEventData lookData = GetLookPointerEventData(HandPosition2);
                        eventSystem.SetSelectedGameObject(null);
                        if (lookData.pointerCurrentRaycast.gameObject != null)
                        {
                            selectedObjOld2 = selectedObj2;
                            selectedObj2 = lookData.pointerCurrentRaycast.gameObject;
                            Debug.Log("Object selected : " + selectedObj2.name);
                            //ExecuteEvents.ExecuteHierarchy(selectedObj, lookData, ExecuteEvents.submitHandler);
                            //ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.pointerUpHandler);
                        }

                        if (selectedObjOld2 == selectedObj2)
                        {
                            if (staticTime2 + Time.deltaTime < selectTime)
                            {
                                staticTime2 += Time.deltaTime;
                            }
                            else
                            {
                                lookData = GetLookPointerEventData(HandPosition2);
                                eventSystem.SetSelectedGameObject(null);
                                if (lookData.pointerCurrentRaycast.gameObject != null)
                                {
                                    selectedObjOld2 = selectedObj2;
                                    selectedObj2 = lookData.pointerCurrentRaycast.gameObject;
                                    Debug.Log("Object selected : " + selectedObj2.name);
                                    ExecuteEvents.ExecuteHierarchy(selectedObj2, lookData, ExecuteEvents.submitHandler);
                                    //ExecuteEvents.ExecuteHierarchy(go, lookData, ExecuteEvents.pointerUpHandler);
                                }
                            }
                        } // TODO: Add SwipeUp and Down support : test selectedObj for slider
                        else
                        {
                            staticTime2 = 0;
                        }
                    }
                }
            }
        }
        else
        {
            manager = KinectManager.Instance;
        }
    }

    private void ProcessDrag()
    {
        throw new NotImplementedException();
    }

    private void ProcessWaitOver()
    {
        throw new NotImplementedException();
    }

    private void ProcessPress()
    {
        throw new NotImplementedException();
    }

    private void ProcessHover()
    {
        throw new NotImplementedException();
    }

    public void UserDetected(uint userId, int userIndex)
    {
        Debug.Log("USER DETECTED!!!");
        // detect these user specific gestures
        

        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
        manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        //manager.DetectGesture(userId, KinectGestures.Gestures.Click);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);
        manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);
    }

    public void UserLost(uint userId, int userIndex)
    {
        Debug.Log("User " + userId.ToString() + " is Lost");
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        // don't do anything here
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        string sGestureText = gesture + " detected";
        Debug.Log(sGestureText);

        if (gesture == KinectGestures.Gestures.SwipeLeft)
            swipeLeft = true;
        else if (gesture == KinectGestures.Gestures.SwipeRight)
            swipeRight = true;
        else if (gesture == KinectGestures.Gestures.SwipeUp)
            swipeUp = true;
        else if (gesture == KinectGestures.Gestures.SwipeDown)
            swipeDown = true;
        else if (gesture == KinectGestures.Gestures.Click)
            click = true;
        else if (gesture == KinectGestures.Gestures.Jump)
            jump = true;
        else if (gesture == KinectGestures.Gestures.Squat)
            squat = true;
        else if (gesture == KinectGestures.Gestures.Tpose)
            tPose = true;

        return true;
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }
}
