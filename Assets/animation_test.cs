using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;


public class animation_test : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioHaptic;

  

    const int channel = 1;
    //OVRHapticsClip HapticClip;

    public Rigidbody rb;
    public AudioSource shurikenSound;
    public AudioSource bladeSound;
    public AudioSource hitSound;



    private float speed = 1;
    private bool beforeFistCollision = true;

    private Vector3 directionColision = Vector3.zero;
    private float inclinitationShuriken = 0.0f;


    public void PlaySoundThrow()
    {
        shurikenSound.Play();
    }

    
    void Awake()
    {
        inclinitationShuriken = Random.Range(-15, 15);
        Vector3 playerPos = GameObject.Find("CenterEyeAnchor").transform.position;
        Vector3 shurikenPos = this.transform.position;

        Vector3 direction = playerPos - shurikenPos;
        direction.Normalize();

        Quaternion rotVec = Quaternion.LookRotation(-direction);
        Quaternion rotx=Quaternion.Euler(90.0f + inclinitationShuriken, 0, 0);
        this.transform.rotation = rotVec * rotx;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (beforeFistCollision)
        {
            Vector3 playerPos = GameObject.Find("CenterEyeAnchor").transform.position;
            Vector3 shurikenPos = this.transform.position;

            Vector3 direction = playerPos - shurikenPos;
            direction.Normalize();
            rb.velocity = direction * speed;

            this.transform.Rotate(0.0f, 0.0f, 360.0f * Time.deltaTime * 4, Space.Self);

        }
        else
        {
            rb.velocity = directionColision * speed;
        }

    }

    public void SetSpeed(float sp)
    {
        speed=sp;
    }

    void VibrateRightHand()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        UnityEngine.XR.InputDevices.GetDevicesWithRole(UnityEngine.XR.InputDeviceRole.RightHanded, devices);

        foreach (var device in devices)
        {
            UnityEngine.XR.HapticCapabilities capabilities;
            if (device.TryGetHapticCapabilities(out capabilities))
            {
                if (capabilities.supportsImpulse)
                {
                    uint channel = 0;
                    float amplitude = 1.0f;
                    float duration = 0.5f;
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
   
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "CenterEyeAnchor")
        {
            RawImage damage = GameObject.Find("RawImageScreen").GetComponent<RawImage>();
            Color newColor = damage.color;
            newColor.a += 0.20f;

            if (newColor.a>=1.0f)
            {
                newColor.a = 1.0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            damage.color = newColor;

            hitSound.Play();

       
            Destroy(this.gameObject);
        }
        
        else if ((collision.gameObject.name == "katana") || (collision.gameObject.name == "katanaHR"))
        {
            if (beforeFistCollision)
            {                
                Vector3 bladePos = GameObject.Find(collision.gameObject.name).transform.position;
                Vector3 shurikenPos = this.transform.position;
                Vector3 direction = shurikenPos - bladePos;
                direction.Normalize();



                directionColision = collision.contacts[0].normal;
                bladeSound.Play();

                VibrateRightHand();

               // OVRHapticsClip haptic = new OVRHapticsClip(audioHaptic);
              //  OVRHaptics.RightChannel.Preempt(haptic);

                Destroy(this.gameObject,10.0f);
            }

            beforeFistCollision = false;
        }

    }
}
