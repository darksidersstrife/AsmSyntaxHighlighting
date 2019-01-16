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

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "AsmRegister")]
    [Name("AsmRegister")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class RegisterClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorClassifier1Format"/> class.
        /// </summary>
        public RegisterClassifierFormat()
        {
            this.DisplayName = "AsmOpcode";
            this.BackgroundColor = Colors.LightYellow;
            this.ForegroundColor = Colors.Gray;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "AsmNumber")]
    [Name("AsmNumber")]
    [UserVisible(true)]
    [Order(Before = Priority.Default)]
    internal sealed class NumberClassifierFormat : ClassificationFormatDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EditorClassifier1Format"/> class.
        /// </summary>
        public NumberClassifierFormat()
        {
            this.DisplayName = "Number";
            this.ForegroundColor = Colors.Orange;
        }
    }
}
