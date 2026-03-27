using PdfSharpCore.Fonts;

namespace Workshopmanagement.Utility
{
    public class PdfFontResolver : IFontResolver
    {
        private static readonly string FontDir = GetFontDir();

        private static string GetFontDir()
        {
            // Fedora / RHEL
            if (Directory.Exists("/usr/share/fonts/liberation-sans-fonts"))
                return "/usr/share/fonts/liberation-sans-fonts";
            // Debian / Ubuntu (Docker aspnet image after apt install fonts-liberation)
            if (Directory.Exists("/usr/share/fonts/truetype/liberation"))
                return "/usr/share/fonts/truetype/liberation";
            // Fallback: fonts bundled beside the app
            return Path.Combine(AppContext.BaseDirectory, "fonts");
        }

        private const string FaceRegular    = "LiberationSans-Regular";
        private const string FaceBold       = "LiberationSans-Bold";
        private const string FaceItalic     = "LiberationSans-Italic";
        private const string FaceBoldItalic = "LiberationSans-BoldItalic";

        public string DefaultFontName => FaceRegular;

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            // Map every font family to Liberation Sans (the available Linux font)
            var face = (isBold, isItalic) switch
            {
                (true, true)   => FaceBoldItalic,
                (true, false)  => FaceBold,
                (false, true)  => FaceItalic,
                _              => FaceRegular,
            };
            return new FontResolverInfo(face);
        }

        public byte[] GetFont(string faceName)
        {
            var fileName = faceName switch
            {
                FaceBold      => "LiberationSans-Bold.ttf",
                FaceItalic    => "LiberationSans-Italic.ttf",
                FaceBoldItalic => "LiberationSans-BoldItalic.ttf",
                _             => "LiberationSans-Regular.ttf",
            };
            return File.ReadAllBytes(Path.Combine(FontDir, fileName));
        }
    }
}
