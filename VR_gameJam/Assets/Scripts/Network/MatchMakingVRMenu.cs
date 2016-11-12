using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;

public class MatchMakingVRMenu : NetworkLobbyManager
{
    [SerializeField]
    private VRMenuSystem MainVRMenu;
    [SerializeField]
    private VRMenuSystem ScndVRMenu;
    [SerializeField]
    private TextMesh MatchInfo;
    [SerializeField]
    private GameObject ReadyIndicator;

    private int LastMathCount;
    private NetworkLobbyPlayer LobbyPlayer;

    void Awake()
    {
    }

    void Start()
    {
        StartMatchMaker();
        matchName = Environment.MachineName;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        MainVRMenu.MenuInstance.ClearEachFrame = true;
        ScndVRMenu.MenuInstance.ClearEachFrame = true;

        bool noConnection = (client == null || client.connection == null ||
                         client.connection.connectionId == -1);

        //Debug.Log("NetworkServer.active: " + NetworkServer.active);
        //Debug.Log("IsClientConnected(): " + IsClientConnected());
        //Debug.Log("noConnection: " + noConnection);

        if (!NetworkServer.active && !IsClientConnected() && noConnection)
        {
            if (matchMaker != null)
            {
                if (matchInfo == null)
                {
                    if (matches == null)
                    {
                        MainVRMenu.MenuInstance.RegisterButton("Create Internet Match", () =>
                        {
                            Debug.Log("Create Internet Match");
                            //matchMaker.CreateMatch(matchName, matchSize, true, "", OnMatchCreate);
                            return false;
                        });
                        MainVRMenu.MenuInstance.RegisterButton("Find Internet Match", () =>
                        {
                            Debug.Log("Find Internet Match");
                            //matchMaker.ListMatches(0, 20, "", OnMatchList);
                            return false;
                        });
                    }
                    if (matches != null)
                    {
                        if (LastMathCount != matches.Count)
                        {
                            foreach (var match in matches)
                            {
                                MainVRMenu.MenuInstance.RegisterButton(match.name, () =>
                                {
                                    matchName = match.name;
                                    matchSize = (uint)match.currentSize;
                                    //matchMaker.JoinMatch(match.networkId, "", OnMatchJoined);
                                    return true;
                                });
                            }
                        }

                        LastMathCount = matches.Count;
                    }
                }

                MainVRMenu.MenuInstance.RegisterButton("Restart Match Making", () =>
                {
                    Debug.Log("Restart Match Making");
                    StopMatchMaker();
                    StartMatchMaker();
                    return false;
                });
            }
        }
        else
        {
            if (MatchInfo != null)
                MatchInfo.text = matchName;

            LobbyPlayer = FindObjectOfType<NetworkLobbyPlayer>();
            if (LobbyPlayer != null)
            {
                MainVRMenu.MenuInstance.RegisterButton("Ready", () =>
                {
                    LobbyPlayer.SendReadyToBeginMessage();
                    return false;
                });
                MainVRMenu.MenuInstance.RegisterButton("Unready", () =>
                {
                    LobbyPlayer.SendNotReadyToBeginMessage();
                    return false;
                });
            }
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("OnServerConnect");
    }

    public override void OnLobbyServerPlayersReady()
    {
        ServerChangeScene(playScene);
    }
}
