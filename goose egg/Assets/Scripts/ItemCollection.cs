using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class item_collection : MonoBehaviour
{
    public int eggs=0;

    public AudioSource collectSoundEffect;

    //public Text eggsText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //eggsText.text="Eggs"+0;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Eggs"))
        {
            Destroy(collision.gameObject);
            eggs++;
            collectSoundEffect.Play();
            //eggsText.text = "Eggs"+eggs;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
