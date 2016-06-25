using UnityEngine;
using System.Collections;

public class Tap : MonoBehaviour {

    public GameObject scoreObject;
    Score score;


    protected virtual void OnEnable()
    {
        // Hook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
        score = scoreObject.GetComponent<Score>();
    }

    protected virtual void OnDisable()
    {
        // Unhook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap -= OnFingerTap;
    }

    public void OnFingerTap(Lean.LeanFinger finger)
    {
        // Raycast information
        var ray = finger.GetRay();
        var hit = default(RaycastHit);

        // Was this finger pressed down on a collider?
        if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Blobs")))
        {
            Destroy(hit.transform.gameObject);
            score.ChangeScore(10);
        }
    }
}
