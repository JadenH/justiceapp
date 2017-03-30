using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Singleton;

namespace _scripts
{
    public class ProfileData : MonoSingleton<ProfileData>
    {

        public List<Sprite> ProfilePictures;

        public Sprite GetProfilePicture(string fileName)
        {
            
            if (ProfilePictures.Exists(picture => picture.name + ".png" == fileName))
            {
                return ProfilePictures.Find(picture => picture.name + ".png" == fileName);
            }
            throw new UnityException("Image not found for: " + fileName);
        }
    }
}