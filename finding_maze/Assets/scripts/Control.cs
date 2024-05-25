using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class Control : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float rotationSpeed = 100.0f;
    private float moveSpeed = 10.0f;
    private Transform playerTr;
    private int key = 0;
    public AudioClip keySfx;
    private AudioSource audioSource;
    public GameObject keyEffect;
    public GameObject doorGroup;
    public GameObject textUI;
    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        // Debug.Log("H: " + h.ToString() + ", V: " + v.ToString());
        playerTr.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
        playerTr.Rotate(new Vector3(0, r, 0) * rotationSpeed * Time.deltaTime);
    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("KEY"))
        {
      
            Vector3 keyPosition = collision.gameObject.GetComponent<Transform>().position;
            GameObject Effect = Instantiate(keyEffect, keyPosition, Quaternion.identity);
            Destroy(Effect, 2.0f);
            Destroy(collision.gameObject);
            key += 1;
            Debug.Log("Key: " + key.ToString());
            audioSource.PlayOneShot(keySfx, 1.0f);
            textUI.GetComponent<Text>().text = "3개의 열쇠를 찾으세요!\n찾은 열쇠: " + key.ToString();

        }

        if(collision.gameObject.tag == "DOOR" && key >= 3)
        {
            Destroy(doorGroup);
            collision.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }

        
        
        
    }

}
