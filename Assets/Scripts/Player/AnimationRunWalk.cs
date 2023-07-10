//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AnimationRunWalk : MonoBehaviour
//{
//    [SerializeField] private PlayerMovement _playerMovement;
//    [SerializeField] private Animator _animator;   

//    private float _playerSpeed;

//    //private void Update()
//    //{
//    //    ChangeSpeed();
//    //}
//    private void Awake()
//    {
//        _playerSpeed = _playerMovement.Speed;
//        _animator.SetFloat("Speed", _playerSpeed);
//    }


//    private void OnEnable()
//    {
//        _playerMovement.PlayerAccelerated += ChangeSpeed;
//    }

//    private void OnDisable()
//    {
//        _playerMovement.PlayerAccelerated -= ChangeSpeed;
//    }


//    private void ChangeSpeed()
//    {
//        _animator.SetFloat("Speed", _playerSpeed);
//    }



//}
