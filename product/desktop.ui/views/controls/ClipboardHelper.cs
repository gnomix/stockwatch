using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace solidware.financials.windows.ui.views.controls
{
    static public class ClipboardHelper
    {
        static public List<string[]> ParseClipboardData()
        {
            var dataObj = Clipboard.GetDataObject();
            if (dataObj == null) return new List<string[]>();

            var clipboardRawData = dataObj.GetData(DataFormats.Text);
            if (clipboardRawData == null) return new List<string[]>();

            var rawDataStr = clipboardRawData as string;
            if (rawDataStr == null && clipboardRawData is MemoryStream)
            {
                var ms = clipboardRawData as MemoryStream;
                var sr = new StreamReader(ms);
                rawDataStr = sr.ReadToEnd();
            }
            Debug.Assert(rawDataStr != null, string.Format("clipboardRawData: {0}, could not be converted to a string or memorystream.", clipboardRawData));

            var clipboardData = new List<string[]>();
            var rows = rawDataStr.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            if (rows.Length > 0)
            {
                clipboardData = rows.Select(row => ParseTextFormat(row)).ToList();
            }
            else
            {
                Debug.WriteLine("unable to parse row data.  possibly null or contains zero rows.");
            }

            return clipboardData.Where(x => x != null && x.Count() > 0 && x.First() != "\0").ToList();
        }

        static string[] ParseTextFormat(string value)
        {
            var outputList = new List<string>();
            const char separator = '\t';
            var startIndex = 0;
            var endIndex = 0;

            for (var i = 0; i < value.Length; i++)
            {
                var ch = value[i];
                if (ch == separator)
                {
                    outputList.Add(value.Substring(startIndex, endIndex - startIndex));

                    startIndex = endIndex + 1;
                    endIndex = startIndex;
                }
                else if (i + 1 == value.Length)
                {
                    // add the last value
                    outputList.Add(value.Substring(startIndex));
                    break;
                }
                else
                {
                    endIndex++;
                }
            }

            return outputList.ToArray();
        }
    }
}