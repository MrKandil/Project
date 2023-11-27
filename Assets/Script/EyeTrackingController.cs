using UnityEngine;
using Varjo.XR;
using static Varjo.XR.VarjoEyeTracking;

public class EyeTrackingController : MonoBehaviour
{

    private Camera varjoCamera;
    void Start()
    {
        // R�cup�rer le composant de suivi oculaire VarjoEyeTracking


        // V�rifier si le composant est disponible
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
            // Obtenir les donn�es de suivi oculaire
            Vector2 leftEyeGaze = VarjoEyeTracking.GetGaze().left.origin;
            Vector2 rightEyeGaze = VarjoEyeTracking.GetGaze().right.origin;

            // Afficher les coordonn�es de point de fixation dans la console Unity
            Debug.Log("Coordonn�es de fixation de l'�il gauche : " + leftEyeGaze);
            Debug.Log("Coordonn�es de fixation de l'�il droit : " + rightEyeGaze);
            GazeData gd = VarjoEyeTracking.GetGaze();
            //Vector3 gazeDirection = varjoCamera.transform.TransformPoint(gd.right.origin);
        }
    }
}