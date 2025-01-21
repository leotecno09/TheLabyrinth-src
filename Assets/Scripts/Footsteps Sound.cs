using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip footstepsSound;  // Suono dei passi
    private AudioSource audioSource;  // Riferimento all'AudioSource
    public float stepInterval = 0.5f;  // Intervallo tra i passi
    private float stepTimer;  // Timer per gestire l'intervallo tra i passi

    private CharacterController characterController;  // Riferimento al CharacterController
    private Vector3 previousPosition;  // Posizione precedente del personaggio

    void Start()
    {
        // Ottieni il componente AudioSource e CharacterController
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        stepTimer = stepInterval;
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calcola la velocità del personaggio (la distanza percorsa tra il frame corrente e il precedente)
        float movementSpeed = (transform.position - previousPosition).magnitude / Time.deltaTime;

        // Se il personaggio si sta muovendo e tocca il suolo (isGrounded)
        if (movementSpeed > 0.1f && characterController.isGrounded)
        {
            stepTimer -= Time.deltaTime;

            // Se è passato l'intervallo tra i passi, riproduci il suono
            if (stepTimer <= 0f)
            {
                audioSource.PlayOneShot(footstepsSound);  // Riproduce il suono dei passi
                stepTimer = stepInterval;  // Resetta il timer
            }
        }

        // Aggiorna la posizione precedente per il prossimo frame
        previousPosition = transform.position;
    }
}
