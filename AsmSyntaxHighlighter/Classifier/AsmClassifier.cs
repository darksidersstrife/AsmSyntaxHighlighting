using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace VSIXProject2
{

    class MyListener : asm8086BaseListener
    {
        private SnapshotSpan _span;
        private Dictionary<String, IClassificationType> _classificationTypes;
        public List<ClassificationSpan> ClassifiedSpans;

        public  MyListener(SnapshotSpan span, Dictionary<String, IClassificationType> classificationTypes)
        {
            _span = span;
            ClassifiedSpans = new List<ClassificationSpan>();
            _classificationTypes = classificationTypes;
        }

        public override void EnterRegister_([NotNull] asm8086Parser.Register_Context context)
        {
            if (context.Start.StartIndex >= _span.Start && context.Start.StartIndex < _span.End || context.Stop.StopIndex > _span.Start && context.Stop.StopIndex <= _span.End)
            {
                ClassifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(_span.Snapshot, context.Start.StartIndex, context.Stop.StopIndex + 1 - context.Start.StartIndex), _classificationTypes["Register"]));
            }

        }

        public override void EnterOpcode([NotNull] asm8086Parser.OpcodeContext context)
        {
            if (context.Start.StartIndex >= _span.Start && context.Start.StartIndex < _span.End || context.Stop.StopIndex >= _span.Start && context.Stop.StopIndex < _span.End)
            {
                ClassifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(_span.Snapshot, context.Start.StartIndex, context.Stop.StopIndex + 1 - context.Start.StartIndex), _classificationTypes["Opcode"]));
            }
        }

        public override void EnterNumber([NotNull] asm8086Parser.NumberContext context)
        {
            if (context.Start.StartIndex >= _span.Start && context.Start.StartIndex < _span.End || context.Stop.StopIndex >= _span.Start && context.Stop.StopIndex < _span.End)
            {
                ClassifiedSpans.Add(new ClassificationSpan(new SnapshotSpan(_span.Snapshot, context.Start.StartIndex, context.Stop.StopIndex + 1 - context.Start.StartIndex), _classificationTypes["Number"]));
            }
        }
    }

    /// <summary>
    /// Classifier that classifies all text as an instance of the "EditorClassifier1" classification type.
    /// </summary>
    internal class AsmClassifier : IClassifier
    {
        /// <summary>
        /// Classification type.
        /// </summary>
        private readonly Dictionary<String, IClassificationType> classificationTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsmClassifier"/> class.
        /// </summary>
        /// <param name="registry">Classification registry.</param>
        internal AsmClassifier(IClassificationTypeRegistryService registry)
        {
            classificationTypes = new Dictionary<string, IClassificationType>();
            this.classificationTypes.Add("Opcode", registry.GetClassificationType("AsmOpcode"));
            this.classificationTypes.Add("Register", registry.GetClassificationType("AsmRegister"));
            this.classificationTypes.Add("Number", registry.GetClassificationType("AsmNumber"));
        }

        #region IClassifier

#pragma warning disable 67

        /// <summary>
        /// An event that occurs when the classification of a span of text has changed.
        /// </summary>
        /// <remarks>
        /// This event gets raised if a non-text change would affect the classification in some way,
        /// for example typing /* would cause the classification to change in C# without directly
        /// affecting the span.
        /// </remarks>
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        /// <summary>
        /// Gets all the <see cref="ClassificationSpan"/> objects that intersect with the given range of text.
        /// </summary>
        /// <remarks>
        /// This method scans the given SnapshotSpan for potential matches for this classification.
        /// In this instance, it classifies everything and returns each span as a new ClassificationSpan.
        /// </remarks>
        /// <param name="span">The span currently being classified.</param>
        /// <returns>A list of ClassificationSpans that represent spans identified to be of this classification.</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var snapshot = span.Snapshot;
            ICharStream stream = CharStreams.fromstring(snapshot.GetText() + '\n');
            ITokenSource lexer = new asm8086Lexer(stream);
            ITokenStream tokens = new CommonTokenStream(lexer);
            asm8086Parser parser = new asm8086Parser(tokens);
            //parser.RemoveErrorListeners();
            parser.BuildParseTree = true;
            IParseTree tree = parser.prog();
            MyListener l = new MyListener(span, classificationTypes);
            ParseTreeWalker walker = new ParseTreeWalker();
            walker.Walk(l, tree);
            /*var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationTypes[0])
            };*/

            return l.ClassifiedSpans;
        }

        #endregion
    }
}
