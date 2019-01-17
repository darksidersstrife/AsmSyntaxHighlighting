﻿using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace VSIXProject2
{
    /// <summary>
    /// Classification type definition export for EditorClassifier1
    /// </summary>
    internal static class EditorClassifier1ClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        /// <summary>
        /// Defines the "EditorClassifier1" classification type.
        /// </summary>
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("AsmOpcode")]
        private static ClassificationTypeDefinition OpCodetypeDefinition;

#pragma warning restore 169
    }
}
