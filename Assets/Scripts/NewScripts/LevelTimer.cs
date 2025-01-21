using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class LevelTimer : MonoBehaviour
{
    public bool levelCompleted = false;

    [SerializeField] RoomManager roomManager;

    private float currentTime;

    [PunRPC]
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= 30f && levelCompleted)
        {
            levelCompleted = false;

            roomManager.SpawnPlayer();
            currentTime = 0f;
            PhotonNetwork.LocalPlayer.SetScore(0);
        }
        else if (currentTime >= 300f)
        {
            levelCompleted = true;
            roomManager.MoveToResults();
            currentTime = 0f;
        }
    }
}