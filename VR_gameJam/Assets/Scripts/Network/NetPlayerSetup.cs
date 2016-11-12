using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetPlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private GameObject VisualHeadPrefab;
    [SerializeField]
    private Transform HeadRoot;

    public override void OnStartLocalPlayer()
    {
        Debug.Log("playerControllerId: " + playerControllerId);

        var spawnPoints = FindObjectsOfType<NetPlayerSpawnPoint>();
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.SpawnID == playerControllerId)
            {
                transform.position = spawnPoint.transform.position; 
                transform.rotation = spawnPoint.transform.rotation;
            }
        }
    }

    public override void OnStartClient()
    {
        if (isLocalPlayer == false)
        {
            var obj = Instantiate(VisualHeadPrefab);
            obj.transform.SetParent(HeadRoot, false);
        }
    }
}
