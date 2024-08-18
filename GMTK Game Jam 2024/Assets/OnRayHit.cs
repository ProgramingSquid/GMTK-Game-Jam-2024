using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OnRayHit : MonoBehaviour
{
    bool hasHit;
    public float distance;
    public Transform rayPosAndDir;
    [Tag]
    public string tagFilter;
    public UnityEvent OnHit;

    private void Update()
    {
        if (hasHit || ScoreDisplay.instance.startGame == false) return;

        Debug.DrawRay(rayPosAndDir.position, rayPosAndDir.right * distance, Color.red);
        var hit = Physics2D.Raycast(rayPosAndDir.position, rayPosAndDir.right, distance);

        if(hit == false) return;
        Debug.Log("hit");
        if (hit.collider.CompareTag(tagFilter))
        {
            Debug.Log("hit tag!");
            hasHit = true;
            OnHit.Invoke();
        }
    }
}
