using UnityEngine;

// This script allows you to drag this GameObject using any finger, as long it has a collider
public class Drag : MonoBehaviour
{
    // This stores the layers we want the raycast to hit (make sure this GameObject's layer is included!)
    public LayerMask LayerMask = UnityEngine.Physics.DefaultRaycastLayers;

    // This stores the finger that's currently dragging this GameObject
    private Lean.LeanFinger draggingFinger;

    Score score;

    GameObject holdingObject;

    protected virtual void OnEnable()
    {
        // Hook into the OnFingerDown event
        Lean.LeanTouch.OnFingerDown += OnFingerDown;

        // Hook into the OnFingerUp event
        Lean.LeanTouch.OnFingerUp += OnFingerUp;

        score = GameObject.Find("Score").GetComponent<Score>();
    }

    protected virtual void OnDisable()
    {
        // Unhook the OnFingerDown event
        Lean.LeanTouch.OnFingerDown -= OnFingerDown;

        // Unhook the OnFingerUp event
        Lean.LeanTouch.OnFingerUp -= OnFingerUp;
    }
    public float DragSpd = 4.0f;
    protected virtual void Update()
    {
        // If there is an active finger, move this GameObject based on it
        if (draggingFinger != null && holdingObject != null  )
        {
            //holdingObject.GetComponent<Steering>().enabled = false;
          //  Lean.LeanTouch.MoveObject(holdingObject.transform, draggingFinger.DeltaScreenPosition);
            var ray = draggingFinger.GetRay();
            var hit = default(RaycastHit);
            if (Physics.Raycast(ray, out hit, float.PositiveInfinity, -1 )) {
                var str = holdingObject.GetComponentInChildren<BlobSim>().HigherBlob.GetComponent<Steering>();
                str.Vel = Vector2.Lerp(str.Vel, (Vector2)hit.point - ((Vector2)str.transform.position + str.Vel*0.5f), DragSpd *Time.deltaTime) *3.0f;
            }
           
        } else {
            draggingFinger = null;
            holdingObject = null;

        }
    }
    public LayerMask BlobMask;
    public void OnFingerDown(Lean.LeanFinger finger)
    {
        // Raycast information
        var ray = finger.GetRay();
        var hit = default(RaycastHit);

        // Was this finger pressed down on a collider?
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, BlobMask ))
        {
            
            holdingObject = hit.collider.transform.parent.gameObject;
            Debug.Log("click  "+holdingObject.name);
            // Was that collider this one?
           // if (hit.collider.gameObject == gameObject)
           // {
                // Set the current finger to this one
                draggingFinger = finger;
           // }
        }
    }
    public float SwipeMd = 4;
    public void OnFingerUp(Lean.LeanFinger finger)
    {
        // Was the current finger lifted from the screen?
        if (finger == draggingFinger)
        {

            if(holdingObject != null) {

                var str = holdingObject.GetComponentInChildren<BlobSim>().HigherBlob.GetComponent<Steering>();
                str.Vel += draggingFinger.SwipeDelta * SwipeMd;
                holdingObject =null;
            }

            // Unset the current finger
            draggingFinger = null;

           // holdingObject.GetComponent<Steering>().enabled = true;
  

            //// Was this finger pressed down on a collider?
            //if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Blobs2")))
            //{
            //    GetComponent<Steering>().ComboColors(hit.transform.gameObject.GetComponent<Steering>().color);
            //}
        }
    }

    

}