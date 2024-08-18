using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class OnCollide : MonoBehaviour
{
    public List<tagFilter> filters;
    public UnityEvent OnCollided;
    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var filter in filters) {
            if (collision.gameObject.CompareTag(filter.tag))
            {
                OnCollided.Invoke();
                return;
            }
        }
    }
    [System.Serializable]
    public struct tagFilter 
    {
        [Tag]
        public string tag;
    }
}
