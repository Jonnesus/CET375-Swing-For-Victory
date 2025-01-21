using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager instance;

    public LevelTimer levelTimer;

    public GameObject player;
    //public GameObject roomCam;
    //public GameObject connectionPanel;
    //public GameObject nicknamePanel;
    public GameObject nicknameField;

    public Transform spawnPoint;
    public Transform resultsPoint;

    public string roomNameToJoin = "Test Room";
    public string mapName = "None";

    private string nickname = "Unnamed";

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        JoinRoomButtonPressed();
    }

    public void ChangeNickname(string _name)
    {
        nickname = _name;
    }

    public void SetNicknameButtonPressed()
    {
        PhotonNetwork.LocalPlayer.NickName = nickname;
    }

    public void JoinRoomButtonPressed()
    {
        Debug.Log("Connecting...");

        RoomOptions ro = new RoomOptions() { MaxPlayers = 5, IsVisible = true, IsOpen = true };

        ro.CustomRoomProperties = new Hashtable()
        {
            {"mapSceneIndex", SceneManager.GetActiveScene().buildIndex},
            {"mapName", mapName}
        };

        ro.CustomRoomPropertiesForLobby = new []
        {
            "mapSceneIndex",
            "mapName"
        };

        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("RoomNameToJoin"), ro, null);
        //nicknamePanel.SetActive(false);
        //connectionPanel.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Debug.Log("Joined room");
        //roomCam.SetActive(false);
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

    public void MoveToResults()
    {
        if (levelTimer.levelCompleted)
        {
            GameObject _player = PhotonNetwork.Instantiate(player.name, resultsPoint.position, Quaternion.identity);
            _player.GetComponentInChildren<PlayerSetup>().IsLocalPlayer();
            _player.GetComponentInChildren<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
            _player.GetComponentInChildren<PlayerHealth>().localPlayer = true;

            PhotonNetwork.LocalPlayer.NickName = nickname;
        }
    }
}