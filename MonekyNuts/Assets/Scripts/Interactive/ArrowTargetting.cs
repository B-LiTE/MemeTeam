using UnityEngine;
using System.Collections;

public class ArrowTargetting : MonoBehaviour {

    Rigidbody rigidbody;
    SphereCollider detectionZone;

    [SerializeField]
    float arrowSpeed;

    PlayerStats playerStats;

    void Awake()
    {
        detectionZone = GetComponent<SphereCollider>();
        rigidbody = GetComponent<Rigidbody>();
        playerStats = References.player.GetComponent<PlayerStats>();
        References.stateManager.changeState += onChangeState;
    }

    void Start()
    {
        setArrowVelocity();
    }

    void onChangeState()
    {
        if (References.stateManager.CurrentState == StateManager.states.strategy) StartCoroutine(pause());
    }

    IEnumerator pause()
    {
        rigidbody.useGravity = false;

        while (References.stateManager.CurrentState == StateManager.states.strategy)
        {
            rigidbody.velocity = Vector3.zero;

            yield return null;
        }

        setArrowVelocity();
        rigidbody.useGravity = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            transform.LookAt(other.gameObject.transform);
            setArrowVelocity();
            detectionZone.enabled = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Enemy")
            other.collider.GetComponent<EnemyStats>().Damage(playerStats.activeDamage, References.player);

        killArrow();
    }

    void setArrowVelocity()
    {
        rigidbody.velocity = transform.forward.normalized * (arrowSpeed + playerStats.attackRange);
    }

    void killArrow()
    {
        References.stateManager.changeState -= onChangeState;
        Destroy(this.gameObject);
    }

}
