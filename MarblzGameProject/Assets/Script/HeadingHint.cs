using UnityEngine;

public class HeadingHint : MonoBehaviour {
    public Transform Line;

    public void SetDistance(float distance) {
    //    Line.localScale = new Vector3(distance, 1, 1);
    }

    public void SetHeading(float heading) {
        transform.rotation = Quaternion.Euler(0, 0, heading);
    }
}