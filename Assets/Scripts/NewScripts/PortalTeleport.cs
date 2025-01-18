using Photon.Pun;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 5);
        }
    }
}