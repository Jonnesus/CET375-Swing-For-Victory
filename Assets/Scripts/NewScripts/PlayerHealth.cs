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

        if (health == 5)
        {
            if (localPlayer)
            {
                RoomManager.instance.SpawnPlayer();
                PhotonNetwork.LocalPlayer.AddScore(1);
            }

            Destroy(transform.parent.gameObject);
        }
        else if (health <= 0)
        {
            if (localPlayer)
            {
                RoomManager.instance.SpawnPlayer();
            }

            Destroy(transform.parent.gameObject);
        }
    }
}