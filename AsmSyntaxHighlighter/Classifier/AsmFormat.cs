using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace VSIXProject2
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "AsmOpcode")]
    [Name("AsmOpcode")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)] 
    internal sealed class OpcodeClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorClassifier1Format"/> class.
        /// </summary>
        public OpcodeClassifierFormat()
        {
            this.DisplayName = "AsmOpcode";
            this.ForegroundColor = Colors.Blue;
        }
    }
