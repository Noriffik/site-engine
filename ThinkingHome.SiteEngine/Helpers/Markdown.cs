using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CommonMark;
using CommonMark.Formatters;
using CommonMark.Syntax;

namespace ThinkingHome.SiteEngine.Helpers
{
    public static class Markdown
    {
        private static readonly CommonMarkSettings settings;

        static Markdown()
        {
            settings = CommonMarkSettings.Default.Clone();
            settings.OutputFormat = OutputFormat.CustomDelegate;
            settings.OutputDelegate =
                (doc, output, settings) => new CustomHtmlFormatter(output, settings).WriteDocument(doc);
        }

        public static string ConvertToHtml(string md)
        {
            return CommonMarkConverter.Convert(md, settings);
        }

        private static string GenerateId(Block block)
        {
            var sb = new StringBuilder();

            foreach (var entry in block.AsEnumerable())
            {
                sb.Append(entry.Inline?.LiteralContent);
            }

            return Regex.Replace(sb.ToString(), "[^a-zA-Zа-яА-ЯёЁ]+", "-").Trim('-');
        }

        private class CustomHtmlFormatter : HtmlFormatter
        {
            public CustomHtmlFormatter(TextWriter target, CommonMarkSettings settings) : base(target, settings)
            {
            }

            protected override void WriteBlock(Block block, bool isOpening, bool isClosing, out bool ignoreChildNodes)
            {
                var tag = block.Tag;

                if ((tag == BlockTag.AtxHeading || tag == BlockTag.SetextHeading) && !RenderPlainTextInlines.Peek())
                {
                    ignoreChildNodes = false;

                    var level = block.Heading.Level;

                    if (isOpening)
                    {
                        EnsureNewLine();

                        var id = GenerateId(block);

                        Write($"<h{level} id=\"{id}\"");

                        if (Settings.TrackSourcePosition)
                        {
                            WritePositionAttribute(block);
                        }

                        Write('>');
                    }

                    if (isClosing)
                    {
                        WriteLine($"</h{level}>");
                    }
                }
                else
                {
                    base.WriteBlock(block, isOpening, isClosing, out ignoreChildNodes);
                }
            }
        }
    }
}