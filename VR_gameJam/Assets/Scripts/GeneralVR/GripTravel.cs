using UnityEngine;
using System.Collections;

public abstract class GripTravel : MonoBehaviour
{
    enum States { None, Moving, Zooming, }

    [System.Serializable]
    struct VisualSettings
    {
        public LineRenderer MidLine;
        public Transform MidPointHUD;
    }

    [SerializeField]
    private Transform ObjectToMove;
    [SerializeField]
    private Transform POV;
    [SerializeField]
    private GameObject BackgroundGraphics;
    [SerializeField]
    private Transform LeftControllerTrans;
    [SerializeField]
    private Transform RightControllerTrans;
    [SerializeField]
    private Transform LeftHoldTrans;
    [SerializeField]
    private Transform RightHoldTrans;
    [SerializeField]
    private Transform MidHoldTrans;
    [SerializeField]
    private float MinScale;
    [SerializeField]
    private float MaxScale;
    [SerializeField]
    private VisualSettings Visuals;

    protected abstract bool LeftTriggerPress();
    protected abstract bool RightTriggerPress();

    struct StateData
    {
        public States State;

        public bool LeftHold;
        public bool RightHold;
        public bool TravelSuspended;
        public Transform SuspendedCtrlTrans;
        public bool InitializedMenu;

        public Vector3 RootHoldPosition;

        public float ZoomHoldValue;
        public float ZoomHoldDistance;
        public float StartAngleOffset;
        public float LastAngle;
    }
    StateData _State;

    Transform _Trans;

    void Start()
    {
        _Trans = transform;
        Visuals.MidLine.SetVertexCount(2);

        Visuals.MidLine.gameObject.SetActive(false);
        Visuals.MidPointHUD.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_State.TravelSuspended)
        {
            if (_State.SuspendedCtrlTrans == null)
            {
                _State.LeftHold = false;
                _State.RightHold = false;
                _State.State = States.None;

                if (LeftTriggerPress() == false && RightTriggerPress() == false)
                    _State.TravelSuspended = false;
                else
                    return;
            }
            else
            {
                bool leftCtrlDroped = _State.SuspendedCtrlTrans == LeftControllerTrans && LeftTriggerPress() == false;
                bool rightCtrlDroped = _State.SuspendedCtrlTrans == RightControllerTrans && RightTriggerPress() == false;

                if (leftCtrlDroped || rightCtrlDroped)
                {
                    _State.TravelSuspended = false;
                    _State.SuspendedCtrlTrans = null;
                }
            }
        }

        if (_State.State != States.None && LeftTriggerPress() == false && RightTriggerPress() == false)
        {
            _State.LeftHold = false;
            _State.RightHold = false;
            _State.State = States.None;
        }

        bool active = _State.State != States.None;
        if (BackgroundGraphics.activeSelf != active)
            BackgroundGraphics.SetActive(active);

