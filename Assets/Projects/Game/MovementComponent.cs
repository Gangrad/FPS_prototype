using Common;
using Unity.Utils;
using UnityEngine;

namespace Game {
    public class MovementComponent {
        private const float ExtraAcceleration = 2f;
        private const float JumpSpeed = 10f;
        private readonly Rigidbody _rigidbody;
        private readonly IPlayerParams _playerParams;
        private readonly float _maxSpeedSqr;
        private readonly float _height;

        public MovementComponent(Rigidbody rigidbody, IPlayerParams playerParams, GameConfig config) {
            _rigidbody = rigidbody;
            _playerParams = playerParams;
            _maxSpeedSqr = config.MaxSpeed * config.MaxSpeed;
            var collider = rigidbody.GetComponent<Collider>();
            if (collider != null)
                _height = collider.bounds.extents.y;
        }

        public void Tick(float dt) {
            var input = _rigidbody.transform.rotation * GetInputVector();
            var vel = _rigidbody.velocity;
            var damping = CalcDamping(vel);
            var accelerationValue = _playerParams.Acceleration;
            if (CheckExtraSpeed())
                accelerationValue *= ExtraAcceleration;
            var acceleration = input * accelerationValue + damping;
            var resultVel = vel + acceleration * dt;
            if (resultVel.sqrMagnitude > _maxSpeedSqr)
                resultVel *= _maxSpeedSqr / resultVel.sqrMagnitude;
            if (CheckJump() && IsGrounded())
                resultVel.y = JumpSpeed;
            _rigidbody.velocity = resultVel;
        }

        private Vector3 CalcDamping(Vector3 velocity) {
            if (velocity.IsZero())
                return Vector3.zero;
            velocity.y = 0;
            var dampingValue = _playerParams.Damping;
            var sqrDamping = dampingValue * dampingValue;
            if (velocity.sqrMagnitude <= sqrDamping)
                return -velocity;
            return -velocity.normalized * dampingValue;
        }

        private Vector3 GetInputVector() {
            float x = 0, z = 0;
            CheckMovementInput(KeyCode.W, ref z, 1);
            CheckMovementInput(KeyCode.S, ref z, -1);
            CheckMovementInput(KeyCode.D, ref x, 1);
            CheckMovementInput(KeyCode.A, ref x, -1);
            return new Vector3(x, 0, z);
        }

        private bool IsGrounded() {
            return Physics.Raycast(_rigidbody.position, -Vector3.up, _height + 0.1f);
        }

        private bool CheckJump() {
            return Input.GetKeyDown(KeyCode.Space);
        }

        private bool CheckExtraSpeed() {
            return Input.GetKey(KeyCode.LeftShift);
        }

        private void CheckMovementInput(KeyCode key, ref float value, float change) {
            if (Input.GetKey(key))
                value += change;
        }
    }
}