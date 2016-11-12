using UnityEngine;
using System.Collections;
using System;
using Valve.VR;

public class GripTravelSteamVR : GripTravel
{
    [SerializeField]
    private ViveControllerBase LeftController;
    [SerializeField]
    private ViveControllerBase RightController;
    [SerializeField]
    private EVRButtonId GripButton;

    protected override bool LeftTriggerPress()
    {
        if (LeftController.ControlIndex < 0)
            return false;
        return SteamVR_Controller.Input(LeftController.ControlIndex).GetPress(GripButton);
    }
    protected override bool RightTriggerPress()
    {
        if (RightController.ControlIndex < 0)
            return false;
        return SteamVR_Controller.Input(RightController.ControlIndex).GetPress(GripButton);
    }
}
