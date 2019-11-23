using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocateNearestGuard : MonoBehaviour
{
    public Transform bestGuard = null;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Guard")
        {
            Transform GetClosestGuard(List<Transform> guards, Transform speakerDetector)
            {
                float closestDistanceSqr = Mathf.Infinity;
                Vector2 currentPosition = speakerDetector.position;
                foreach (Transform potentialChoice in guards)
                {
                    Vector2 directionToTarget = potentialChoice.position - speakerDetector.position;
                    float dSqrToTarget = directionToTarget.sqrMagnitude;
                    if (dSqrToTarget < closestDistanceSqr)
                    {
                        closestDistanceSqr = dSqrToTarget;
                        bestGuard = potentialChoice;
                    }
                }
                return bestGuard;
            }
        }
    }
}
