using UnityEngine;

public class laatikko10 : MonoBehaviour
{
    private bool hasScored = false;
    private int score = 0; // Muuttuja pistemäärälle

    void OnCollisionEnter(Collision collision)
    {
        if (!hasScored && collision.gameObject.CompareTag("Ground"))
        {
            hasScored = true;
            score += 10; // Lisää pisteitä
            Debug.Log("Pisteet: " + score); // Tulosta pistemäärä konsoliin
            //ScoreManager.Instance.AddScore(10);
        }
    }
}
