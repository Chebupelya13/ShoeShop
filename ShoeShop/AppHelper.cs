using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShoeShop
{
    public static class AppHelper
    {
        public static void SetAppIcon(Form form)
        {
            try
            {
                string iconPath = System.IO.Path.Combine(Application.StartupPath, "logo.png");
                if (System.IO.File.Exists(iconPath))
                {
                    using (Bitmap bmp = new Bitmap(iconPath))
                    {
                        form.Icon = Icon.FromHandle(bmp.GetHicon());
                    }
                }
            }
            catch
            {
                // Если не удалось загрузить, игнорируем (останется стандартная иконка)
            }
        }
    }
}
