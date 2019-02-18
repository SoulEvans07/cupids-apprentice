using UnityEngine;

public class AnimationRandomStart : MonoBehaviour {
    private Animator _animator;
    private static readonly int Offset = Animator.StringToHash("offset");

    public float maxOffset = 1f;

    private void Start() {
        _animator = GetComponent<Animator>();
        maxOffset = _animator.GetCurrentAnimatorStateInfo(0).length;
        _animator.SetFloat(Offset, Random.Range(0, maxOffset));
    }
}