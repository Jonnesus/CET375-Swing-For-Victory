using UnityEngine;
using Photon.Pun;

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
                RoomManager.instance.SpawnPlayer();

            Destroy(transform.parent.gameObject);
        }
    }
}