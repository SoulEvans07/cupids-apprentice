using System.Numerics;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Fog : MonoBehaviour {
    private Transform _transform;
    private Vector2 _pos;
    private Camera _camera;
    public Rect fog;
    public float _diff;
    public float minDiff = 1.5f;
    public float maxDiff = 0.5f;
    public float minScale = 1.0f;
    public float maxScale = 4.0f;
    private Vector2 aspect;
    public float jitter = 0.1f;
    public float jitterTimer;
    public float jitterSpeed = 3;

    void Start() {
        _camera = Camera.main;
        _transform = GetComponent<Transform>();
        BoxCollider2D coll = GetComponentInParent<BoxCollider2D>();
        this.fog = new Rect(_transform.position, coll.size);
        
//        this.aspect = new Vector2(1, this.fog.size.x / this.fog.size.y);
        this.aspect = new Vector2( this.fog.size.y / this.fog.size.x * 0.4f, 0.5f);
    }

    void FixedUpdate() {
        _pos = _transform.position;

        // Set fog position
        this.fog.position = _pos - this.fog.size * 0.5f;

        // Update jitter
        jitterTimer += Time.deltaTime * jitterSpeed;
        if (jitterTimer > Mathf.PI * 2) jitterTimer = 0;
        
        // Get distance
        Vector2 mPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        _diff = DistancePointToRectangle(mPos, this.fog);
        
        // Animate fog
        if (_diff < minDiff) {
            _transform.localScale = this.aspect * getScale(_diff) + this.aspect * getJitter() ;
        } else if (!_transform.localScale.Equals(Vector3.zero)) {
            _transform.localScale = Vector3.zero;
        }
    }

    float getScale(float diff) {
        return minScale + (maxScale - minScale) * ((minDiff - diff) / (minDiff - maxDiff));
    }

    float getJitter() {
        return jitter * Mathf.Sin(jitterTimer);
    }

    float DistancePointToRectangle(Vector2 point, Rect rect) {
        if (point.x < rect.xMin) {
            if (point.y < rect.yMin) {
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMin);
                return diff.magnitude;
            }

            if (point.y > rect.yMax) {
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMax);
                return diff.magnitude;
            }

            return rect.xMin - point.x;
        }

        if (point.x > rect.xMax) {
            if (point.y < rect.yMin) {
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMin);
                return diff.magnitude;
            }

            if (point.y > rect.yMax) {
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMax);
                return diff.magnitude;
            }

            return point.x - rect.xMax;
        }

        if (point.y < rect.yMin) {
            return rect.yMin - point.y;
        }

        if (point.y > rect.yMax) {
            return point.y - rect.yMax;
        }

        return 0f;
    }

    [ExecuteInEditMode]
    void OnDrawGizmosSelected() {
        Handles.DrawSolidRectangleWithOutline(this.fog, Color.clear, Color.cyan);
    }
}