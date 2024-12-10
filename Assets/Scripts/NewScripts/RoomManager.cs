using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public GameObject player;
    public GameObject roomCam;
    public GameObject connectionPanel;
    public GameObject nicknamePanel;

    public Transform spawnPoint;

    private string nickname = "Unnamed";

    private void Awake()
    {
        instance = this;
    }

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
        nicknamePanel.SetActive(false);
        connectionPanel.SetActive(true);
    }

/*    private void Start()
    {
        Debug.Log("Connecting...");

        PhotonNetwork.ConnectUsingSettings();
    }*/

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to master");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        Debug.Log("Joined Lobby");

        PhotonNetwork.JoinOrCreateRoom("Test", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Joined room");
        roomCam.SetActive(false);

        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawnPoint.position, Quaternion.identity);
        _player.GetComponentInChildren<PlayerSetup>().IsLocalPlayer();
        _player.GetComponentInChildren<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
        _player.GetComponentInChildren<PlayerHealth>().localPlayer = true;

        PhotonNetwork.LocalPlayer.NickName = nickname;
    }
}