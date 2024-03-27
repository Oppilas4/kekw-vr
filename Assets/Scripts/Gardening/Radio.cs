using UnityEngine;

namespace Gardening
{
	public class Radio : MonoBehaviour
	{
		[SerializeField] private AudioSource source;
		[SerializeField] private AudioClip[] musicClips;
		[SerializeField] private Transform   startTransform, endTransform, chanelPanel;
	
		public void UpdateMusic(XRKnob knob)
		{
			int index = (int) (knob.Value * (musicClips.Length - 1));
			
			source.clip = musicClips[index];
			source.Play();

			chanelPanel.position = Vector3.Lerp(startTransform.position, endTransform.position, knob.Value);
		}
	}
}
