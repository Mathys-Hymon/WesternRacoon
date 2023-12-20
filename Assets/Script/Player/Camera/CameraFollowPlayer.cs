using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    [Header("Offset")]
    [SerializeField] private float yOffset;
    [SerializeField] private float xOffset;
    [SerializeField] PlayerMovement _player;

    private Coroutine _turnCoroutine;

    private bool _isFacingRight;
    private float x, y, z;

    private void Start()
    {
        _isFacingRight = _player.isFacingRight;
        x = transform.position.x + xOffset;
        y = transform.position.y + yOffset;
        z = transform.position.z;
    }

    private void Update()
    {
        x = _player.transform.position.x + xOffset;

        transform.position = new Vector3(x, y, z);
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight;

        if(_isFacingRight )
        {
            return 0f;
        }
        else
        {
            return 180f;
        }
    }
}
