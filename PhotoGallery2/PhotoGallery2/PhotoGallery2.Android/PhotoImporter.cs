﻿using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Annotations;
using PhotoGallery2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoGallery2.Droid
{
     public  class PhotoImporter : IPhotoImporter
       
    {

        private bool hasCheckedPermission;
        private string[] result;

        public bool ContinueWithPermission(bool granted)
        {
            if (!granted)
                return false;
            Android.Net.Uri imageUri =
                MediaStore.Images.Media.ExternalContentUri;
                
            var cursor =           
                Application.Context.ContentResolver.Query(
                    imageUri
                    , null
                    , MediaStore.IMediaColumns.MimeType + "=? or" +
                        MediaStore.IMediaColumns.MimeType + "=?"
                    , new string[] { "image/jpeg", "image/png" }
                    , MediaStore.IMediaColumns.DateModified
                );

            var paths = new List<string>(); 
            while  (cursor.MoveToNext())
                    {
                string path = cursor.GetString(
                    cursor.GetColumnIndex(MediaStore.IMediaColumns.Data)
                    );

                paths.Add(path);
                
            }
                    
            result = paths.ToArray();   

            hasCheckedPermission = true;
            return true;
        }


        private async Task<bool> Import()
        {
            string[] permissions = {Manifest.Permission.ReadExternalStorage};

            if (Application.Context.CheckSelfPermission(Manifest.Permission.ReadExternalStorage) == Android.Content.PM.Permission.Granted)
            {
                ContinueWithPermission(true);
                return true;
            }

            return true;

        }


        public Task<ObservableCollection<Photo>> Get(int start, int count, Quality quality = Quality.Low)
        {
            throw new NotImplementedException();
        }

        public Task<ObservableCollection<Photo>> Get(List<string> filenames, Quality quality = Quality.Low)
        {
            throw new NotImplementedException();
        }
    }
}