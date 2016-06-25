using UnityEngine;
using System.Collections;

public class Tap : MonoBehaviour {

    public GameObject scoreObject;
    Score score;
    Spawn spawn;
    public GameObject spawnObject;
    protected virtual void OnEnable()
    {
        // Hook into the OnFingerTap event
        Lean.LeanTouch.OnFingerTap += OnFingerTap;
        score = scoreObject.GetComponent<Score>();
        spawn = spawnObject.GetComponent<Spawn>();
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
            spawn.spawnedObjects.Remove(hit.transform.gameObject.GetComponent<Steering>());
            Destroy(hit.transform.gameObject);
            score.ChangeScore(10);
            
        }
    }
}
