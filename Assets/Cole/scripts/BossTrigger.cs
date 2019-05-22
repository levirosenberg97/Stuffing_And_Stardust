using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossTrigger : MonoBehaviour {

    public AudioSource AudioFile;

    

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if ( other.gameObject.CompareTag("Player"))
        {
                    AudioFile.Play();
                    Cursor.visible = true;
                    SceneManager.LoadScene("BossScene");
                    Debug.Log("Fight Time");
                    Destroy(this.gameObject);
        }
    }
}
