using System;
using UnityEngine;

namespace Tarodev {
    
    public class Missile : MonoBehaviour {
        [Header("REFERENCES")] 
        [SerializeField] private Rigidbody _rb;
        public Vector3 _target;
        public Rigidbody targetRigidBody;
        public bool enemyNotFound = false;    

        [SerializeField] private GameObject _explosionPrefab;

        [Header("MOVEMENT")] 
        [SerializeField] private float _speed = 15;
        [SerializeField] private float _rotateSpeed = 95;

        [Header("PREDICTION")] 
        [SerializeField] private float _maxDistancePredict = 100;
        [SerializeField] private float _minDistancePredict = 5;
        [SerializeField] private float _maxTimePrediction = 5;
        private Vector3 _standardPrediction, _deviatedPrediction;

        [Header("DEVIATION")] 
        [SerializeField] private float _deviationAmount = 50;
        [SerializeField] private float _deviationSpeed = 2;

        private void FixedUpdate() {
            _rb.velocity = transform.forward * _speed;
            
            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target));
            

            PredictMovement(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }

        private void PredictMovement(float leadTimePercentage) {
            var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

            if (enemyNotFound)
            {
                _standardPrediction = _target;
            }
            else
            {
                if(targetRigidBody != null)
                    _standardPrediction = targetRigidBody.position + targetRigidBody.velocity * predictionTime;
                else
                    _standardPrediction = _target;
            }
        }

        private void AddDeviation(float leadTimePercentage) {
            var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);
            
            var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

            _deviatedPrediction = _standardPrediction + predictionOffset;
        }

        private void RotateRocket() {
            var heading = _deviatedPrediction - transform.position;

            var rotation = Quaternion.LookRotation(heading);
            _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision) {
            if(_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            //if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();
            if(collision.transform.tag == "Enemy" || collision.transform.tag == "Malang")
            {
                collision.transform.GetComponent<DamageManager>().Take_Damage(10,this.gameObject);
            }
   
            Destroy(gameObject);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, _standardPrediction);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
        }
    }
}