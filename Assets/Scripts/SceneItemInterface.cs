using UnityEngine;
using System.Collections;

public interface SceneItemInterface {
	
	Material podlahaMaterial { get; }
	Material skyboxMaterial { get; }
	GameObject prekazkaPunch { get; }
	GameObject prekazkaSlide { get; }
}
