using Photon.Pun;
using UnityEngine;

public class DeathPlaneRespawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, 10);
        }
    }
}