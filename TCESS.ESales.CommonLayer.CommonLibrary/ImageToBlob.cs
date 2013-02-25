// -----------------------------------------------------------------------
// <copyright file="ImageToBlob.cs" company="Q3 Technologies">
// Copyright 2010 - 2011 - www.q3tech.com. All rights reserved.
// This class is created to provide methods to convert image file to bytes and bytes to image file in the application.
// </copyright>
// -----------------------------------------------------------------------

#region Using directives

using System;
using System.IO;

#endregion

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
	public class ImageToBlob
	{		
		/// <summary>
		/// To convert the image file to bytes 
		/// </summary>
		/// <param name="FilePath">File path of the string to be converted</param>
		/// <returns>Image in byte form</returns>
        public static byte[] ConvertImageToByteArray(String filePath)
		{
			byte[] rawData = null;
			FileStream imageStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(imageStream);
            rawData = reader.ReadBytes((int)imageStream.Length);
            
            //If bytes array does not contain a valid image
            if (rawData.Length == 0)
            {
                //Set a default image to byte array
                rawData = Globals._blankImageBytes;
            }

            imageStream.Close();
            return rawData;
		}

        /// <summary>
        /// To convert the byte array to System.Drawing.Image type
        /// </summary>
        /// <param name="byteArrayIn">byte array to be converted to image</param>
        /// <returns>returns an image</returns>
        public static System.Drawing.Image ConvertByteArrayToImage(byte[] byteArrayIn)
        {            
            MemoryStream ms = new MemoryStream(byteArrayIn);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
            return returnImage;
        }
	}
}