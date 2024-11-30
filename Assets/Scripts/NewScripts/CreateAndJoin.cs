using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public void JoinRoomFromList(string RoomName)
    {
        PhotonNetwork.JoinOrCreateRoom(RoomName, new RoomOptions() { MaxPlayers = 4, IsVisible = true, IsOpen = true }, TypedLobby.Default, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("FirstLevel");
    }
}