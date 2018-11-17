using UnityEngine;
using System.Collections;


public enum DayType
{
	DayTime,
	NightTime,
	RainTime
};


public class DayAndNightSystem : MonoBehaviour 
{

	public Material DaySky,NightSky,RainSky;
	public Color DayFog,NightFog,RainFog;
	public float DayFogDepth,NightFogDepth,RainFogDepth;
	public float DayLightIntensity,NightLightIntensity,RainLightIntensity;
	public Color DayLightColor,NightLightColor,RainLightColor;

	public bool IsDay,IsNight,IsRainy;
	public static DayType _dayType;


	public void Awake()
	{
		Invoke("SetType",0.01f);
	}

	void SetType()
	{
		if(_dayType == DayType.DayTime)
		{
			RenderSettings.skybox = DaySky;
			RenderSettings.fogDensity = DayFogDepth;
			RenderSettings.fogColor = DayFog;
			RenderSettings.ambientLight = DayLightColor;
			RenderSettings.ambientIntensity = DayLightIntensity;
		}

		if(_dayType == DayType.NightTime)
		{
			RenderSettings.skybox = NightSky;
			RenderSettings.fogDensity = NightFogDepth;
			RenderSettings.fogColor = NightFog;
			RenderSettings.ambientLight = NightLightColor;
			RenderSettings.ambientIntensity = NightLightIntensity;
		}

		if(_dayType == DayType.RainTime)
		{
			RenderSettings.skybox = RainSky;
			RenderSettings.fogDensity = RainFogDepth;
			RenderSettings.fogColor = RainFog;
			RenderSettings.ambientLight = RainLightColor;
			RenderSettings.ambientIntensity = RainLightIntensity;
		}
	}
}
