using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;

public class snakeTail : MonoBehaviour
{
    public List<Transform> Tails;
    public List<GameObject> Tails_Objects;
    //
    [Range(0, 3)]
    public float BonesDistance;
    public GameObject BonePrefab;
    public GameObject pointLeft;
    public GameObject pointRight;
    public GameObject head;
    public GameObject baboom;
    public GameObject based;
    public GameObject cubeParticle;
    private GameObject[] spawn;
    public Animator[] anim;
    //
    [SerializeField] [Range(0f, 100f)] float lerpTime;
    [SerializeField] Vector3[] myPositions;
    float leftElapsedTime;
    float rightElapsedTime;
    float desiredDuration = 0.2f;
    //
    [Range(0, 4)]
    public float Speed;
    private Transform _transform;
    private Transform ground_transform;      
    public Vector3 curVelocity;
    private bool leftSide = true;
    [Range(0, 2)]
    public float smoothTime; 
    private float headDistance;
    //
    public int score;
    public TextMeshProUGUI tailCount;
    public static int times;
    //
    int scoredPoints;
    bool lose = false;
    private AudioSource _audio;
    public AudioClip[] AudioClip;
    [Min(0)]
    public float Volume;

    private void Awake()
    {
        score = 5;
       _transform = GetComponent<Transform>();
       for(int i = 0; i < score; i++)
            {
                var bone = Instantiate(BonePrefab);
                Tails.Add(bone.transform);
                Tails_Objects.Add(bone);
            }
        tailCount.text = score.ToString();
        ground_transform = based.GetComponent<Transform>();
        spawn = GameObject.FindGameObjectsWithTag("spawn");
        _audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        MoveTail(_transform.position + _transform.forward * Speed);
        TurnTail();
        MoveGround(ground_transform.position + ground_transform.forward * Speed);
        


        

        // float angle = Input.GetAxis("Horizontal") * 4;
        // _transform.Rotate(0, angle, 0);
        
        // elapsedTime += Time.deltaTime;
        // float percentageComplete = elapsedTime / desiredDuration;
        
    }

    private void TurnTail()
    {

        var goLeft = Input.GetKey(KeyCode.A);
        var goRight = Input.GetKey(KeyCode.D);

        if(goRight)
        {
            _transform.localPosition = Vector3.Lerp(_transform.localPosition, myPositions[0], 15f * Time.fixedDeltaTime);
          
        }
        
        if(goLeft)
        {
            _transform.localPosition = Vector3.Lerp(_transform.localPosition, myPositions[1], 15f * Time.fixedDeltaTime);
        }

    }

    private void MoveTail(Vector3 newPosition)
    {
        float sqrDistance = BonesDistance * BonesDistance;
        Vector3 prvsBone = _transform.position;
        foreach (var bone in Tails)
        {
           if((bone.position - prvsBone).sqrMagnitude > sqrDistance)
           {
                var temp = bone.position;
                bone.position = prvsBone;
                prvsBone = temp;
           }
           else
           {
                break;
           } 
        }
        // _transform.position = Vector3.SmoothDamp(_transform.position, newPosition, ref curVelocity, smoothTime);
        // ground_transform.position = Vector3.SmoothDamp(ground_transform.position, newPosition, ref curVelocity, smoothTime);
    }
    
    private void MoveGround(Vector3 newPosition)
    {
        ground_transform.position = Vector3.SmoothDamp(ground_transform.position, newPosition, ref curVelocity, smoothTime);
        // ground_transform.position = Vector3.SmoothDamp(ground_transform.position, newPosition, ref curVelocity, smoothTime);
    }

    // private Vector3 MovePosition(Vector3 curPosition, Vector3 newPosition, float percent)
    // {
    //     return Vector3.Lerp(curPosition, newPosition, percent);
    // }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "points")
        {
            var pointsScript = collision.gameObject.GetComponent<points>(); 
            if (pointsScript != null)
            {
                int pointsInObject = pointsScript.recPoints;
                for(int i = 0; i < pointsInObject; i++)
                {
                    var bone = Instantiate(BonePrefab);
                    Tails.Add(bone.transform);
                    Tails_Objects.Add(bone);   
                }
                score += pointsInObject;
                tailCount.text = score.ToString();
                Destroy(collision.gameObject);
            }
            
        }
        else if(collision.gameObject.tag == "cube")
        {
            var pointsScript = collision.gameObject.GetComponent<points>(); 
            if (pointsScript != null)
            {
                int pointsInObject = pointsScript.recPoints;
                
                if(score >= pointsInObject)
                {
                    Instantiate(cubeParticle, _transform);
                    for(int i = 0; i < pointsInObject; i++)
                    {
                        Tails.RemoveAt(0);
                        var bone = Tails_Objects[0];
                        Destroy(bone);
                        Tails_Objects.RemoveAt(0);
                    }
                    score -= pointsInObject;
                    tailCount.text = score.ToString();
                    _audio.PlayOneShot(AudioClip[0], Volume); 
                    Destroy(collision.gameObject);
                }
                else
                {
                    foreach(GameObject spawner in spawn)
                    {
                        spawner.SetActive(false);
                    }
                    foreach(Animator an in anim)
                    {
                        var animation = an.gameObject.GetComponent<Animator>(); 
                        animation.SetBool("lose", true);
                    }
                    lose = true;
                    Speed = 0;
                    _audio.PlayOneShot(AudioClip[1], Volume); 
                    Instantiate(baboom, transform);
                    Debug.Log("GameOver");
                }
                
                
            }
            
        }
        else if(collision.gameObject.tag == "wall")
        {
            foreach(GameObject spawner in spawn)
            {
                spawner.SetActive(false);
            }
            foreach(Animator an in anim)
            {
                an.SetBool("lose", true);
            }
            lose = true;
            Speed = 0;
            Instantiate(baboom, transform);
            _audio.PlayOneShot(AudioClip[1], Volume); 
            Debug.Log("GameOver");
            // head.SetActive(false);
        }
    }

    
    
}
