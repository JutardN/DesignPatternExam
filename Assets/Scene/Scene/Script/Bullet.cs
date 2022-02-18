using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] float _collisionCooldown = 0.5f;

    BulletPool _bulletPool;
    //[SerializeField] ParticleSystem _fxBullet;
    [SerializeField] GameObject _fxBullet;
    [SerializeField] UnityEvent _onDeath;
    public event UnityAction OnDeath { add => _onDeath.AddListener(value); remove => _onDeath.RemoveListener(value); }

    public Vector3 Direction { get; private set; }
    public int Power { get; private set; }
    float LaunchTime { get; set; }

    internal Bullet Init(Vector3 vector3, int power)
    {
        Direction = vector3;
        Power = power;
        LaunchTime = Time.fixedTime;
        return this;
    }

    private void Start()
    {
        OnDeath += LaunchEffects;
    }
    private void OnDestroy()
    {
        OnDeath -= LaunchEffects;
    }

    void FixedUpdate()
    {
        _rb.MovePosition((transform.position + (Direction.normalized * _speed)));
    }
    
    void LateUpdate()
    {
        transform.rotation = EntityRotation.AimPositionToZRotation(transform.position, transform.position + Direction);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Time.fixedTime < LaunchTime + _collisionCooldown) return;

        collision.GetComponent<IHealth>()?.TakeDamage(Power);
        // on rajoute les toggle/box au collisions de la bullet
        collision.GetComponent<ITouchable>()?.Touch(Power);
        
        if (collision.transform.parent && collision.transform.parent.TryGetComponent<Item>(out Item obj))
        {
            //Destroy(gameObject);
            //on détruit la balle au contact de la porte mais pas de la potion ni des clès
            if (obj.Type == IObject.TYPE.DOOR)
            {
                _bulletPool.EndBullet(this);
                _onDeath?.Invoke();
            }
        }
        else
        {
            // si ce n'est pas un objet alors c'est un mur et on détruit la balle
            if (!collision.transform.TryGetComponent<TakeObject>(out TakeObject notPlayer))
            {
                _bulletPool.EndBullet(this);
                _onDeath?.Invoke();
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Time.fixedTime < LaunchTime + _collisionCooldown) return;

        collision.collider.GetComponent<IHealth>()?.TakeDamage(Power);
        // on rajoute les toggle/box au collisions de la bullet
        collision.collider.GetComponent<ITouchable>()?.Touch(Power);
        if (collision.collider.transform.parent && collision.collider.transform.parent.TryGetComponent<Item>(out Item obj))
        {
            //Destroy(gameObject);
            //on détruit la balle au contact de la porte mais pas de la potion ni des clès

            if (obj.Type == IObject.TYPE.DOOR)
            {
                _bulletPool.EndBullet(this);
                _onDeath?.Invoke();
            }
        }
        else
        {
            // on ne change pas la balle au contact du trigger de prise d'objet du joueur
            if (!collision.collider.transform.TryGetComponent<TakeObject>(out TakeObject notPlayer))
            {
                _bulletPool.EndBullet(this);
                _onDeath?.Invoke();
            }
        }
    }

    private void Health_OnDamage(int arg0)
    {
        throw new NotImplementedException();
    }

    // on Set la pool pour les balles
    public void SetBulletPool(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    // On lance less effets de SFX/FX lors de l'event onDeath
    public void LaunchEffects()
    {
        GameObject ps = Instantiate(_fxBullet, transform.position, Quaternion.identity);
        ps.transform.SetParent(null);
        ps.transform.position = new Vector3(ps.transform.position .x, ps.transform.position .y,-3);
        ps.gameObject.SetActive(true);
        ps.TryGetComponent<ParticleSystem>(out ParticleSystem particle);
        if (particle)
        {
            ps.transform.rotation = transform.rotation;
            particle.Play();
        }
        ps.TryGetComponent<AudioSource>(out AudioSource src);
        if (src) src.Play();
        Destroy(ps, 0.3f);
    }
}
