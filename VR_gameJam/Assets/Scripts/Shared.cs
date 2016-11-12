using System;
using UnityEngine;

public static class Shared
{
    public static void SetGOActive(GameObject go, bool active)
    {
        if (go.activeSelf != active)
            go.SetActive(active);
    }
}
