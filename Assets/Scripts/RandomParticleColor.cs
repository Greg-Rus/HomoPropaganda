using UnityEngine;
using System.Collections;

public class RandomParticleColor : MonoBehaviour {
	public ParticleSystem myParticleSystem;
	public bool jobDone = false;
	public Color[] colors;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if(myParticleSystem.particleCount > 0 && !jobDone)
		{
			ParticleSystem.Particle[] particles = new ParticleSystem.Particle[myParticleSystem.maxParticles];
			myParticleSystem.GetParticles(particles);
			for(int i = 0; i < particles.Length; i++)
			{
				particles[i].color = colors[Random.Range(0,6)];
			}
			myParticleSystem.SetParticles(particles, myParticleSystem.particleCount);
			jobDone = true;
		}

	
	}
}
