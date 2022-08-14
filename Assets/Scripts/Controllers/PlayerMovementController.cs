using Data.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables
        #region Serialized Variables

        //[SerializeField] private PlayerManager manager;
        [SerializeField] private Rigidbody rigidbody;
        #endregion
        #region Private Variables
        [Header("Data")] private PlayerMovementData _movementData;
        private bool _isReadyToMove, _isReadyToPlay, _isOnDronePool = false;
        private float _inputValue;
        private float _inputValueZ;
        private Vector2 _clampValues;
        private InputStates _currentInputState = InputStates.OldInputSystem;
        #endregion
        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {   
            _movementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }
        public void DeactiveForwardMovement(Transform poolTriggerTransform)
        {
            _isOnDronePool = true;
        }

        public void UnDeactiveForwardMovement()
        {
            _isOnDronePool = false;
        }

        public void UpdateRunnerInputValue(RunnerInputParams inputParam)
        {
            _inputValue = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }

        public void UpdateIdleInputValue(IdleInputParams inputParams)
        {
            _inputValue = inputParams.ValueX;
            _inputValueZ = inputParams.ValueZ;
        }

        public void GetMovementState()
        {
            _currentInputState = InputStates.NewInputSystem;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        // private void Update() //Degisebilir
        // {
        //     if (_isReadyToPlay)
        //     {
        //         manager.SetStackPosition();
        //
        //     }
        // }
    
        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                if (_isOnDronePool)
                {
                    OnlySideways();
                }
                else if (_isReadyToMove)
                {
                    if (_currentInputState == InputStates.OldInputSystem)
                    {
                        Debug.Log("Runner");
                        RunnerMove();
                    }else if (_currentInputState == InputStates.NewInputSystem)
                    {
                        IdleMove();
                        Debug.Log("IDLE  MOVING");
                    }
                }
                else
                {
                    if (_currentInputState == InputStates.OldInputSystem)
                    {
                        Debug.Log("Runner 2");
                        StopSideways();
                    }else if (_currentInputState == InputStates.NewInputSystem)
                    {
                        Debug.Log("IDLE 2");
                        Stop();
                    }
                }
            }
            else
                Stop();
        }

        private void RunnerMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed, velocity.y,
                _movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }

        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed, velocity.y,
                _inputValueZ*_movementData.ForwardSpeed);
            rigidbody.velocity = velocity;

            var position1 = rigidbody.position;
            var position = new Vector3(position1.x, position1.y, position1.z);
            position1 = position;
            rigidbody.position = position1;
        }

        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.ForwardSpeed);
            //rigidbody.angularVelocity = Vector3.zero;
        }

        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
        private void OnlySideways()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValue * _movementData.SidewaysSpeed, velocity.y,
                0);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                    _clampValues.y),
                (position = rigidbody.position).y,
                position.z);
            rigidbody.position = position;
        }
        
        public void Jump(float distance,float duration)
        {
            rigidbody.DOMoveY(distance, duration);
        }
        
        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}