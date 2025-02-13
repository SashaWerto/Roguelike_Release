using UnityEngine;
public class RotateToMousePos : MonoBehaviour
{
    private float _rotateZ;
    public void Rotate()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rotateZ);
        Vector3 _localScale = Vector3.one;
        if (_rotateZ > 90 || _rotateZ < -90)
        {
            _localScale.y = -1f;
        }
        else
        {
            _localScale.y = +1f;
        }
        transform.localScale = _localScale;
    }
}
