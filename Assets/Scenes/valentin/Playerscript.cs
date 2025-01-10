using UnityEngine;

public class Playerscript : MonoBehaviour
{
    public Camera playerCamera;
    private CharacterController characterController;

    public float walkingSpeed = 7.5f;  // Vitesse de marche du joueur
    public float runningSpeed = 15f;  // Vitesse de course du joueur
    public float jumpSpeed = 8f;  // Vitesse du saut
    public float gravity = 20f;  // Gravité appliquée au joueur
    public float rotationSpeed = 2.0f;  // Vitesse de rotation de la caméra et du joueur
    public float rotationXLimit = 45.0f;  // Limite de rotation verticale de la caméra

    private Vector3 moveDirection;  // Direction du mouvement du joueur
    private float rotationX = 0;  // Rotation de la caméra sur l'axe X (haut/bas)

    void Start()
    {
        characterController = GetComponent<CharacterController>();  // Initialisation du CharacterController
    }

    void Update()
    {
        MovePlayer();  // Gère le déplacement du joueur
        RotatePlayer();  // Gère la rotation du joueur et de la caméra
    }

    void MovePlayer()
    {
        // Récupère les entrées du joueur pour les axes de déplacement
        float speedZ = Input.GetAxis("Vertical");  // Mouvement avant/arrière (W/S ou flèches haut/bas)
        float speedX = Input.GetAxis("Horizontal");  // Mouvement gauche/droite (A/D ou flèches gauche/droite)

        // Vérifie si le joueur appuie sur la touche "Shift" pour courir
        bool isRunning = Input.GetKey(KeyCode.LeftShift);  // Si on appuie sur LeftShift, on court

        // Ajuste la vitesse en fonction de si le joueur marche ou court
        float speedMultiplier = isRunning ? runningSpeed : walkingSpeed;  // Choisit la vitesse en fonction de la course ou marche
        speedX *= speedMultiplier;  // Applique le multiplicateur de vitesse pour l'axe X
        speedZ *= speedMultiplier;  // Applique le multiplicateur de vitesse pour l'axe Z

        // Calcule la direction du mouvement
        moveDirection = (transform.TransformDirection(Vector3.forward) * speedZ)  // Mouvement avant/arrière
                       + (transform.TransformDirection(Vector3.right) * speedX);  // Mouvement gauche/droite

        // Gère le saut
        if (Input.GetButton("Jump") && characterController.isGrounded)  // Si le joueur appuie sur "Jump" et est au sol
            moveDirection.y = jumpSpeed;  // Applique la vitesse de saut

        // Applique la gravité
        if (!characterController.isGrounded)  // Si le joueur n'est pas au sol
            moveDirection.y -= gravity * Time.deltaTime;  // Applique la gravité au mouvement vertical

        // Applique le mouvement au CharacterController
        characterController.Move(moveDirection * Time.deltaTime);  // Déplace le joueur selon la direction calculée
    }

    void RotatePlayer()
    {
        // Rotation de la caméra (haut/bas) basée sur le mouvement de la souris
        rotationX -= Input.GetAxis("Mouse Y") * rotationSpeed;  // Calcul de la rotation de la caméra sur l'axe X
        rotationX = Mathf.Clamp(rotationX, -rotationXLimit, rotationXLimit);  // Limite la rotation pour éviter les rotations excessives
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);  // Applique la rotation de la caméra

        // Rotation du joueur (gauche/droite) basée sur le mouvement de la souris
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotationSpeed, 0);  // Applique la rotation du joueur sur l'axe Y
    }
}
