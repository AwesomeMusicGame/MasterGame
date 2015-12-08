using UnityEngine;
using System.Collections;

public interface ISceneItem {
	
	Material podlahaMaterial { get; }
	Material skyboxMaterial { get; }
    GameObject prekazkaPunch { get; }
    GameObject prekazkaSlide { get; }
    Color fontColor { get; }
    Color noteColor { get; }
}
