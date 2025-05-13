using UnityEngine;

public class catapult : MonoBehaviour
{
    public Transform launchPosition; // Katapultin kärki, josta hahmo lähtee
    public Rigidbody characterRigidbody; // Hahmon Rigidbody
    public float launchForce = 500f; // Voima, jolla hahmo ammutaan
    public Vector3 launchDirection = new Vector3(1, 1, 0); // Lentosuunnan määrittely

    private bool isLoaded = true; // Tarkistetaan onko hahmo valmis

    public Animator catapultAnimator;

    void Start()
    {
        
            ResetCatapult();
          
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isLoaded)
        {

            Launch();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            
            Debug.Log("reset");
            ResetCatapult();
        }

        
    }

    void Launch()
    {
        if (characterRigidbody != null)
        {
            
            Debug.Log(launchDirection);
            Debug.Log(launchPosition.position);
            Debug.Log(launchForce);

            characterRigidbody.transform.position = launchPosition.position; // Aseta hahmo lähtöpisteeseen
            characterRigidbody.isKinematic = false; // Anna fysiikan vaikuttaa

            catapultAnimator.SetTrigger("Fire"); // Käynnistä animaatio
            characterRigidbody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
            
            
            
            isLoaded = false; // Estetään useampi laukaisu ilman resettiä
        }
    }

    void ResetCatapult()
{
    characterRigidbody.linearVelocity = Vector3.zero;
    characterRigidbody.angularVelocity = Vector3.zero;
    characterRigidbody.isKinematic = true;
    characterRigidbody.transform.position = launchPosition.position;
    characterRigidbody.transform.rotation = Quaternion.Euler(0, -90, 0);
    isLoaded = true;
}
}
