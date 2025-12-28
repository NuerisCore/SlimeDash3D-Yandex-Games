using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    private Transform _thisTR;
    private Rigidbody _thisRB;
    private Animator _thisANI;

    public int _JumpForce;
    public int _DownForce;
    private bool _CanJump;
    public bool _CanJump2;
    private bool _CanDown;
    public bool _isStoped;
    public bool _isShield;

    public ParticleSystem _JumpDownParticle;
    public ParticleSystem _DieParticle;
    public GameObject _Solvers;
    public GameObject _Body;
    public GameObject _Body2;
    public ParticleSystem _Boost;

    public FollowCamera1 cameraFollow;

    public AudioSource s_Jump;
    public AudioSource s_JumpDown;
    public AudioSource s_Down;
    public AudioSource s_Die;
    public AudioSource s_Prujina;
    public AudioSource s_SpeedUp;
    public AudioSource s_Respawn;

    public GameObject _shieldObject;
    public GameObject Lose;

    private void OnEnable()
    {
        player = this;
        _thisANI = GetComponent<Animator>();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        player = this;
        _thisTR = transform;
        _thisRB = GetComponent<Rigidbody>();
        _thisANI = GetComponent<Animator>();
        cameraFollow.vector3.y = 0.87f;
        cameraFollow.dsd = 0.31f;
    }

    public Color _baseBoostColor;
    public Color _unieBoostColor;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) Agregate(true);
        if (Input.GetKeyDown(KeyCode.DownArrow)) Agregate(false);

        if (_CanJump2) _Boost.startColor = _unieBoostColor;
        else _Boost.startColor = _baseBoostColor;

        if (_isShield) _shieldObject.SetActive(true);
        else _shieldObject.SetActive(false);
    }

    public void Agregate(bool isUP)
    {
        Interface.instance.Pouse(true);

        if (isUP && _CanJump && !_isStoped)
        {
            s_Jump.Play();
            Controller.instance._is1_25X = false;
            _Boost.emissionRate = 6;
            _thisRB.AddForce(0f, _JumpForce, 0f);
            _thisANI.SetBool("isJumping", true);
            _thisANI.SetBool("isGoing", false);
            _thisANI.SetBool("isRunning", false);
            _thisANI.SetBool("isDowning", false);
            CancelInvoke("UnDown");

            _CanJump = false;
        }
        else if (isUP && _CanJump2 && !_isStoped)
        {
            s_Jump.Play();
            Controller.instance._is1_25X = false;
            _Boost.emissionRate = 6;
            _thisRB.AddForce(0f, _JumpForce * 1.3f, 0f);
            _thisANI.SetBool("isJumping", true);
            _thisANI.SetBool("isGoing", false);
            _thisANI.SetBool("isRunning", false);
            _thisANI.SetBool("isDowning", false);
            CancelInvoke("UnDown");

            _CanJump2 = false;
        }
        if (!isUP && _CanDown && !_isStoped)
        {
            Interface.instance.AddPoint(10);
            s_Down.Play();
            Controller.instance._is1_25X = true;
            _Boost.emissionRate = 12;
            _thisRB.AddForce(0f, -_JumpForce, 0f);
            _thisANI.SetBool("isDowning", true);
            _thisANI.SetBool("isGoing", false);
            _thisANI.SetBool("isRunning", false);
            _thisANI.SetBool("isJumping", false);
            CancelInvoke("UnDown");
            Invoke("UnDown", 1f);
        }
        else if (!isUP && !_isStoped)
        {
            s_JumpDown.Play();
            Controller.instance._is1_25X = true;
            _Boost.emissionRate = 12;
            _thisANI.SetBool("isDowning", true);
            _thisANI.SetBool("isJumping", false);
            _thisANI.SetBool("isGoing", false);
            _thisANI.SetBool("isRunning", false);
            CancelInvoke("UnDown");
            Invoke("UnDown", 1f);
        }
    }

    private void FixedUpdate()
    {
        if (mover_active) _thisTR.Translate(0f, 11.25f * Time.deltaTime, 0f);
        if (mover_active2) _thisTR.Translate(0f, -11.25f * Time.deltaTime, 0f);
    }

    public void Do_UD(bool isUp)
    {
        _CanJump = false;
        _CanDown = false;
        GetComponent<Rigidbody>().useGravity = false;

        if (isUp)
        {
            CancelInvoke("Un_UD");
            Invoke("Un_UD", 0.115f);
            mover_active = true;
        }
        else
        {
            CancelInvoke("Un_UD2");
            Invoke("Un_UD2", 0.115f);
            mover_active2 = true;
        }
    }

    private bool mover_active = false;
    private bool mover_active2 = false;

    private void Un_UD()
    {
        mover_active = false;
        GetComponent<Rigidbody>().useGravity = true;
        _CanDown = true;
    }

    private void Un_UD2()
    {
        mover_active2 = false;
        GetComponent<Rigidbody>().useGravity = true;
        _CanDown = true;
    }

    private void UnDown()
    {
        Controller.instance._is1_25X = false;
        _Boost.emissionRate = 6;
        _thisANI.SetBool("isDowning", false);
        if (Controller.instance._SpeedRot < 15) _thisANI.SetBool("isGoing", true);
        else if (Controller.instance._SpeedRot >= 15) _thisANI.SetBool("isRunning", true);
    }

    private void f()
    {
        Interface.instance.Die();
    }

    private void f2()
    {
        YG.YandexGame.FullscreenShow();
    }

    private void Stop()
    {
        Lose.SetActive(true);
        YG.YandexGame.SaveCloud();
        Invoke("f", 2.25f);
        Invoke("f2", 1f);
        Interface.instance._canAddPoint = false;

        _Body.SetActive(false);
        _Body2.SetActive(false);
        GetComponent<Animator>().enabled = false;
        _thisRB.isKinematic = true;
        GetComponent<SphereCollider>().enabled = false;
        _Solvers.transform.position = transform.position;
        _Solvers.SetActive(true);
    }

    private void Stop1()
    {
        s_Die.Play();
        CancelInvoke("UnLock");
        CancelInvoke("Lock");
        Invoke("UnLock", 0f);
        _isStoped = true;
        Invoke("Stop2", 0.5f);
        Invoke("Stop", 0.1f);
        Controller.instance.Stun();


        _thisANI.SetBool("isStoping", true);
        _thisANI.SetBool("isDowning", false);
        _thisANI.SetBool("isJumping", false);
        _thisANI.SetBool("isGoing", false);
        _thisANI.SetBool("isRunning", false);
    }

    private void Stop2()
    {
        _thisANI.SetBool("isStoping", false);
        _thisANI.SetBool("isDowning", false);
        _thisANI.SetBool("isJumping", false);
        _thisANI.SetBool("isGoing", true);
        _thisANI.SetBool("isRunning", false);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col != null && col.gameObject.tag != "Floor")
        {
            CancelInvoke("UnLock");
            CancelInvoke("Lock");
            Invoke("UnLock", 0f);
            s_JumpDown.Play();
            _thisANI.SetBool("isJumping", false);

            _CanJump = true;
            _CanDown = false;

            if (Controller.instance._SpeedRot < 15) _thisANI.SetBool("isGoing", true);
            else if (Controller.instance._SpeedRot >= 15) _thisANI.SetBool("isRunning", true);

            if (col.gameObject.tag == "Enemy" && !_isShield)
            {
                cameraShake.rotationMagnitude = 15f;
                cameraShake.shakeMagnitude = 0.15f;
                cameraShake.shakeDuration = 1.25f;
                cameraShake.dampingSpeed = 0.15f;

                Stop1();

                if (YG.YandexGame.savesData.record < Interface.instance._points)
                {
                    YG.YandexGame.savesData.record = Interface.instance._points;
                    YG.YandexGame.NewLeaderboardScores("TopPlayers", YG.YandexGame.savesData.record);
                    YG.YandexGame.SaveCloud();
                }

                GameObject ds = Instantiate(_DieParticle.gameObject, _DieParticle.transform.position, Quaternion.identity);//contacts[0].point, Quaternion.identity);
                ds.SetActive(true);
                ds.transform.SetParent(Controller.instance.gameObject.transform);
                cameraFollow.vector3.y = 1.5f;
                cameraFollow.dsd = 0.11f;
                Destroy(ds, 1.5f);
            }
            else if (col.gameObject.tag == "Enemy" && _isShield)
            {
                s_Respawn.Play();
                if (col.transform.parent.tag == "Floor") Destroy(col.gameObject);
                else Destroy(col.transform.parent.gameObject);

                Controller.instance.Do_FB(false);
                _isShield = false;

                GameObject ds = Instantiate(_DieParticle.gameObject, col.transform.position, Quaternion.identity);//contacts[0].point, Quaternion.identity);
                ds.SetActive(true);
                ds.transform.SetParent(Controller.instance.gameObject.transform);
                Destroy(ds, 1.5f);

                GameObject ds1 = Instantiate(_DieParticle.gameObject, col.transform.position, Quaternion.identity);//contacts[0].point, Quaternion.identity);
                ds1.SetActive(true);
                ds1.transform.SetParent(Controller.instance.gameObject.transform);
                Destroy(ds1, 1.5f);

                GameObject ds2 = Instantiate(_DieParticle.gameObject, col.transform.position, Quaternion.identity);//contacts[0].point, Quaternion.identity);
                ds2.SetActive(true);
                ds2.transform.SetParent(Controller.instance.gameObject.transform);
                Destroy(ds2, 1.5f);
            }

            ///
            
            if (col.gameObject.tag == "Jump")
            {
                s_Prujina.Play();
                _thisRB.AddForce(0f, _JumpForce * 2.25f, 0f);
                col.gameObject.GetComponent<Animator>().SetBool("a", true);
            }

            //ContactPoint[] contacts = col.contacts;

            GameObject d = Instantiate(_JumpDownParticle.gameObject, _JumpDownParticle.transform.position, Quaternion.identity);//contacts[0].point, Quaternion.identity);
            cameraShake.Shake();
            d.SetActive(true);
            d.transform.SetParent(Controller.instance.gameObject.transform);
            Destroy(d, 1.5f);
        }
    }

    public CameraShake cameraShake;

    private void OnCollisionExit(Collision col)
    {
        if (col != null)
        {
            _CanDown = true;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "X1")
        {
            Controller.instance._is2_0X = false;
            s_SpeedUp.pitch = -1;
            s_SpeedUp.Play();
            CancelInvoke("DoNormalSpeed");
        }
        else if (col.tag == "X2")
        {
            s_SpeedUp.pitch = 1;
            s_SpeedUp.Play();
            Controller.instance._is2_0X = true;
            _Boost.emissionRate = 20;
            CancelInvoke("DoNormalSpeed");
            Invoke("DoNormalSpeed", 3.75f);
        }

        if (col.tag == "Coin" && CGG)
        {
            CGG = false;
            Invoke("CanGetCoin", 0.3f);

            col.GetComponent<Animator>().enabled = true;
            Destroy(col.gameObject, 4f);

            YG.YandexGame.savesData.money++;
        }
    }

    bool CGG = true;
    private void CanGetCoin()
    {
        CGG = true;
    }

    private void DoNormalSpeed()
    {
        s_SpeedUp.pitch = -1;
        s_SpeedUp.Play();
        Controller.instance._is2_0X = false;
        _Boost.emissionRate = 6;
    }
}
