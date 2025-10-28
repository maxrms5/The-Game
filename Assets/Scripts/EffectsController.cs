using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.Play("fireballEffect");
    }
    void Start()
    {
        Destroy(gameObject, anim.GetCurrentAnimatorStateInfo(0).length);
    }
}
