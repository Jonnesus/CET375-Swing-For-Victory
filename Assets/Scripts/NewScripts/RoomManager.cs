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

    public string roomNameToJoin = "Test Room";

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

        PhotonNetwork.JoinOrCreateRoom(roomNameToJoin, new Photon.Realtime.RoomOptions() { MaxPlayers = 5, IsVisible = true, IsOpen = true }, null);
        nicknamePanel.SetActive(false);
        connectionPanel.SetActive(true);
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