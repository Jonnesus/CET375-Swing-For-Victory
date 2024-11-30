using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    public TextMeshProUGUI Name;

    public void JoinRoom()
    {
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoomFromList(Name.text);
    }
}