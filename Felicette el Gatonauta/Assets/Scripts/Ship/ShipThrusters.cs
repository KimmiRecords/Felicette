using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipThrusters : Ship, IGravity
{
    //la clase principal de nuestra nave
    //controla su rigidbody, y aplica la fuerza que corresponde
    //aplica powerups tambien

    public Animator m_Animator;
    bool _isThrusting;
    bool _isReleased;
    
    bool _isMoving;
    BasePositionDirection _dir;
    Vector3 _move;

    IPowerUp[] _powers = new IPowerUp[4];
    IPowerUp _currentPowerUp;

    ShipGasManager gasManager;


    void Start()
    {
        EventManager.Subscribe(Evento.ThrusterDown, StartThruster);
        EventManager.Subscribe(Evento.ThrusterDown, ReleaseShip);
        EventManager.Subscribe(Evento.ThrusterUp, EndThruster);
        EventManager.Subscribe(Evento.BasePositionDown, StartMoveShip);
        EventManager.Subscribe(Evento.BasePositionUp, EndMoveShip);
        EventManager.Subscribe(Evento.AtmosphereWall, EscapeAtmosphere);
        EventManager.Subscribe(Evento.CajaPickup, SetCurrentPowerUp);
        EventManager.Subscribe(Evento.PowerUpButtonUp, ActivatePowerUp);

        CurrentGas = maxGas;
        myRigidBody.useGravity = true;
        _isReleased = false;
        myRigidBody = this.gameObject.GetComponent<Rigidbody>();

        _powers[0] = new EmptyPowerUp(this);
        _powers[1] = new GasPowerUp(this);
        _powers[2] = new ScalePowerUp(this);
        _powers[3] = new CoinPowerUp(this);
        _currentPowerUp = _powers[0];

        gasManager = new ShipGasManager(this);
    }

    private void FixedUpdate()
    {
        if (_isThrusting && canThrust)
        {
            Thruster();
        }

        if (_isMoving && canThrust)
        {
            MoveShip();
        }
    }

    public void ReleaseShip(params object[] parameters)
    {
        //la primera vez que tocas el thrusterbutton, suelta a la nave de su base

        //myRigidBody.constraints = RigidbodyConstraints.None;
        //myRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        //myRigidBody.constraints = RigidbodyConstraints.FreezeRotationX;

        //freezeaar rotations
        _isReleased = true;
        EventManager.Unsubscribe(Evento.ThrusterDown, ReleaseShip);
    }

    public void StartThruster(params object[] parameters)
    {
        if (canThrust)
        {
            AudioManager.instance.PlayByName("PropulsoresSFX");
            _isThrusting = true;
            m_Animator.SetBool("IsThrusting", true);
        }
    }
    public void Thruster()
    {
        //pum para arriba, y consume gas
        myRigidBody.AddForce(transform.up * thrusterPower);
        if (canThrust)
        {
            gasManager.BurnGas();
        }
    }
    public void EndThruster(params object[] parameters)
    {
        AudioManager.instance.StopByName("PropulsoresSFX");
        _isThrusting = false;
        m_Animator.SetBool("IsThrusting", false);
    }

    public void StartMoveShip(params object[] parameters)
    {
        _isMoving = true;

        if (parameters[0] is BasePositionDirection)
        {
            _dir = (BasePositionDirection)parameters[0];
        }
        else
        {
            //print("ojo que el primer parametro que me pasaste no es un BasePositionDirection");
        }
    }
    public void MoveShip()
    {
        if (_dir == BasePositionDirection.left)
        {
            _move = Vector3.left;
        }
        else
        {
            _move = Vector3.right;
        }

        if (_isReleased && canThrust)
        {
            //fuera de la base se mueve mucho pero quema gas
            gasManager.BurnGas();
            transform.position += _move * moveSpeed * Time.deltaTime;
        }
        else
        {
            //en la base no quema gas pero se mueve poquito
            transform.position += _move * basePositionMoveSpeed * Time.deltaTime;
        }
    }
    public void EndMoveShip(params object[] parameters)
    {
        _isMoving = false;
    }

    public void EscapeAtmosphere(params object[] parameters)
    {
        AudioManager.instance.StopByName("RadioPreLaunchSFX");

        if (!AudioManager.instance.sound["EroicaLoop"].isPlaying)
        {
            AudioManager.instance.PlayByName("EroicaLoop");
        }
        myRigidBody.useGravity = false;
        //print("escapaste de la gravedad del planeta");
    }

    public void ApplyGravity(Vector3 planetPosition, float planetMass)
    {
        Vector3 grav = GravityForce.GetGravityVector3(myRigidBody.mass, planetMass, Vector3.Distance(transform.position, planetPosition), (planetPosition - transform.position).normalized);

        myRigidBody.AddForce(grav * Time.deltaTime, ForceMode.Force);
    }

    public void SetCurrentPowerUp(params object[] parameters)
    {
        print("setcurrentpowerup");
        if (parameters[0] is int)
        {
            switch ((int)parameters[0])
            {
                case 0: //si en la caja sale 0, es gas powerup
                    _currentPowerUp = _powers[1];
                    break;
                case 1: //si sale 1, es speed powerup
                    _currentPowerUp = _powers[2];
                    break;
                case 2:
                    _currentPowerUp = _powers[3];
                    break;
            }
        }
        else
        {
            print("ojo, no me pasaste int de primer parametro");
        }

        EventManager.Trigger(Evento.GotPowerUp, parameters[0]);

    }
    public void ActivatePowerUp(params object[] parameters)
    {
        //lo activo y lo hago empty de nuevo
        print("activate powerup");

        _currentPowerUp.Activate();

        if (_currentPowerUp == _powers[2]) //solo para modo chiquito
        {
            EventManager.Trigger(Evento.ModoChiquitoStart);
        }
        if (_currentPowerUp == _powers[3]) //solo para lluvia de monedas
        {
            m_Animator.SetTrigger("Monedas");
        }
        _currentPowerUp = _powers[0];
    }

    public void StartRescale()
    {
        StartCoroutine(Rescale());
    }
    public IEnumerator Rescale()
    {

        Vector3 originalScale = transform.localScale;

        //pasan cosas
        //print("arranca el boost");
        transform.localScale = Vector3.one * rescaleFactor;

        yield return new WaitForSeconds(1);
        AudioManager.instance.PlayByName("TimerFourTicks");

        yield return new WaitForSeconds(rescaleDuration - 1);

        //terminan de pasar cosas
        //print("termina el boost");
        AudioManager.instance.PlayByName("ModoChiquitoOff");
        EventManager.Trigger(Evento.ModoChiquitoEnd);
        transform.localScale = originalScale;

    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded) //cuando se destruye porque lo destrui a mano
        {
            //print("destrui a este shipthrusters on isloaded");
        }
        else //cuando se destruye porque cambie de escena
        {
            EventManager.Unsubscribe(Evento.ThrusterDown, StartThruster);
            EventManager.Unsubscribe(Evento.ThrusterDown, ReleaseShip);
            EventManager.Unsubscribe(Evento.ThrusterUp, EndThruster);
            EventManager.Unsubscribe(Evento.AtmosphereWall, EscapeAtmosphere);
            EventManager.Unsubscribe(Evento.CajaPickup, SetCurrentPowerUp);
            EventManager.Unsubscribe(Evento.PowerUpButtonUp, ActivatePowerUp);
            //print("destrui a este shipthrusters on sceneclosure");
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ITriggerCollider>() != null)
        {
            var collider = other.GetComponent<ITriggerCollider>();
            collider.Activate();
        }
    }
}
