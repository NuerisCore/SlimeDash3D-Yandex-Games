using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHelper : MonoBehaviour
{
    public bool _isStatic;
    public TypeOfMover _type;

    public ParticleSystem[] _parts;
    public Color[] _partColors1;
    public Color[] _partColors2;
    public Vector2 _yZ;

    public GameObject ob1;
    public GameObject ob2;

    private void Start()
    {
        if (_type == TypeOfMover.Up || _type == TypeOfMover.Right)
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].startColor = _partColors1[i];
            }
        }
        else
        {
            for (int i = 0; i < _parts.Length; i++)
            {
                _parts[i].startColor = _partColors2[i];
            }
        }

        var mainModule = _parts[0].main;
        var mainModule2 = mainModule.startRotationX;
        var mainModule3 = mainModule.startRotationY;
        var mainModule4 = mainModule.startRotationZ;
        mainModule3 = _yZ.x * Mathf.Deg2Rad;
        mainModule4 = _yZ.y * Mathf.Deg2Rad;
        if (_type == TypeOfMover.Up) mainModule2.constant = -90 * Mathf.Deg2Rad;
        else if (_type == TypeOfMover.Down) mainModule2.constant = 90f * Mathf.Deg2Rad;
        else if (_type == TypeOfMover.Right) mainModule2.constant = 0f * Mathf.Deg2Rad;
        else if (_type == TypeOfMover.Left) mainModule2.constant = 180f * Mathf.Deg2Rad;
        mainModule.startRotationX = mainModule2;
        mainModule.startRotationY = mainModule3;
        mainModule.startRotationZ = mainModule4;
    }

    public enum TypeOfMover
    {
        Up,
        Down,
        Right,
        Left
    }

    public void FP()
    {
        if (_type == TypeOfMover.Right) Controller.instance.Do_FB(true);
        else if (_type == TypeOfMover.Left) Controller.instance.Do_FB(false);
        else if (_type == TypeOfMover.Up) Player.player.Do_UD(true);
        else if (_type == TypeOfMover.Down) Player.player.Do_UD(false);

        for (int i = 0; i < _parts.Length; i++)
        {
            _parts[i].enableEmission = false;
        }

        if (_type == TypeOfMover.Down || _type == TypeOfMover.Left) ob2.SetActive(true);
        else ob1.SetActive(true);

        if (!_isStatic) Destroy(gameObject, 0.5f);
    }
}
