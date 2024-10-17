using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void interacciones()
    {
        int numeroescena = gameObject.scene.buildIndex;
        SceneManager.LoadScene(numeroescena + 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        interacciones();
    }
}
