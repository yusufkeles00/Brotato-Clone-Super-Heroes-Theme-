using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectSc : MonoBehaviour
{
	public List<Renderer> renderers = new List<Renderer>();

	public Material white;

	IEnumerator DamageEffect(float delay)
	{
		List<Material> mainMats = new List<Material>();
		foreach (var item in renderers)
		{
			mainMats.Add(item.material);
			item.material = white;
		}
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < renderers.Count; i++)
		{
			renderers[i].material = mainMats[i];
		}
	}
}
