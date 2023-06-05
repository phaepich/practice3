using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource musicSource;
    [SerializeField] private GameObject _gameObject;
    private bool k = false;
    public Transform objectToMove; // Объект, который нужно переместить
    public Vector3 targetPosition; // Целевая позиция, куда нужно переместить объект
    public bool conditionMet; // Условие, которое должно быть выполнено для начала движения объекта
    public float moveSpeed = 5f;
    private bool playerEnteredTrigger; // Флаг, указывающий, что игрок находится в триггере
    
    void Start () 
    {
        musicSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (gameObject.name == "Trigger1")
            {
                _gameObject.transform.position = new Vector3(506.01f, 7.672f , 500.647f);
                _gameObject.transform.rotation = new Quaternion(0.0506434217f,0.992991865f,-0.00543892104f,-0.106643818f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
            }
            if (gameObject.name == "Trigger2")
            {
                _gameObject.transform.position = new Vector3(521.974f, 5.174f , 501.793f);
                _gameObject.transform.rotation = new Quaternion(-0.230122626f,0.968811214f,0.000378430646f,-0.0919149145f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
            }

            if (gameObject.name == "Trigger3")
            {
                playerEnteredTrigger = true;
                _gameObject.transform.position = new Vector3(526.155f, 13.22f , 512.128f);
                _gameObject.transform.rotation = new Quaternion(-0.0470253006f,0.994655788f,-0.0166228134f,-0.0904000923f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
                
            }
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            if (gameObject.name == "Trigger1")
            {
                _gameObject.transform.position = new Vector3(0, 0, 0);
                _gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            if (gameObject.name == "Trigger2")
            {
                _gameObject.transform.position = new Vector3(0, 0, 0);
                _gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            if (gameObject.name == "Trigger3")
            {
                playerEnteredTrigger = false;
                _gameObject.transform.position = new Vector3(0, 0, 0);
                _gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            
        }
    }
    private void Update()
    {
        if (playerEnteredTrigger && conditionMet)
        {
            MoveObject();
        }
    }
    private void MoveObject()
    {
        objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
