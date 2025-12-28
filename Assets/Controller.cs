using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private Transform _thisTR;
    public float _SpeedDown;
    public float _SpeedRot;
    public bool _is2_0X;
    public bool _is1_25X;
    public bool _isFRONT;
    public bool _isBACK;
    float curveValue = 1;
    bool s = false;

    public GameObject[] _easyFloors;
    public GameObject[] _normalFloors;
    public GameObject[] _hardFloors;
    public GameObject[] _infinityFloors;

    public int _countOfSp = 0;

    public Transform _chunkPlayer;

    public AnimationCurve curve;
    //public float animationDuration = 2f;

    private float elapsedTime = 2f;

    void Start()
    {
        instance = this;
        _thisTR = transform;
        enabled = false;
    }

    public void Stun()
    {
        elapsedTime = 0f;
        s = true;
    }

    void FixedUpdate()
    {
        if (elapsedTime < 15f) elapsedTime += Time.deltaTime;
        //float animationProgress = Mathf.Clamp01(elapsedTime / animationDuration);
       
        if (s) curveValue = curve.Evaluate(elapsedTime);

        if (_isBACK)
        {
            _thisTR.Rotate(new Vector3(0f, 0f, _SpeedRot * Time.deltaTime * 5f));
            _thisTR.position = Vector3.MoveTowards(_thisTR.position, _thisTR.position - new Vector3(0f, 1000f, 0f), -_SpeedDown * Time.deltaTime * 5f);
            Interface.instance._addPointCount = 3;
            Player.player.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (_isFRONT)
        {
            _thisTR.Rotate(new Vector3(0f, 0f, -_SpeedRot * Time.deltaTime * 5f));
            _thisTR.position = Vector3.MoveTowards(_thisTR.position, _thisTR.position - new Vector3(0f, 1000f, 0f), _SpeedDown * Time.deltaTime * 5f);
            Interface.instance._addPointCount = 3;
            Player.player.GetComponent<Rigidbody>().useGravity = false;
        }
        else if (_is2_0X)
        {
            _thisTR.Rotate(new Vector3(0f, 0f, -_SpeedRot * Time.deltaTime * 1.8f * curveValue));
            _thisTR.position = Vector3.MoveTowards(_thisTR.position, _thisTR.position - new Vector3(0f, 1000f, 0f), _SpeedDown * Time.deltaTime * 1.8f * curveValue);
            Interface.instance._addPointCount = 3;
        }
        else if (_is1_25X)
        {
            _thisTR.Rotate(new Vector3(0f, 0f, -_SpeedRot * Time.deltaTime * 1.35f * curveValue));
            _thisTR.position = Vector3.MoveTowards(_thisTR.position, _thisTR.position - new Vector3(0f, 1000f, 0f), _SpeedDown * Time.deltaTime * 1.35f * curveValue);
            Interface.instance._addPointCount = 2;
        }
        else
        {
            _thisTR.Rotate(new Vector3(0f, 0f, -_SpeedRot * Time.deltaTime * curveValue));
            _thisTR.position = Vector3.MoveTowards(_thisTR.position, _thisTR.position - new Vector3(0f, 1000f, 0f), _SpeedDown * Time.deltaTime * curveValue);
            Interface.instance._addPointCount = 1;
        }

        _SpeedDown += Time.deltaTime / 150f;
        _SpeedRot += Time.deltaTime / 50f;
    }

    public void Do_FB(bool isfront)
    {
        CancelInvoke("DoFlase");
        if (isfront)
        {
            _isFRONT = true;
            _isBACK = false;
        }
        else
        {
            _isFRONT = false;
            _isBACK = true;
        }
        Player.player.GetComponent<Rigidbody>().useGravity = false;

        if (!isfront) Player.player.GetComponent<Rigidbody>().AddForce(0f, 2.75f, 0f);

        Invoke("DoFlase", 0.1f);
    }

    private void DoFlase()
    {
        _isFRONT = false;
        _isBACK = false;
        Player.player.GetComponent<Rigidbody>().useGravity = true;
    }

    private int oldSpawned = -1;
    public void SpawnFloor()
    {
        int r = Random.Range(0, 101);

        if (Interface.instance._points < 15)
        {
            var a = Instantiate(_easyFloors[0], _chunkPlayer);
            a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
        }
        else if (Interface.instance._points < 650f)
        {
            int rand = Random.Range(0, _easyFloors.Length);
            if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);
            if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);

            var a = Instantiate(_easyFloors[rand], _chunkPlayer);
            a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
            oldSpawned = rand;
        }
        else if (Interface.instance._points < 1100f)
        {
            if (r < 30)
            {
                int rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);

                var a = Instantiate(_easyFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else
            {
                int rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);

                var a = Instantiate(_normalFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
        }
        else if (Interface.instance._points < 1600f)
        {
            if (r < 15)
            {
                int rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);

                var a = Instantiate(_easyFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else if (r < 35)
            {
                int rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);

                var a = Instantiate(_normalFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else
            {
                int rand = Random.Range(0, _hardFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _hardFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _hardFloors.Length);

                var a = Instantiate(_hardFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
        }
        else
        {
            if (r < 4f)
            {
                int rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _easyFloors.Length);

                var a = Instantiate(_easyFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else if (r < 10)
            {
                int rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _normalFloors.Length);

                var a = Instantiate(_normalFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else if (r < 50)
            {
                int rand = Random.Range(0, _hardFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _hardFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _hardFloors.Length);

                var a = Instantiate(_hardFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
            else
            {
                int rand = Random.Range(0, _infinityFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _infinityFloors.Length);
                if (oldSpawned == rand) rand = Random.Range(0, _infinityFloors.Length);

                var a = Instantiate(_infinityFloors[rand], _chunkPlayer);
                a.transform.localPosition = new Vector3(0f, 0f, 0.75f - 0.3f * _countOfSp);
                oldSpawned = rand;
            }
        }

        _countOfSp++;
    }
}
