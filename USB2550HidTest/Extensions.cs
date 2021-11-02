using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace System
{

    public static class xtend
    {
        public static string ToBase64(this string thisString)
        {
            return Convert.ToBase64String(Encoding.ASCII.GetBytes(thisString), Base64FormattingOptions.None);
        }
        public static string FromBase64(this string thisString)
        {
            return Encoding.ASCII.GetString(Convert.FromBase64String(thisString));
        }
        public static int[] getNonZeroIndexes(this byte[] thisByteArray, int trimStart, int trimEnd, int wordLength)
        {
            List<int> indexes = new List<int>();

            if ((trimEnd - trimStart) % wordLength != 0)
                return indexes.ToArray();
            bool notZero = true;
            int byteGroupCount = wordLength;
            int byteGroupIndex = 0;
            for (int i = trimStart; (i < thisByteArray.Length) && (i < trimEnd); i++)
            {
                notZero = true;
                byteGroupCount = wordLength;
                while ((i < thisByteArray.Length) && (i < trimEnd) && (byteGroupCount != 1))
                {
                    if (thisByteArray[i] != 0)
                        notZero = false;
                    byteGroupCount--;
                    i++;
                }
                if (notZero)
                    indexes.Add(byteGroupIndex);
                byteGroupIndex++;

               
            }
            return indexes.ToArray();
        }

        public static string ToHexString(this byte[] thisByteArray, string seperator, int bytesInGroup, bool showInReverseOrder)
        {
            if (showInReverseOrder)
                Array.Reverse(thisByteArray);
            return thisByteArray.ToHexString(seperator, bytesInGroup);
        }
        public static string ToHexString(this byte[] thisByteArray, string seperator, int bytesInGroup)
        {
            if (thisByteArray == null)
                return "input == null";
            if (thisByteArray.Length == 0)
                return "input.Length == 0";

            string result = string.Empty;

            bool notFirst = false;
            int byteGroupCount = bytesInGroup;

            for (int i = 0; i < thisByteArray.Length; i++)
            {
                if (notFirst && (byteGroupCount != 1))
                {
                    result += seperator;
                }
                if (byteGroupCount != 1)
                    byteGroupCount--;
                else
                    byteGroupCount = bytesInGroup;

                result += thisByteArray[i].ToString("X2");
                notFirst = true;
            }

            return result;

            
        }

        public static byte[] Trim(this byte[] thisByteArray, int trimStart, int trimEnd)
        {
            byte[] byteTempArray = new byte[trimEnd - trimStart];
            for (int i = 0; trimStart < trimEnd; i++, trimStart++)
                byteTempArray[i] = thisByteArray[trimStart];
            return byteTempArray;
        }
    }
}

