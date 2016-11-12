using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class VRMenu : MonoBehaviour
{
    enum ButtonStates { Idle, Hovering, Clicked }

    class ButtonData
    {
        public Guid ID;

        public Button ButtonComp;
        public Transform Trans;
        public Func<bool> OnClick;
        public float EvaluationTime;
        public ButtonStates State;
        public ButtonStates LastState;

        public float DefaultZ;
        public float StartZ;
        public float EndZ;
    }
    [Serializable]
    struct ButtonEffectData
    {
        public float Offet;
        public float Duration;
        public AnimationCurve MovementCurve;
    }

    public bool ClearEachFrame;
    [SerializeField]
    private RectTransform ButtonParent;
    [SerializeField]
    private Button ButtonPrefab;
    [SerializeField]
    private ButtonEffectData HoverZEffect;
    [SerializeField]
    private ButtonEffectData ClickZEffect;
    [SerializeField]
    private bool AllowDebugInput;

    private List<ButtonData> Buttons = new List<ButtonData>();

    void Awake()
    {
    }

    void Update()
    {
        if (ClearEachFrame)
            ClearButtons();
    }

    void LateUpdate()
    {
        //Debug.Log("Update!!!");
        foreach (var buttonData in Buttons)
        {
            var lastState = buttonData.State;
            switch (buttonData.State)
            {
                case ButtonStates.Idle:
                    if (buttonData.LastState != ButtonStates.Idle)
                    {
                        buttonData.EvaluationTime = Time.time;
                        buttonData.StartZ = buttonData.Trans.localPosition.z;
                        buttonData.EndZ = buttonData.DefaultZ;
                    }
                    else
                    {
                        if (ClearEachFrame == false)
                            UpdateButtonEffect(buttonData, HoverZEffect);
                    }
                    break;
                case ButtonStates.Hovering:
                    if (ClearEachFrame == false)
                        UpdateButtonEffect(buttonData, HoverZEffect);
                    buttonData.State = ButtonStates.Idle; // The hover state is reset every frame
                    break;
                case ButtonStates.Clicked:
                    float normTime = 1f;
                    if (ClearEachFrame == false)
                        normTime = UpdateButtonEffect(buttonData, ClickZEffect);
                    if (normTime >= 1)
                        buttonData.State = ButtonStates.Idle;
                    break;
            }
            buttonData.LastState = lastState;
        }

        if (AllowDebugInput)
        {
            //Debug.Log("AllowDebugInput!!!: " + Input.GetKeyDown(KeyCode.Alpha1));
            if (Input.GetKeyDown(KeyCode.Alpha1) && Buttons.Count > 0)
                Buttons[0].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha2) && Buttons.Count > 1)
                Buttons[1].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha3) && Buttons.Count > 2)
                Buttons[2].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha4) && Buttons.Count > 3)
                Buttons[3].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha5) && Buttons.Count > 4)
                Buttons[4].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha6) && Buttons.Count > 5)
                Buttons[5].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha7) && Buttons.Count > 6)
                Buttons[6].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha8) && Buttons.Count > 7)
                Buttons[7].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha9) && Buttons.Count > 8)
                Buttons[8].OnClick();
            if (Input.GetKeyDown(KeyCode.Alpha0) && Buttons.Count > 9)
                Buttons[9].OnClick();
        }
    }

    private float UpdateButtonEffect(ButtonData buttonData, ButtonEffectData effectData)
    {
        var normTime = 1 - Mathf.Clamp01(((buttonData.EvaluationTime + effectData.Duration) - Time.time) / effectData.Duration);
        var zPos = Mathf.Lerp(buttonData.StartZ, buttonData.EndZ, effectData.MovementCurve.Evaluate(normTime));
        buttonData.Trans.localPosition = new Vector3(buttonData.Trans.localPosition.x, buttonData.Trans.localPosition.y, zPos);

        return normTime;
    }

    public Guid RegisterButton(string name, Func<bool> onClick)
    {
        if (ClearEachFrame && (gameObject.activeSelf == false || this.enabled == false))
            return Guid.Empty;

        var buttonInst = Instantiate(ButtonPrefab);
        buttonInst.transform.SetParent(ButtonParent, false);
        buttonInst.GetComponentInChildren<Text>().text = name;

        Guid id = Guid.NewGuid();
        Buttons.Add(new ButtonData()
        {
            ID = id,
            ButtonComp = buttonInst,
            Trans = buttonInst.transform,
            DefaultZ = buttonInst.transform.localPosition.z,
            OnClick = onClick,
        });

        return id;
    }

    public void RemoveButton(Guid id)
    {
        if (ClearEachFrame && (gameObject.activeSelf == false || this.enabled == false))
            return;

        for (int i = Buttons.Count - 1; i >= 0; i--)
        {
            if (Buttons[i].ID == id)
            {
                Destroy(Buttons[i].ButtonComp);
                Buttons.RemoveAt(i);
                return;
            }
        }
    }

    public void ClearButtons()
    {
        for (int i = 0; i < Buttons.Count; i++)
            Destroy(Buttons[i].ButtonComp.gameObject);

        Buttons.Clear();
    }

    public void OnButtonHover(Button button)
    {
        foreach (var buttonData in Buttons)
        {
            if (buttonData.ButtonComp == button && buttonData.State != ButtonStates.Clicked)
            {
                if (buttonData.LastState != ButtonStates.Hovering)
                {
                    buttonData.EvaluationTime = Time.time;
                    buttonData.StartZ = buttonData.Trans.localPosition.z;
                    buttonData.EndZ = HoverZEffect.Offet;
                }

                buttonData.State = ButtonStates.Hovering;
                return;
            }
        }
    }
    public void OnButtonClick(Button button, out bool closeMenu)
    {
        closeMenu = false;
        foreach (var buttonData in Buttons)
        {
            //Debug.Log("buttonData.ButtonComp: " + buttonData.ButtonComp.GetComponentInChildren<Text>().text);
            if (buttonData.ButtonComp.GetComponentInChildren<Text>().text == button.GetComponentInChildren<Text>().text)
            {
                buttonData.EvaluationTime = Time.time;
                buttonData.StartZ = buttonData.Trans.localPosition.z;
                buttonData.EndZ = ClickZEffect.Offet;
                buttonData.State = ButtonStates.Clicked;
                closeMenu = buttonData.OnClick();
                return;
            }
        }
    }
}