        switch (_State.State)
        {
            case States.None:
                if (LeftTriggerPress())
                {
                    StartLeftGrip();
                }
                if (RightTriggerPress())
                {
                    StartRightGrip();
                }
                break;
            case States.Moving:
                if (_State.LeftHold && _State.SuspendedCtrlTrans != LeftControllerTrans)
                {
                    Vector3 newOffset = LeftControllerTrans.position - LeftHoldTrans.position;
                    ObjectToMove.position = _State.RootHoldPosition - newOffset;
                }
                if (_State.RightHold && _State.SuspendedCtrlTrans != RightControllerTrans)
                {
                    Vector3 newOffset = RightControllerTrans.position - RightHoldTrans.position;
                    ObjectToMove.position = _State.RootHoldPosition - newOffset;
                }

                if (LeftTriggerPress() && RightTriggerPress() && _State.SuspendedCtrlTrans == null)
                {
                    Vector3 handVec = LeftControllerTrans.position - RightControllerTrans.position;
                    _State.ZoomHoldDistance = handVec.magnitude;
                    _State.ZoomHoldValue = ObjectToMove.localScale.x;

                    handVec.y = 0;
                    _State.StartAngleOffset = MathHelp.SignedVectorAngle(handVec.normalized, Vector3.forward, Vector3.up);

                    RightHoldTrans.position = RightControllerTrans.position;
                    LeftHoldTrans.position = LeftControllerTrans.position;
                    MidHoldTrans.position = (LeftControllerTrans.position + RightControllerTrans.position) / 2;

                    _State.State = States.Zooming;
                    Visuals.MidPointHUD.position = MidHoldTrans.position;
                    Visuals.MidPointHUD.localScale = _Trans.localScale;
                }
                if (_State.SuspendedCtrlTrans == LeftControllerTrans && _State.LeftHold)
                {
                    if (_State.RightHold == false)
                        _State.State = States.None;
                    _State.LeftHold = false;
                }
                if (_State.SuspendedCtrlTrans == RightControllerTrans && _State.RightHold)
                {
                    if (_State.LeftHold == false)
                        _State.State = States.None;
                    _State.RightHold = false;
                }
                break;
            case States.Zooming:
                {
                    Vector3 handVec = LeftControllerTrans.position - RightControllerTrans.position;

                    float holdDistance = Vector3.Distance(LeftHoldTrans.position, RightHoldTrans.position);
                    float newDist = handVec.magnitude;
                    float scale = newDist / holdDistance;
                    float newScale = Mathf.Clamp(_State.ZoomHoldValue / scale, MinScale, MaxScale);

                    Vector3 preScaleMidPos = (LeftControllerTrans.position + RightControllerTrans.position) / 2;
                    ObjectToMove.localScale = new Vector3(newScale, newScale, newScale);
                    Vector3 postScaleMidPos = (LeftControllerTrans.position + RightControllerTrans.position) / 2;

                    Vector3 newOffset = postScaleMidPos - MidHoldTrans.position;
                    Vector3 scaleOffset = (postScaleMidPos - preScaleMidPos) + newOffset;

                    MidHoldTrans.position = postScaleMidPos;
                    ObjectToMove.position = ObjectToMove.position - scaleOffset;

                    ObjectToMove.RotateAround(postScaleMidPos, Vector3.up, -_State.LastAngle);

                    handVec = LeftControllerTrans.position - RightControllerTrans.position;
                    handVec.y = 0;

                    float handAngle = MathHelp.SignedVectorAngle(handVec.normalized, Vector3.forward, Vector3.up);
                    float angleDiff = handAngle - _State.StartAngleOffset;

                    ObjectToMove.RotateAround(postScaleMidPos, Vector3.up, angleDiff);

                    _State.LastAngle = angleDiff;

                    if (LeftTriggerPress() == false ^ RightTriggerPress() == false)
                    {
                        if (LeftTriggerPress())
                            StartLeftGrip();
                        if (RightTriggerPress())
                            StartRightGrip();
                    }

                    if (_State.TravelSuspended && _State.SuspendedCtrlTrans != null)
                    {
                        if (_State.SuspendedCtrlTrans == RightControllerTrans)
                            StartLeftGrip();
                        if (_State.SuspendedCtrlTrans == LeftControllerTrans)
                            StartRightGrip();
                    }
                }
                break;
            default:
                break;
        }

        bool zooming = _State.State == States.Zooming;
        if (zooming)
        {
            Visuals.MidLine.SetPosition(0, LeftControllerTrans.position);
            Visuals.MidLine.SetPosition(1, RightControllerTrans.position);
        }

        if (Visuals.MidLine.gameObject.activeSelf != zooming)
            Visuals.MidLine.gameObject.SetActive(zooming);
        if (Visuals.MidPointHUD.gameObject.activeSelf != zooming)
            Visuals.MidPointHUD.gameObject.SetActive(zooming);
    }
    private void StartLeftGrip()
    {
        if (_State.SuspendedCtrlTrans == LeftControllerTrans)
            return;

        _State.LeftHold = true;
        _State.RightHold = false;
        LeftHoldTrans.position = LeftControllerTrans.position;
        _State.RootHoldPosition = ObjectToMove.position;
        _State.State = States.Moving;
    }
    private void StartRightGrip()
    {
        if (_State.SuspendedCtrlTrans == RightControllerTrans)
            return;

        _State.RightHold = true;
        _State.LeftHold = false;
        RightHoldTrans.position = RightControllerTrans.position;
        _State.RootHoldPosition = ObjectToMove.position;
        _State.State = States.Moving;
    }
    public void SuspendInputIntillRelease(Transform ctrlTrans = null)
    {
        _State.TravelSuspended = true;
        _State.SuspendedCtrlTrans = ctrlTrans;
    }
}
