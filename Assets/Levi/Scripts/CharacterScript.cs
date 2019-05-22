using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CharacterScript : MonoBehaviour
{
    public enum ElementalTyping { Water, Fire, Wood }
    public ElementalTyping element;
    public ObjectStats stats;
    public float healthValue;
    public float charDmg;
    public GameObject target;
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public bool enemy;
    public GameObject lightDispenser;
    public bool player;
    public bool isAlive = true;
    public AttackStats spell;
    public bool particlesPlayed;

    List<GameObject> deactivatedObjects;

    //GameObject deactivatedParticle;
    GameObject particle;

    private void Start()
    {
        healthValue = stats.hp;        
        healthText.text = healthValue.ToString() + "/" + healthValue.ToString();
        healthSlider.maxValue = stats.hp;
        healthSlider.value = healthSlider.maxValue;
        if (spell != null)
        {
            charDmg = stats.spellPower + spell.damage;
        }
        if (enemy == true)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        deactivatedObjects = new List<GameObject>();
        if(player == true)
        {
            TurnOffObject("FireBall");
        }
    }

    public void RaiseObject(string name)
    {
        GameObject translatedObject = GameObject.Find(name);

        StartCoroutine(ObjectRise(translatedObject.transform));
    }

    public void LowerObject(string name)
    {
        GameObject translatedObject = GameObject.Find(name);

        StartCoroutine(ObjectLower(translatedObject.transform));
    }

    public void ParticleStart(string name)
    {
        TurnOnObject(name);
        particle = GameObject.Find(name);
        if (particle != null)
        {
            particle.GetComponent<ParticleContoller>().StartParticle();
        }
    }

    public void StopParticle(string name)
    {
        particle = GameObject.Find(name);
        particle.GetComponent<ParticleContoller>().StopParticle();
    }

    public void TurnOnObject(string name)
    {
        for(int i = 0; i < deactivatedObjects.Count; i++)
        {
            if(deactivatedObjects[i].name == name)
            {
                deactivatedObjects[i].SetActive(true);
            }
        }
    }

    public void TurnOffObject(string name)
    {
        particle = GameObject.Find(name);
        if(deactivatedObjects.Count == 0)
        {
            deactivatedObjects.Add(particle);
        }
        else
        {
            int counter = 0;
            for(int i = 0; i < deactivatedObjects.Count; i++)
            {
                if(particle.name == deactivatedObjects[i].name)
                {
                    counter++;
                }
            }
            if (counter == 0)
            {
                deactivatedObjects.Add(particle);
            }
        }
        particle.SetActive(false);
    }

    public void MoveObject(float distance)
    {
        particle.GetComponent<ParticleContoller>().MoveObject(distance);
    }

    public void ResetPosition(string name)
    {
        particle = GameObject.Find(name);
        particle.GetComponent<ParticleContoller>().ResetPosition();
    }

    public void PlayAudio(string name)
    {
        transform.Find(name).GetComponent<AudioSource>().Play();
    }

    private IEnumerator ObjectRise(Transform target)
    {
        Vector3 newPos = new Vector3(target.localPosition.x, target.localPosition.y, target.localPosition.z + 300);
        Vector3 buffer = target.localPosition;

        while(target.localPosition != newPos)
        {
            buffer.z += 15;
            target.localPosition = buffer;

            if(target.localPosition.z >= newPos.z)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

    private IEnumerator ObjectLower(Transform target)
    {
        Vector3 newPos = new Vector3(target.localPosition.x, target.localPosition.y, target.localPosition.z - 300);
        Vector3 buffer = target.localPosition;

        while (target.localPosition != newPos)
        {
            buffer.z -= 15;
            target.localPosition = buffer;

            if (target.localPosition.z <= newPos.z)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }

    }

}
