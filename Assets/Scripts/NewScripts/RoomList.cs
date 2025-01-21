using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomList : MonoBehaviourPunCallbacks
{
    public static RoomList instance;

    public GameObject createNewRoomButton;
    public GameObject maximumRoomsDisplay;

    [Header("UI")]
    public Transform roomListParent;
    public GameObject roomListItenPrefab;

    private List<RoomInfo> cachedRoomList = new List<RoomInfo>();

    private string cachedRoomNameToCreate;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
        }

        yield return new WaitUntil(() => !PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public void ChangeRoomToCreateName(string _roomName)
    {
        cachedRoomNameToCreate = _roomName;
    }

    public void CreateRoomByIndex(int _sceneIndex)
    {
        JoinRoomByName(cachedRoomNameToCreate, _sceneIndex);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        base.OnRoomListUpdate(roomList);

        if (cachedRoomList.Count <= 0)
        {
            cachedRoomList = roomList;
        }
        else
        {
            foreach (var room in roomList)
            {
                for (int i = 0; i < cachedRoomList.Count; i++)
                {
                    if (cachedRoomList[i].Name == room.Name)
                    {
                        List<RoomInfo> newList = cachedRoomList;

                        if (room.RemovedFromList)
                        {
                            newList.Remove(newList[i]);
                        }
                        else
                        {
                            newList[i] = room;
                        }

                        cachedRoomList = newList;
                    }
                }
            }
        }

        if (cachedRoomList.Count >= 4)
        {
            createNewRoomButton.SetActive(false);
            maximumRoomsDisplay.SetActive(true);
        }
        else
        {
            createNewRoomButton.SetActive(true);
            maximumRoomsDisplay.SetActive(false);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (Transform roomItem in roomListParent)
        {
            Destroy(roomItem.gameObject);
        }

        foreach (var room in cachedRoomList)
        {
            GameObject roomItem = Instantiate(roomListItenPrefab, roomListParent);
            string roomMapName = "";

            object mapNameObject;

            if (room.CustomProperties.TryGetValue("mapName", out mapNameObject))
                roomMapName = (string)mapNameObject;

            roomItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = room.Name + " (" + roomMapName + ")";
            roomItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/5";

            roomItem.GetComponent<RoomItemButton>().roomName = room.Name;

            int roomSceneIndex = 1;
            object sceneIndexObject;

            if (room.CustomProperties.TryGetValue("mapSceneIndex", out sceneIndexObject))
                roomSceneIndex = (int)sceneIndexObject;

            roomItem.GetComponent<RoomItemButton>().sceneIndex = roomSceneIndex;
        }
    }

    public void JoinRoomByName(string _name, int _sceneIndex)
    {
        PlayerPrefs.SetString("RoomNameToJoin", _name);

        gameObject.SetActive(false);

        SceneManager.LoadScene(_sceneIndex);
    }
}