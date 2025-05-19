using UnityEngine;

public class laatikko10 : MonoBehaviour
{
    private bool hasScored = false;

    void OnCollisionEnter(Collision collision)
    {
        if (!hasScored && collision.gameObject.CompareTag("Ground"))
        {
            hasScored = true;
            ScoreManager.Instance.AddScore(10);
        }
    }
}
