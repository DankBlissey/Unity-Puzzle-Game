using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private bool isMoving;
    private bool thingsMoving;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDuration = 0.1f;
    private bool hasMoved;
    
    // Start is called before the first frame update
    void Start()
    {
        hasMoved = false;
    }

    // Update is called once per frame
    void Update()
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
        //Vector2 adjFar = direction * 0.45f * gridSize;

        isMoving = true;

        Vector2 startPos = transform.position;

        Vector2 hitObj = startPos;

        hitObj = RayMove(startPos, direction, adj, false);

        //float distance = Vector2.Distance(startPos, hitObj);

        //set 10 as the distance for increasing speed limit, if 0.2 then 0.2s to go 10 blocks is the limit
        float normMoveDuration = moveDuration;
        //if(distance > 10)
        //{
        //    normMoveDuration = moveDuration * Vector2.Distance(startPos, hitObj) * 0.1f;
        //}
        
        float elapsedTime = 0;

        while(elapsedTime < normMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float percent = elapsedTime / normMoveDuration;
            transform.position = Vector2.Lerp(startPos, hitObj, percent);
            yield return null;
        }

        hitObj = new Vector2(Mathf.Round(hitObj.x * 2) / 2, Mathf.Round(hitObj.y * 2) / 2);
        transform.position = hitObj;

        //yield return new WaitForSeconds(0.1f);

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
            Debug.Log("hit object at " + hit.point);
            hitObj = hit.point - adj;
            if (hit.transform.gameObject.tag == "Button")
            {
                Button button = hit.transform.GetComponent<Button>();
                button.IncomingHit();
            } else if (hit.transform.gameObject.tag == "Teleport")
            {
                Teleport teleport = hit.transform.GetComponent<Teleport>();
                teleport.IncomingHit();
            } else if (hit.transform.gameObject.tag == "Sticky")
            {
                hitObj = hit.point + adj;
            } else if (hit.transform.gameObject.tag == "Doorway" && ignore == false)
            {
                Doorway doorway = hit.transform.GetComponent<Doorway>();
                doorway.IncomingHit();
                hitObj = RayMove(startPos, direction, adj, true);
            }
        }
        return hitObj;
    }

    public void Teleport(Vector2 position, Vector2 direction)
    {
        isMoving = true;
        transform.position = position + direction;
        StartCoroutine(Move(direction));
    }
    public void ThingsMoving()
    {
        thingsMoving = true;
    }

    public void ThingsMoved()
    {
        thingsMoving = false;
    }

    /*
            else if (hit.transform.gameObject.tag == "Invisible")
            {
                Invisibleblock invisibleblock = hit.transform.GetComponent<Invisibleblock>();
                invisibleblock.IncomingHit();
            }
    */
}
