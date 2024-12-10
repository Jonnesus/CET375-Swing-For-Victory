using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class PlayerHealth : MonoBehaviour
{
    public int health = 10;
    public bool localPlayer;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if (health <= 0)
        {
            if (localPlayer)
            {
                RoomManager.instance.SpawnPlayer();
                PhotonNetwork.LocalPlayer.AddScore(1);
            }

            Destroy(transform.parent.gameObject);
        }
    }
}