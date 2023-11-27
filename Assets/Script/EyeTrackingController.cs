using UnityEngine;
using Varjo.XR;
using static Varjo.XR.VarjoEyeTracking;

public class EyeTrackingController : MonoBehaviour
{

    private Camera varjoCamera;
    void Start()
    {
        // Récupérer le composant de suivi oculaire VarjoEyeTracking


        // Vérifier si le composant est disponible
        if (((int)VarjoEyeTracking.GetGazeCalibrationQuality().left) >=1 )
        {
            Debug.Log("Suivi oculaire Varjo disponible");
        }
        else
        {
            Debug.LogError("Le suivi oculaire Varjo n'est pas disponible.");
        }
    }

    void Update()
    {
        //Debug.Log(((int)VarjoEyeTracking.GetGazeCalibrationQuality().left));
        if (((int)VarjoEyeTracking.GetGazeCalibrationQuality().left) >= 1)
        {
            //Debug.Log("Suivi oculaire Varjo disponible");
        }
        else
        {
            //Debug.LogError("Le suivi oculaire Varjo n'est pas disponible.");
        }
        if ((int)(VarjoEyeTracking.GetGaze().status)==2)
        {
            // Obtenir les données de suivi oculaire
            Vector2 leftEyeGaze = VarjoEyeTracking.GetGaze().left.origin;
            Vector2 rightEyeGaze = VarjoEyeTracking.GetGaze().right.origin;

            // Afficher les coordonnées de point de fixation dans la console Unity
            Debug.Log("Coordonnées de fixation de l'œil gauche : " + leftEyeGaze);
            Debug.Log("Coordonnées de fixation de l'œil droit : " + rightEyeGaze);
            GazeData gd = VarjoEyeTracking.GetGaze();
            //Vector3 gazeDirection = varjoCamera.transform.TransformPoint(gd.right.origin);
        }
    }
}