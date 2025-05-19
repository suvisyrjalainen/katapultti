using UnityEngine;

public class catapult : MonoBehaviour
{
    public Transform launchPosition; // Katapultin kärki, josta hahmo lähtee
    public Rigidbody characterRigidbody; // Hahmon Rigidbody
    public float launchForce = 500f; // Voima, jolla hahmo ammutaan

    //Tämä ampuu hahmon suoraan eteenpäin
    //public Vector3 launchDirection = new Vector3(1, 1, 0); // Lentosuunnan määrittely

    // Tämä ampuu hahmon sinne minne katapultti osoittaa
    public Vector3 launchDirection = Vector3.forward; // tämä päivittyy
    
    public float rotationSpeed = 50f;
    private bool isLoaded = true; // Tarkistetaan onko hahmo valmis

    public Animator catapultAnimator;

    public Transform rotator; // ← TÄMÄ on Catapult_Rotator GameObject

    void Start()
    {

        ResetCatapult();
        
    }

    void Update()
    {
        HandleRotation();

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

            //characterRigidbody.transform.position = launchPosition.position; // Aseta hahmo lähtöpisteeseen
            characterRigidbody.transform.parent = null; // Irrota pupu katapultista ennen lentoa

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

        // Siirrä pupu takaisin laukaisukohtaan
        characterRigidbody.transform.position = launchPosition.position;

        // Aseta sama suunta kuin katapultin kärki ja käännä pupua 180 astetta
        characterRigidbody.transform.rotation = launchPosition.rotation * Quaternion.Euler(0, 180, 0);

        // Tee pupusta taas katapultin "lapsi", jotta se seuraa kääntöjä
        characterRigidbody.transform.parent = launchPosition;

        isLoaded = true;
    }


    void HandleRotation()
    {
        float h = Input.GetAxis("Horizontal"); // ← ohjaus nuolinäppäimillä tai A/D
        rotator.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime);


        // Päivitetään launchDirection pyörimissuunnan mukaan:
        // Tällä pupu menee ampuuntuu liian ylös
        launchDirection = rotator.forward + Vector3.up * 0.6f; // hieman ylös + eteen

        // Päivitetään pupu lähtöpisteeseen kun pupua pyöritetään:                                                    
        if (characterRigidbody.isKinematic)
        {
            characterRigidbody.transform.position = launchPosition.position;
            characterRigidbody.transform.rotation = launchPosition.rotation * Quaternion.Euler(0, 180, 0); // Jos tarvitaan käännös
        }
    }
}
