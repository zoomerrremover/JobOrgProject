
using System.Reflection;

namespace TheJobOrganizationApp.Services
{
    public static class Misc
    {
        public static List<Color> colors { 
            get{
                {
                    var colorType = typeof(Colors); // Get the Type of the Color structure.
                                                   // Get all public static properties (these are the color properties of the Color structure).
                    var colorProperties = colorType.GetFields(BindingFlags.Static | BindingFlags.Public);

                    var colors = new List<Color>();

                    foreach (var colorProperty in colorProperties)
                    {
                        // Check if the property is a Color.
                        if (colorProperty.FieldType == typeof(Color))
                        {
                            colors.Add((Color)colorProperty.GetValue(null));
                        }
                    }

                    return colors;
                }
            }
        } 
    }
}
