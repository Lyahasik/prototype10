using UnityEngine;

public class RotationStartSwitch : RotationStart
{
    [SerializeField] private int _countStop = 2;
    [SerializeField] private float _delayStop = 0.4f;
    private float _intervalStop;

    private float _delay = 0.0f;
    private int _currentCountStop = 0;
    private float _checkPoint = 0.0f;

    protected override void Rotation()
    {
        if (_isActive
            && _delay < Time.time)
        {
            _delta = 1 - (_timeNext - Time.time) / _duration;

            _currentSpeed = Mathf.Lerp(0, _finalSpeed, _delta);

            if (_currentSpeed >= _finalSpeed)
            {
                transform.Rotate(Vector3.forward * _finalSpeed * Time.deltaTime);

                _isActive = false;
                _delay = 0.0f;
                _checkPoint = 0.0f;
                
                _iDiskRotation.StartRotation();
                
                return;
            }

            transform.Rotate(Vector3.forward * _currentSpeed * Time.deltaTime);
            
            if (_checkPoint < Time.time)
            {
                _checkPoint = Time.time + _delayStop + _intervalStop;

                if (_currentCountStop == 0)
                {
                    _intervalStop = (_duration - _delayStop * _countStop) / _countStop;
                }
                else
                {
                    _delay = Time.time + _delayStop;
                }

                _currentCountStop++;
            }
        }
    }
}
