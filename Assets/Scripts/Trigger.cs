using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    private AudioSource musicSource;
    [SerializeField] private GameObject scp;
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
                scp.transform.position = new Vector3(506.01f, 7.672f , 500.647f);
                scp.transform.rotation = new Quaternion(0.0506434217f,0.992991865f,-0.00543892104f,-0.106643818f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
            }
            if (gameObject.name == "Trigger2")
            {
                scp.transform.position = new Vector3(526.139404f,7.42729616f,512.047791f);
                scp.transform.rotation = new Quaternion(-0.230122626f,0.968811214f,0.000378430646f,-0.0919149145f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
            }

            if (gameObject.name == "Trigger3")
            {
                playerEnteredTrigger = true;
                scp.transform.position = new Vector3(525.5909f, 13.22f , 519.312f);
                scp.transform.rotation = new Quaternion(-0.0470253006f,0.994655788f,-0.0166228134f,-0.0904000923f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
                
            }
            
            if (gameObject.name == "Trigger3 (1)")
            {
                playerEnteredTrigger = true;
                scp.transform.position = new Vector3(423.611f, 22.338f , 393.758f); 
                scp.transform.rotation = new Quaternion(-0.0875115246f,0.493071556f,0.00488754362f,-0.865562499f);
                if (!musicSource.isPlaying)
                {
                    musicSource.clip = musicClip;
                    musicSource.Play();
                }
                
            }
            
            if (gameObject.name == "Trigger2 (1)")
            {
                scp.transform.position = new Vector3(450.85f, 2.372f , 502.431f);
                scp.transform.rotation = new Quaternion(-0.0445940532f, 0.995361626f, -0.0754555538f, 0.0396621153f);
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
                scp.transform.position = new Vector3(0, 0, 0);
                scp.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            if (gameObject.name == "Trigger2")
            {
                scp.transform.position = new Vector3(0, 0, 0);
                scp.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }            
            if (gameObject.name == "Trigger2 (1)")
            {
                scp.transform.position = new Vector3(0, 0, 0);
                scp.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
            
        }
    }
    private void Update()
    {
        if (playerEnteredTrigger)
        {
            MoveObject();
            if (objectToMove.position == targetPosition)
            {
                scp.transform.position = new Vector3(0, 0, 0);
                scp.transform.rotation = new Quaternion(0, 0, 0, 0);
                Destroy(gameObject);
            }
        }
    }
    private void MoveObject()
    {
        objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, Time.deltaTime * moveSpeed);
        
    }
}
