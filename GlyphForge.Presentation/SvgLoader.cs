using System.IO;
using System.Windows.Media;
using System.Xml.Linq;

namespace GlyphForge.Presentation
{
    public static class SvgLoader
    {
        public static List<SvgIcon> LoadFromFolder(string folderPath)
        {
            List<SvgIcon> icons = new();

            string[] svgFiles = Directory.GetFiles(folderPath, "*.svg");

            foreach (string file in svgFiles)
            {
                XDocument document = XDocument.Load(file);

                XNamespace ns = document.Root?.Name.Namespace ?? "";

                IEnumerable<XElement> pathElements = document.Descendants(ns + "path");

                Geometry geometry = null;

                foreach (XElement path in pathElements)
                {
                    string data = path.Attribute("d")?.Value;

                    if (string.IsNullOrWhiteSpace(data))
                        continue;

                    try
                    {
                        Geometry g = Geometry.Parse(data);

                        g.Freeze();

                        if(geometry == null)
                        {
                            geometry = g;
                        }
                        else
                        {
                            geometry = Geometry.Combine(geometry, g, GeometryCombineMode.Union, null);
                            geometry.Freeze();
                        }
                    }
                    catch
                    {

                    }
                }

                icons.Add(new SvgIcon
                {
                    Name = Path.GetFileNameWithoutExtension(file),
                    Geometry = geometry
                });
            }

            return icons;
        }
    }
}
