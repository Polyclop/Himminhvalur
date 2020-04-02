using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoGameEvent : MonoBehaviour
{
    bool isInside;


    /// DANS MON GAME EVENT
    /// je déclare mon événement 

        // Le nom de mon event 
        // et le type de paramètre que je vais transmettre quand je le déclencherai
        public event Action<bool> onBeingSeen;

        // La Fonction liée a cet event qui va s'activer lors de l'activation
        public void BeSeen(bool seen)
        {
            if (onBeingSeen != null)
            {
                onBeingSeen(seen);
            }
        }


    /// DANS MON SCRIPT DECLENCHANT L'EVENT
    /// Quand les conditions sont réunies je déclenche l'événement

        private void OnTriggerEnter(Collider other)
        {
            isInside = true;

            // j'avais dit avant que je voulais transmettre une variable alors je la transmet
            GameEvents.current.BeSeen(isInside);
        }


    /// DANS MON SCRIPT QUI ECOUTE
    /// Je souscrit au début a l'événement, et je crée une fonction qui se lancera dans ce cas


        // Ici je veux récupérer la valeur que je transmet via l'événement
        bool seen;

        void Start()
        {
            // Souscription
            GameEvents.current.onBeingSeen += OnGettingCaughtScreenShake;
        }

        // Ma fonction qui se lancera automatiquement 
        void OnGettingCaughtScreenShake(bool caught)
        {
            // je récupère ma valeur
            seen = caught;
        }
}