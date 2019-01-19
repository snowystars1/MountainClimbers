using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkManage : NetworkManager
{
    public GameObject player0;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    private GameObject MainCamera;
    private MouseOrbitImprovedMulti Mouse;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if(conn.connectionId == 0)
        {
            GameObject climber = Instantiate(player0, new Vector3(-50f, -.75f, -5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, climber, playerControllerId);
        }
        if(conn.connectionId == 1)
        {
            GameObject climber1 = Instantiate(player1, new Vector3(-50f, -.75f, -10f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, climber1, playerControllerId);
        }
        if(conn.connectionId == 2)
        {
            GameObject climber2 = Instantiate(player2, new Vector3(-50f, -.75f, 10f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, climber2, playerControllerId);
        }
        if(conn.connectionId == 3)
        {
            GameObject climber3 = Instantiate(player3, new Vector3(-50f, -.75f, 5f), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
            NetworkServer.AddPlayerForConnection(conn, climber3, playerControllerId);
        }
        base.OnServerAddPlayer(conn, playerControllerId);
    }
}
