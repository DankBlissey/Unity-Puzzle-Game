using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    private bool thingsMoving;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;
    private bool hasMoved;
    private string whatHit;
    [SerializeField] private float GoalAnimLeng = 3;
    [SerializeField] private float GoalShakeLeng = 1;
    public AudioClip bump;
    public AudioClip slide;
    public AudioClip whoosh1;
    public AudioClip whoosh2;
    public AudioClip whoosh3;
    public AudioClip whoosh4;
    public AudioClip whoosh5;
    private List<AudioClip> whooshList = new List<AudioClip>();


    // Start is called before the first frame update
    void Start()
    { 
        hasMoved = false;
    }

    void Awake()
    {
        whooshList.Add(whoosh1);
        whooshList.Add(whoosh2);
        whooshList.Add(whoosh3);
        whooshList.Add(whoosh4);
        whooshList.Add(whoosh5);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!isMoving && !thingsMoving)
        {
            
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 moving = new Vector2(h, v);

            if((h != 0) != (v != 0))
            {
                if (!hasMoved)
                {
                    GameObject[] player = GameObject.FindGameObjectsWithTag("Invisible");
                    foreach (GameObject i in player)
                    {
                        i.GetComponent<Invisibleblock>().Hide();
                        
                        hasMoved = true;
                    }
                    GameObject[] text = GameObject.FindGameObjectsWithTag("InvisibleText");
                    foreach (GameObject i in text)
                    {
                        i.GetComponent<TextDissapear>().Hide();
                    }
                }
                StartCoroutine(Move(moving));
            }
        }

    }

    public float getMoveDuration()
    {
        return moveDuration;
    }

    private IEnumerator Move(Vector2 direction)
    {
        Vector2 adj = direction * 0.5f * gridSize;
        //Vector2 adj = direction * 0.45f * gridSize;

        isMoving = true;

        Vector2 startPos = transform.position;

        Vector2 hitObj1 = RayMove(startPos, direction, adj, false);

        Vector2 hitObj = hitObj1 - adj;

        //float distance = Vector2.Distance(startPos, hitObj);

        //set 10 as the distance for increasing speed limit, if 0.2 then 0.2s to go 10 blocks is the limit
        float normMoveDuration = moveDuration;
        //if(distance > 10)
        //{
        //    normMoveDuration = moveDuration * Vector2.Distance(startPos, hitObj) * 0.1f;
        //}
        
        float elapsedTime = 0;
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();

        if (Vector2.Distance(hitObj1, startPos) < 1)
        {
            Debug.Log("Moving into something next to you");
            transform.position = startPos + (hitObj1 - startPos) * 0.1f;
            startPos = transform.position;
            audioSource.PlayOneShot(bump);
        } else
        {
            int setIndex = Random.Range(0, whooshList.Count - 1);
            AudioClip whooshToUse = whooshList[setIndex];
            audioSource.PlayOneShot(whooshToUse);
        }
        

        while (elapsedTime < normMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / normMoveDuration;
            transform.position = Vector2.Lerp(startPos, hitObj, percent);
            yield return null;
        }

        hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
        transform.position = hitObj;

        //yield return new WaitForSeconds(0.1f);
        //yield return null;
        
        switch (whatHit)
        {
            case "Sticky":
                Debug.Log("Hit sticky block");
                break;
            case "Teleport":
                foreach (GameObject i in GameObject.FindGameObjectsWithTag(whatHit)) 
                {
                    i.GetComponent<Teleport>().Activate(startPos);
                }
                break;
            case "Button":
                Debug.Log("Hit button");
                audioSource.Play();
                foreach (GameObject i in GameObject.FindGameObjectsWithTag(whatHit))
                {
                    i.GetComponent<PressButton>().Activate();
                }
                break;
            case "Goal":
                Debug.Log("Goal Reached");
                ThingsMoving();
                StartCoroutine(GoalReached());
                break;
            default:

                
                break;
        }
        
        

        GameObject.FindGameObjectWithTag("Moves").GetComponent<MoveCounter>().AddMoves(1);

        isMoving = false;
    }

    private Vector2 RayMove(Vector2 startPos, Vector2 direction, Vector2 adj, bool ignore)
    {

        Vector2 hitObj = startPos;

        RaycastHit2D hit;

        if(ignore) 
        {
            //LayerMask mask = LayerMask.GetMask("default");
            Debug.Log("masked ray cast");
            LayerMask mask = (LayerMask.GetMask("Default"));
            hit = Physics2D.Raycast(startPos, direction, 100f, mask);
        } else
        {
            hit = Physics2D.Raycast(startPos, direction, 100f);
        }
        

        if (hit)
        {
            //Debug.Log("hit object at " + hit.point);
            hitObj = hit.point;
            if (hit.transform.gameObject.tag == "Button")
            {
                PressButton button = hit.transform.GetComponent<PressButton>();
                button.IncomingHit();
                whatHit = "Button";
            } else if (hit.transform.gameObject.tag == "Teleport")
            {
                Teleport teleport = hit.transform.GetComponent<Teleport>();
                hitObj = hit.point + adj * 2;
                teleport.IncomingHit();
                whatHit = "Teleport";
            } else if (hit.transform.gameObject.tag == "Sticky")
            {
                hitObj = hit.point + adj * 2;
                whatHit = "Sticky";
            } else if (hit.transform.gameObject.tag == "Doorway" && ignore == false)
            {
                Doorway doorway = hit.transform.GetComponent<Doorway>();
                doorway.IncomingHit();
                hitObj = RayMove(startPos, direction, adj, true);
            } else if (hit.transform.gameObject.tag == "Goal")
            {
                whatHit = "Goal";
                hitObj = hit.point + adj * 2;
            } else
            {
                whatHit = "";
            }
        }
        return hitObj;
    }

    public void Teleport(Vector2 position, Vector2 direction)
    {
        isMoving = true;
        //GetComponent<TrailRenderer>().emitting = false;
        //Vector2 pos = position + direction;
        GameObject newPlayer = Instantiate(gameObject, position = new Vector3(position.x, position.y), transform.rotation);
        newPlayer.GetComponent<Movement>().movePlz(direction);
        //newPlayer.GetComponent<Movement>().setIsMoving(true);
        gameObject.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("mid");
        gameObject.GetComponent<TrailRenderer>().autodestruct = true;
        //transform.position = position + direction;
        //GetComponent<TrailRenderer>().Clear();
        //GetComponent<TrailRenderer>().emitting = true;
        //ThingsMoved();
        //StartCoroutine(Move(direction));
        //StartCoroutine(TeleportRun(position, direction));
    }

    public void movePlz(Vector2 direction)
    {
        StartCoroutine(Move(direction));
    }

    public void setIsMoving(bool isIt)
    {
        isMoving = isIt;
    }
    
    /*
    public IEnumerator TeleportRun(Vector2 position, Vector2 direction)
    {
        transform.position = position + direction;
        
        GetComponent<TrailRenderer>().Clear();
        //yield return null;
        GetComponent<TrailRenderer>().emitting = true;
        ThingsMoved();
        StartCoroutine(Move(direction));
        yield return null;
    }
    */
    
    public void ThingsMoving()
    {
        thingsMoving = true;
    }

    public void ThingsMoved()
    {
        thingsMoving = false;
    }

    public IEnumerator GoalReached()
    {
        GameObject.FindGameObjectWithTag("Goal").GetComponent<ReachGoal>().CompleteLevel();
        SpriteRenderer sprRen = gameObject.GetComponent<SpriteRenderer>();
        Vector3 startScale = transform.localScale;
        float timeElapsed = 0.0f;
        Vector3 originalPos = transform.position;
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.2f;
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        while(timeElapsed < GoalShakeLeng)
        {
            transform.position = originalPos + new Vector3(0.05f, 0);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(0.05f, 0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(0, 0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(-0.05f, 0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(-0.05f, 0);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(-0.05f, -0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(0f, -0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            transform.position = originalPos + new Vector3(0.05f, -0.05f);
            yield return null;
            timeElapsed += Time.deltaTime;
            audioSource.PlayOneShot(bump);
        }
        transform.position = originalPos;
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(slide);
        timeElapsed = 0.0f;
        while (timeElapsed < GoalAnimLeng)
        {
            timeElapsed += Time.deltaTime;
            float percent = timeElapsed / GoalAnimLeng;
            float r = Mathf.Lerp(1, 0, percent);
            sprRen.color = new Color(r, 0, 0);
            transform.localScale = Vector3.Lerp(startScale, new Vector3(0, 0, 1), percent);
            yield return null;
        }

        audioSource.volume = 1;

        SceneManager.LoadScene("Level select");
        
        //CompleteLevel();
    }


    /*
            else if (hit.transform.gameObject.tag == "Invisible")
            {
                Invisibleblock invisibleblock = hit.transform.GetComponent<Invisibleblock>();
                invisibleblock.IncomingHit();
            }
    */
}
