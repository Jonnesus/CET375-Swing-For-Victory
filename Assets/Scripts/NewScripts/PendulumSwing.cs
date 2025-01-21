using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    private bool randomStart=false;
    private float random = 0;

    private void Awake()
    {
        if (randomStart)
            random = Random.Range(0f, 1f);
    }

    private void Update()
    {
        float angle = 75f * Mathf.Sin(Time.time + random * 1.5f);
        transform.localRotation = Quaternion.Euler(0, 90, angle);
    }
}