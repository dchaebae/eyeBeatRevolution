using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayScript : MonoBehaviour {
    public Light attachedLight;
    public FoveInterfaceBase foveInterface;
    public ChuckSubInstance audiok;
    public AudioSource attachedClip; 

    private Collider instrument_face;
    private Material light_material;
    private bool light_attached = false;
    private bool clip_attached = false;
    private bool clip_toggle = false;
    private bool audio_enabled = false;
    private bool audio_toggle = false;

	// Use this for initialization
	void Start () {
        instrument_face = GetComponent<Collider>();

        if (attachedLight == null)
            attachedLight = transform.GetComponentInChildren<Light>(); // get the light component

        if (attachedClip == null)
            attachedClip = transform.GetComponent<AudioSource>(); // get the audio component

        if (attachedLight)
        {
            light_attached = true;
            attachedLight.enabled = false; // light is not on when initializing
        }
        light_material = gameObject.GetComponent<Renderer>().material; // grab the material for possible lighting changes

        if (attachedClip)
        {
            clip_attached = true;
            clip_toggle = false;
        } 

        if (light_material == null)
            gameObject.SetActive(false);

        if (audiok == null)
            audiok = GetComponent<ChuckSubInstance>();

        if (audiok)
        {
            audio_enabled = true;
            audio_toggle = false;


            GetComponent<ChuckSubInstance>().RunCode(@"
            fun void playHH() {
                Noise n => HPF f => ADSR e => dac;
                f.freq(7000.0);
                n.gain(.9);
                e.set(2::ms, (80+Math.random2(0, 60))::ms, 0.0, 120::ms);
                e.keyOn();
                100::ms => now;
                }
            global Event HHImpact;
            while(true)
            {
                HHImpact => now;
                spork~playHH();
            }");

            GetComponent<ChuckSubInstance>().RunCode(@"
            class Toms 
            { 
                Impulse i; // the attack 
                i => Gain g1 => Gain g1_fb => g1 => LPF g1_f => Gain TomFallFreq; // tom decay pitch envelope 
                i => Gain g2 => Gain g2_fb => g2 => LPF g2_f; // tom amp envelope 
    
                // drum sound oscillator to amp envelope to overdrive to LPF to output 
                TomFallFreq => SinOsc s => Gain ampenv => SinOsc s_ws => LPF s_f => Gain output; 
                Step BaseFreq => s; // base Tom pitch 
    
                g2_f => ampenv; // amp envelope of the drum sound 
                3 => ampenv.op; // set ampenv a multiplier 
                1 => s_ws.sync; // prepare the SinOsc to be used as a waveshaper for overdrive 
    
                // set default 
                100.0 => BaseFreq.next; 
                50.0 => TomFallFreq.gain; // tom initial pitch: 80 hz 
                // 1.0 - 1.0 / 4000 => g1_fb.gain; // tom pitch decay 
                .9998 => g1_fb.gain; // tom pitch decay 
                g1_f.set(200, 1); // set tom pitch attack 
                //1.0 - 1.0 / 5000 => g2_fb.gain; // tom amp decay 
                .9998 => g2_fb.gain; // tom amp decay 

                g2_f.set(80, 1); // set tomD amp attack 
                .5 => ampenv.gain; // overdrive gain 
                s_f.set(1000, 1); // set tom lowpass filter 
    
                fun void hit(float v) 
                { 
                    v => i.next; 
                } 
                fun void setBaseFreq(float f) 
                { 
                    f => BaseFreq.next; 
                }    
                fun void setFreq(float f) 
                { 
                    f => TomFallFreq.gain; 
                } 
                fun void setPitchDecay(float f) 
                { 
                    f => g1_fb.gain; 
                } 
                fun void setPitchAttack(float f) 
                { 
                    f => g1_f.freq; 
                } 
                fun void setDecay(float f) 
                { 
                    f => g2_fb.gain; 
                } 
                fun void setAttack(float f) 
                { 
                    f => g2_f.freq; 
                } 
                fun void setDriveGain(float g) 
                { 
                    g => ampenv.gain; 
                } 
                fun void setFilter(float f) 
                { 
                    f => s_f.freq; 
                } 
            } 

            Toms T; 
            T.output => dac; 

            fun void playTom(int x) {
                if (x == 1) {
                    T.setBaseFreq(190); 
                    T.hit(.4 + Math.random2f(0, 0.2)); }
                else if (x == 2) {
                    T.setBaseFreq(140); 
                    T.hit(.6 + Math.random2f(0, .2)); }
                else if (x == 3){
                    T.setBaseFreq(90);
                    T.hit(.8 + Math.random2f(0, .2));}
            }

            global int Choice;
            global Event TImpact;

            while(true)
            {
                TImpact => now;
                spork~playTom(Choice);
            }
");
        }
    }

	
	// Update is called once per frame
	void Update () {
		if (foveInterface.Gazecast(instrument_face))
        {
            light_material.EnableKeyword("_EMISSION");

            if (light_attached)
            {
                light_material.SetColor("_EmissionColor", attachedLight.color);
                attachedLight.enabled = true;
                DynamicGI.SetEmissive(GetComponent<Renderer>(), attachedLight.color);
            }

            if (clip_attached && !clip_toggle)
            {
                attachedClip.Play();
                clip_toggle = true;
            }

            if (audio_enabled && !audio_toggle)
            {
                if (instrument_face.gameObject.CompareTag("HH"))
                {
                    GetComponent<ChuckSubInstance>().BroadcastEvent("HHImpact");
                    audio_toggle = true;
                }
                else if (instrument_face.gameObject.CompareTag("Th"))
                {
                    GetComponent<ChuckSubInstance>().SetInt("Choice", 1);
                    GetComponent<ChuckSubInstance>().BroadcastEvent("TImpact");
                }
                else if (instrument_face.gameObject.CompareTag("Tm"))
                {
                    GetComponent<ChuckSubInstance>().SetInt("Choice", 2);
                    GetComponent<ChuckSubInstance>().BroadcastEvent("TImpact");
                }
                else if (instrument_face.gameObject.CompareTag("Tl"))
                {
                    GetComponent<ChuckSubInstance>().SetInt("Choice", 3);
                    GetComponent<ChuckSubInstance>().BroadcastEvent("TImpact");
                }
                    
                audio_toggle = true;
            }
        }
        else
        {
            light_material.DisableKeyword("_EMISSION");
            gameObject.GetComponent<Renderer>().material.color = Color.white;

            if (light_attached)
            {
                attachedLight.enabled = false;
                DynamicGI.SetEmissive(GetComponent<Renderer>(), Color.black);
            }
            clip_toggle = false;
            audio_toggle = false;

        }
	}
}