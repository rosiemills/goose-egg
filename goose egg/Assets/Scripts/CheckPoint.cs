using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{

    public AudioSource checkPointSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void LoadNewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        checkPointSound.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    
    {
        Debug.Log("collided");
        if(collision.gameObject.CompareTag("CheckPoint"))
        {
            Debug.Log("checkpoint collided");
            LoadNewScene();
            
        }
        
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
