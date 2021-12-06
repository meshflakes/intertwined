using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactable.ConstructionSite
{
    public class Elevator : MonoBehaviour
    {
        [NonSerialized] public bool Powered = false;
        
        private bool Moving => _targetFloor != _currentFloor;
        private readonly List<Transform> _floors = new List<Transform>();
        private int _currentFloor = 0;
        private int _targetFloor = 0;

        private int _maxFloor;
        private int _minFloor;

        private readonly float elevatorSpeed = 2.7f;

        protected void Start()
        {
            var floors = transform.parent.Find("Floors");
            var numFloors = floors.childCount;

            for (var i = 0; i < numFloors; i++)
            {
                _floors.Add(floors.GetChild(i));
            }

            _minFloor = 0;
            _maxFloor = numFloors - 1;
        }

        protected void Update()
        {
            if (Moving)
            {
                TransitionToTargetFloor();
            }
        }

        private void TransitionToTargetFloor()
        {            
            var distToTarget = transform.position.y - _floors[_targetFloor].position.y;
            var direction = distToTarget > 0 ? -1 : 1;
            transform.position += Vector3.up * direction * Time.deltaTime * elevatorSpeed;
            
            if (distToTarget * distToTarget < 0.01f)
            {
                transform.position = _floors[_targetFloor].position;
                _currentFloor = _targetFloor;
            }
        }

        public bool TryChangeFloor(int deltaLevel)
        {
            // TODO: if not powered start an electricity prompt
            if (Moving || !Powered) return false;
            
            var newTargetFloor = _currentFloor + deltaLevel;
            
            Debug.Log($"current: {_currentFloor}, delta: {deltaLevel}, new: {newTargetFloor}");
            if (_minFloor > newTargetFloor || newTargetFloor > _maxFloor) return false;

            _targetFloor = newTargetFloor;
            return true;
        }
    }
}