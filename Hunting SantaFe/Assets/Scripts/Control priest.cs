using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class RandomMovementWithAnimationAndWait : MonoBehaviour
{
    public float moveSpeed = 2f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotaci�n
    public Vector2 areaSize = new Vector2(10f, 10f); // Tama�o del �rea (X, Z)
    public Animator animator; // Referencia al componente Animator
    public float waitTime = 2f; // Tiempo de espera en segundos

    private Vector3 targetPosition;
    private Vector3 lastPosition; // Para calcular la velocidad
    private bool isWaiting = false; // Controlar si est� esperando
    private float speed = 0f; // Velocidad actual para animaci�n
    private Transform imageTarget; // Referencia al Image Target

    void Start()
    {
        // Busca el Image Target en la escena
        imageTarget = GameObject.Find("YourImageTargetName").transform; // Cambia "YourImageTargetName" por el nombre de tu Image Target

        // Elegir una posici�n inicial en la direcci�n del Image Target
        SetNewRandomPosition();
        lastPosition = transform.position;
    }

    void Update()
    {
        if (!isWaiting && imageTarget != null)
        {
            // Mover el objeto hacia la posici�n aleatoria
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Calcular la velocidad actual (diferencia entre la posici�n actual y la anterior)
            speed = (transform.position - lastPosition).magnitude / Time.deltaTime;

            // Actualizar el par�metro "Speed" en el Animator para controlar las animaciones
            animator.SetFloat("Speed", speed);

            // Rotar el objeto hacia la direcci�n del movimiento
            Vector3 direction = targetPosition - transform.position;
            if (direction.magnitude > 0.1f) // Solo rotar si hay movimiento
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Si el objeto ha alcanzado la posici�n, iniciar la corrutina de espera
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                StartCoroutine(WaitAtPosition());
            }

            // Actualizar la �ltima posici�n
            lastPosition = transform.position;

            // Comprobar si el modelo est� a punto de atravesar la imagen
            AvoidImageTargetCollision();
        }
    }

    void SetNewRandomPosition()
    {
        // Obtener una direcci�n paralela a la imagen (usamos la direcci�n hacia adelante)
        Vector3 direction = imageTarget.forward; // o imageTarget.right, dependiendo de c�mo quieras que se mueva

        // Elegir una nueva posici�n en la direcci�n paralela
        float randomOffset = Random.Range(-3f, 3f); // Ajusta este rango seg�n sea necesario
        targetPosition = imageTarget.position + direction * 2f + new Vector3(randomOffset, 0, randomOffset);

        // Verificar que la nueva posici�n sea diferente a la actual
        if (targetPosition == transform.position)
        {
            SetNewRandomPosition(); // Volver a calcular si es la misma posici�n
        }
    }

    IEnumerator WaitAtPosition()
    {
        // Activar estado de espera
        isWaiting = true;

        // Detener animaci�n de movimiento (Speed = 0)
        animator.SetFloat("Speed", 0f);

        // Esperar el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        // Elegir una nueva posici�n despu�s de la espera
        SetNewRandomPosition();

        // Desactivar estado de espera
        isWaiting = false;
    }

    void AvoidImageTargetCollision()
    {
        // Raycast para verificar colisi�n con la imagen
        RaycastHit hit;
        float rayDistance = 1f; // Distancia del rayo

        // Lanzar un rayo hacia adelante desde la posici�n actual
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance))
        {
            if (hit.collider.CompareTag("YourImageTargetTag")) // Aseg�rate de que el Image Target tenga este tag
            {
                // Si el raycast golpea la imagen, revertir el movimiento
                Vector3 reverseDirection = -transform.forward;
                transform.position += reverseDirection * moveSpeed * Time.deltaTime; // Revertir el movimiento
            }
        }
    }
}
