using UnityEngine.Events;
using UnityEngine;

public class OnTriggerTagAction : MonoBehaviour
{
    public UnityEvent Event;
    public string Tag;
    public TypeOfTrigger Type;

    public Vector3 PosMinus;
    public GameObject Object;
    public Transform Parent;
    public float IntervalReloadSec = 2f;

    private bool Interval = true;

    public enum TypeOfTrigger
    {
        Enter,
        Stay,
        Exit
    }

    private void ResetInterval() { Interval = true; }

    private void OnTriggerEnter(Collider other)
    {
        if (Type == TypeOfTrigger.Enter && Interval)
        {
            if (other.gameObject.tag == Tag)
            {
                Invoke("ResetInterval", IntervalReloadSec);
                Interval = false;
                Event.Invoke();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Type == TypeOfTrigger.Stay && Interval)
        {
            if (other.gameObject.tag == Tag)
            {
                Invoke("ResetInterval", IntervalReloadSec);
                Interval = false;
                Event.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Type == TypeOfTrigger.Exit && Interval)
        {
            if (other.gameObject.tag == Tag)
            {
                Invoke("ResetInterval", IntervalReloadSec);
                Interval = false;
                Event.Invoke();
            }
        }
    }

    public void Dublicate()
    {
        var a = Instantiate(Object, new Vector3(0f, 0f, 0f), Quaternion.identity);
        if (Parent != null) a.transform.SetParent(Parent.transform);
        a.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        a.transform.localScale = new Vector3(1f, 1f, 1f);
        a.transform.localPosition = transform.localPosition - PosMinus;
    }

    public void SpanFlor()
    {
        Controller.instance.SpawnFloor();
    }

    public void DestroyObject(float sec)
    {
        Destroy(Object, sec);
    }
}
