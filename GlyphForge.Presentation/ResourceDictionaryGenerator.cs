using System.Text;

namespace GlyphForge.Presentation
{
    public static class ResourceDictionaryGenerator
    {
        public static string Generate(List<SvgIcon> icons)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine("<ResourceDictionary xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"");
            stringBuilder.AppendLine("                    xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\">");
            stringBuilder.AppendLine();

            foreach (SvgIcon icon in icons)
            {
                stringBuilder.AppendLine($"    <Geometry x:Key=\"{icon.Name}\">");
                stringBuilder.AppendLine($"        {Format(icon.Geometry.ToString())}");
                //stringBuilder.AppendLine($"        {icon.Geometry}");
                stringBuilder.AppendLine("     </Geometry>");
                stringBuilder.AppendLine();
            }

            stringBuilder.AppendLine("</ResourceDictionary>");

            return stringBuilder.ToString();
        }

        public static string Format(string data, int maxLineLength = 100)
        {
            StringBuilder stringBuilder = new();

            int currentIndex = 0;

            bool firstLine = true;

            while (currentIndex < data.Length)
            {
                int remaining = data.Length - currentIndex;

                if (!firstLine)
                {
                    stringBuilder.Append("\t\t");
                }

                if (remaining <= maxLineLength)
                {
                    stringBuilder.Append(data[currentIndex..]);
                    break;
                }

                int breakIndex = data.LastIndexOf(',', currentIndex + maxLineLength, maxLineLength);

                if (breakIndex <= currentIndex)
                {
                    breakIndex = currentIndex + maxLineLength;
                }

                stringBuilder.Append(data[currentIndex..(breakIndex + 1)]);
                stringBuilder.AppendLine();

                currentIndex = breakIndex + 1;

                firstLine = false;
            }

            return stringBuilder.ToString();
        }
    }
}