 #pragma strict
 var imageFolderName = "Resources";
 var MakeTexture = false;
 var pictures = new Array();
 var loop = false;
 var counter = 0;
 var Film = true;
 var PictureRateInSeconds:float = 1;
 private var nextPic:float = 0;
 
 function Start () {
     if(Film == true){
         PictureRateInSeconds = 0.04166666666666666666;
     }
 
     var textures : Object[] = Resources.LoadAll(imageFolderName);
     for(var i = 0; i < textures.Length; i++){
         Debug.Log("found");
         pictures.Add(textures[i]);
     }
 }
 
 function Update () {
     if(Time.time > nextPic){
         nextPic = Time.time + PictureRateInSeconds;
         counter += 1;
         if(MakeTexture){
             GetComponent.<Renderer>().material.mainTexture = pictures[counter];
         }
     }
     if(counter >= pictures.length){
         Debug.Log("fertig");
         if(loop){
             counter = 0;
         }
     }
 }