namespace System.Windows.Forms
{
    public static class ToolStripItemExtension
    {
        public static ContextMenuStrip GetContextMenuStrip(this ToolStripItem item)
        {
            ToolStripItem itemCheck = item;

            while (!(itemCheck.GetCurrentParent() is ContextMenuStrip) && itemCheck.GetCurrentParent() is ToolStripDropDown)
            {
                itemCheck = (itemCheck.GetCurrentParent() as ToolStripDropDown).OwnerItem;
            }

            return itemCheck.GetCurrentParent() as ContextMenuStrip;
        }
        public static Control GetSourceControl(this ToolStripItem item)
        {
            return item.GetContextMenuStrip().SourceControl;
        }
        public static TextBoxBase GetSourceControlAsTextBoxBase(this ToolStripItem item)
        {
            return item.GetContextMenuStrip().SourceControl as TextBoxBase;
        }
    }
    public static class TextBoxBaseExtension
    {
        public static void Delete(this TextBoxBase thisTextBoxBase)
        {
            int SelectionIndex = thisTextBoxBase.SelectionStart;
            int SelectionCount = thisTextBoxBase.SelectionLength;
            thisTextBoxBase.Text = thisTextBoxBase.Text.Remove(SelectionIndex, SelectionCount);
            thisTextBoxBase.SelectionStart = SelectionIndex;
            thisTextBoxBase = null;
        }
    }
    public static class TextInputContextMenuStrip
    {
        public static ContextMenuStrip GetDefault()
        {
            ContextMenuStrip cms = new ContextMenuStrip();

            cms.Items.Add("Undo", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().Undo(); });
            cms.Items.Add(new ToolStripSeparator());
            cms.Items.Add("Cut", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().Cut(); });
            cms.Items.Add("Copy", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().Copy(); });
            cms.Items.Add("Paste", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().Paste(); });
            cms.Items.Add("Delete", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().Delete(); });
            cms.Items.Add(new ToolStripSeparator());
            cms.Items.Add("Select All", null, delegate (object s, EventArgs ea) { ((ToolStripItem)s).GetSourceControlAsTextBoxBase().SelectAll(); });
            cms.Items.Add(new ToolStripSeparator());

            cms.Opening += delegate (object s, System.ComponentModel.CancelEventArgs cea)
            {
                TextBoxBase txtBoxBase = ((ContextMenuStrip)s).SourceControl as TextBoxBase;
                cms.Items["Undo"].Enabled = txtBoxBase.CanUndo;
                bool isTextSelected = (txtBoxBase.SelectedText.Length != 0);
                cms.Items["Cut"].Enabled = isTextSelected;
                cms.Items["Copy"].Enabled = isTextSelected;
                cms.Items["Delete"].Enabled = isTextSelected;
                cms.Items["Paste"].Enabled = Clipboard.ContainsText();
                cms.Items["Select All"].Enabled = (txtBoxBase.Text.Length != 0);
            };

            return cms;
        }
    }
    public static class QuickDialogs
    {
        public delegate void FileSelectedEventHandler(string filePath);
        public delegate void FileSelectedAndReadEventHandler(ref string fileContents);


        public static void SelectFile(string title, string filter, FileSelectedEventHandler eventHandler)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileOk += delegate (object s, System.ComponentModel.CancelEventArgs cea)
                {
                    if (eventHandler != null)
                        eventHandler(((OpenFileDialog)s).FileName);
                };
                ofd.ShowDialog();
            }
        }

        public static void SelectAndReadFile(string title, string filter, FileSelectedAndReadEventHandler eventHandler)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.FileOk += delegate (object s, System.ComponentModel.CancelEventArgs cea)
                {
                    if (eventHandler != null)
                    {
                        string filePath = ((OpenFileDialog)s).FileName;
                        if (System.IO.File.Exists(filePath))
                        {
                            string contents = System.IO.File.ReadAllText(filePath);
                            eventHandler(ref contents);
                        }
                    }
                };
                ofd.ShowDialog();
            }
        }
    }
    public static class xtend
    {
        public static bool HighlightNotCharacters_ThreadSafe(this RichTextBox thisRichTextBox, string notChars, System.Drawing.Color highlightColor)
        {
            int richTextLength = 0;
            string rtext = string.Empty;

            ThreadSafe.Exec(thisRichTextBox, () => richTextLength = thisRichTextBox.TextLength);
            ThreadSafe.Exec(thisRichTextBox, () => rtext = thisRichTextBox.Text);
            bool highlightChar;
            bool anyHighlightChar = false;
            for (int ci = 0; ci < richTextLength; ci++)
            {
                highlightChar = true;
                for (int nci = 0; nci < notChars.Length; nci++)
                {
                    if (rtext[ci] == notChars[nci])
                    {
                        highlightChar = false;
                        break;
                    }
                }
                if (highlightChar)
                {
                    anyHighlightChar = true;
                    ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = ci);
                    ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 1);
                    ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionBackColor = highlightColor);
                }
            }
            return anyHighlightChar;
        }

        public static void AppendLine_ThreadSafe(this RichTextBox thisRichTextBox, string text, bool appendAtEnd)
        {
            if (appendAtEnd)
            {
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = thisRichTextBox.TextLength);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedText = text);
                thisRichTextBox.AppendLineBreak_ThreadSafe();
            }
            else
            {
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedText = Environment.NewLine);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedText = text);
            }
        }

        public static void AppendRtfLine_ThreadSafe(this RichTextBox thisRichTextBox, string rtf, bool appendAtEnd)
        {
            if (appendAtEnd)
            {
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = thisRichTextBox.TextLength);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedRtf = rtf);
                thisRichTextBox.AppendLineBreak_ThreadSafe();
            }
            else
            {
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedText = Environment.NewLine);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectedRtf = rtf);
            }  
        }

        public static void AppendLineBreak_ThreadSafe(this RichTextBox thisRichTextBox)
        {
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.AppendText(Environment.NewLine));
        }

        public static string GetRtfBy_SelectAll_ThreadSafe(this RichTextBox thisRichTextBox)
        {
            string rtf = string.Empty;
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = 0);
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = thisRichTextBox.TextLength);
            ThreadSafe.Exec(thisRichTextBox, () => rtf = thisRichTextBox.SelectedRtf);
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
            return rtf;
        }

        public static void TextSet_ThreadSafe(this RichTextBox thisRichTextBox, string text)
        {
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.Text = text);
        }

        public static void TextSet_ThreadSafe(this RichTextBox thisRichTextBox, string p, int[] highlightIndexes, int highlightDistance, int highlightLength)
        {

            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.Text = p);
            for (int i = 0; i < highlightIndexes.Length; i++)
            {
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionStart = highlightIndexes[i] * highlightDistance);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = highlightLength);
                ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionBackColor = System.Drawing.Color.LightPink);
            }
            ThreadSafe.Exec(thisRichTextBox, () => thisRichTextBox.SelectionLength = 0);
        }

        public static void AppendTextLine_ThreadSafe(this TextBox thisTextBox, string text)
        {
            ThreadSafe.Exec(thisTextBox, () => thisTextBox.AppendText(text + Environment.NewLine));
        }

        public static void EnabledSet_ThreadSafe(this Button thisButton, bool value)
        {
            ThreadSafe.Exec(thisButton, () => thisButton.Enabled = value);
        }
    }
}
