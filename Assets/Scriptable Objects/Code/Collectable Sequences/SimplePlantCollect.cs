//Martin Pérez Villabrille
//Cat & Potions 
//Last Modification 15/12/2022

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName ="Collectables/Plant Collectable")]
public class SimplePlantCollect : CollectSequence
{

    //Simple collection coroutine to collect plants

    //You could add here Audio, Particles, UI instances ... 

    [SerializeField]
    [Tooltip("SO Audio Event that will play when the player interacts with the enviroment")]
    AudioEvent pickUpAudioEvent;

    public override IEnumerator CollectPlantCoroutine(MonoBehaviour runner)
    {
        //Generates an audioSource right at the point where the plant is being collected, then destroy it when the audio ends
        if (pickUpAudioEvent)
        {
            var audioPlayer = new GameObject("Pick Up SoundEffect", typeof(AudioSource)).GetComponent<AudioSource>();
            audioPlayer.transform.position = runner.transform.position;
            pickUpAudioEvent.Play(audioPlayer);
            Destroy(audioPlayer.gameObject, audioPlayer.clip.length * audioPlayer.pitch);
        }

        yield return null;
    }
}
