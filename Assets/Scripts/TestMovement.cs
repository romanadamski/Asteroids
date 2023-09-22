using UnityEngine;

namespace Assets.Scripts
{
    public class TestMovement : MonoBehaviour
    {
        [SerializeField]
        private float speed = 50;
        [SerializeField]
        private Vector3 target;

        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _axis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            //_rigidbody2D.AddForce(_axis * Time.deltaTime * _speed);
            //_rigidbody2D.velocity = _axis * Time.deltaTime * speed;

            var direction = (transform.position - target).normalized;
            if ((transform.position - target).magnitude > 0.5f)
            {
                transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
            }
            ManageScreenEdges();
        }

        private void ManageScreenEdges()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            if (!GeometryUtility.TestPlanesAABB(planes, _spriteRenderer.bounds))
            {
                ScreenManager.Instance.HandleScreenEdgeCrossing(transform);
            }
        }
    }
}
