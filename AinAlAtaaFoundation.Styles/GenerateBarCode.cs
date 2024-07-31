﻿using BarcodeStandard;
using SkiaSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace AinAlAtaaFoundation.Shared
{
    public static class GenerateBarCode
    {
        public static string ToBarCodeString(int code)
        {
            Image img = ToImage(code);

            string barcodeImageString = ImageToBase64(img, ImageFormat.Png);
            return barcodeImageString;
        }

        public static Image ToImage(int code)
        {
            Barcode b = new Barcode
            {
                IncludeLabel = true
            };
            return Image
                .FromStream(b.Encode(BarcodeStandard.Type.Code128, code.ToString(), SKColors.Black, SKColors.White, 290, 120)
                .Encode()
                .AsStream());
        }

        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
}