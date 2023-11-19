


using System.Drawing;
using Color = Microsoft.Maui.Graphics.Color;

namespace TheJobOrganizationApp.Models
{
    public class WorkerUIL
    {
        public bool IsPicked {  get; set; }
         
        public Worker Worker { get; set; }

        public static explicit operator WorkerUIL(Worker worker)
        {
            return new WorkerUIL {Worker=worker,IsPicked=false};
        }

        public string RGBCode { get => Worker.Color.ToHex(); }
}

    }

