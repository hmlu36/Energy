using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace JamZoo.Project.WebSite.Attribute
{
    public class ValidateImageAttribute : RequiredAttribute
    {
        public int MinWidth { get; private set; }
        public int MinHeight { get; private set; }

        public ValidateImageAttribute()
            : this(480, 640)
        {
        }
        public ValidateImageAttribute(int MinWidth, int MinHeight)
        {
            this.MinWidth = MinWidth;
            this.MinHeight = MinHeight;
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;
            if (file == null)
            {
                return true;
            }

            
            if (file.ContentLength > 2 * 1024 * 1024)
            {
                return false;
            }

            try
            {
                
                using (var img = Image.FromStream(file.InputStream))
                {
                    if (img.Width < MinWidth || img.Height < MinHeight)
                        return false;

                    return img.RawFormat.Equals(ImageFormat.Jpeg);
                }
            }
            catch { }
            return false;
        }
    }
}
