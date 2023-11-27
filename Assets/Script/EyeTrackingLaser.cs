using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Varjo.XR;
using UnityEngine.UI;
using TMPro;

public class EyeTrackingLaser : MonoBehaviour
{
    public LineRenderer laserLineRenderer;
    public float laserMaxLength = 10.0f; // Longueur maximale du laser

    public Vector3 eyePosition;
    public Vector3 laserDirection;
    public GameObject lastObjet = null;
    public Point point= new Point();
    private float elapsedTime = 0f;

    public List<Point> lstPoint = new List<Point>();

    public Text texteAffiche;

    public class Point
    {
        public string objet;
        public float temps;

    }

    public void Start()
    {
        // Obtenez la position de l'œil gauche. Vous devrez adapter cela en fonction de votre configuration Varjo.
        VarjoEyeTracking.RequestGazeCalibration();
        eyePosition = VarjoEyeTracking.GetGaze().gaze.origin;
        //eyePosition.y = -1.23f;
    }
    private void Update()
    {
        elapsedTime = Time.deltaTime;
        // Assurez-vous que le Line Renderer est attaché à un GameObject avec les bons paramètres.
        if (laserLineRenderer == null)
        {
            Debug.LogError("Le Line Renderer n'est pas correctement configuré.");
            return;
        }

        // Calculez la direction du "laser" en utilisant la direction du regard de l'œil.
        laserDirection = VarjoEyeTracking.GetGaze().gaze.forward;

        // Tracez le rayon du laser.
        Ray ray = new(eyePosition, laserDirection);

        // Si le rayon du laser heurte un objet, ajustez la longueur du laser.
        if (Physics.Raycast(ray, out RaycastHit hit, laserMaxLength) && hit.collider.gameObject != lastObjet)
        {
            point=new Point();
            laserLineRenderer.SetPosition(0, eyePosition); // Début du laser à l'œil
            laserLineRenderer.SetPosition(1, hit.point);   // Fin du laser à la position de l'impact
            lastObjet = hit.collider.gameObject;
            point.objet = hit.collider.gameObject.name;
            point.temps = elapsedTime;
            lstPoint.Add(point);
            string result = "";
            for(int i = 0; i < lstPoint.Count; i++)
            {
                result += lstPoint[i].objet + Environment.NewLine;
            }
            AfficherContenuListe();
            Debug.Log(result);
        }
        else
        {
            // Si le rayon du laser ne touche rien, affichez le laser complet.
            laserLineRenderer.SetPosition(0, eyePosition);
            laserLineRenderer.SetPosition(1, eyePosition + laserDirection * laserMaxLength);
        }
    }

    void AfficherContenuListe()
    {
        string contenuTexte = "";

        foreach (Point p in lstPoint)
        {
            contenuTexte += p.objet + "     " + p.temps + "\n"; // Ajouter chaque élément suivi d'un saut de ligne.
        }

        // Affecter la chaîne construite à l'objet Text.
        texteAffiche.text = contenuTexte;
    }
}

