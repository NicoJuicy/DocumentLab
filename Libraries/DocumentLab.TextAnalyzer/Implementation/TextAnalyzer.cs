﻿namespace DocumentLab.TextAnalyzer.Implementation
{
  using Contracts;
  using Contracts.Ocr;
  using Interfaces;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;

  public class TextAnalyzer : ITextAnalyzer
  {
    private readonly IAnalyzeTextStrategy[] analyzeTextStrategies;

    public TextAnalyzer(IAnalyzeTextStrategy[] analyzeTextStrategies)
    {
      this.analyzeTextStrategies = analyzeTextStrategies;
    }

    public IEnumerable<AnalyzedText> AnalyzeString(string value)
    {
      var asFakeOcrResult = new OcrResult()
      {
        Result = new string[] { value }
      };

      return analyzeTextStrategies.SelectMany(operation => operation.Analyze(asFakeOcrResult) ?? new AnalyzedText[] { });
    }

    public IEnumerable<AnalyzedText> AnalyzeOcrResult(OcrResult ocrResult)
    {
      var ocrResultAsMany = ocrResult.Result.Select(x => new OcrResult()
      {
        BoundingBox = ocrResult.BoundingBox,
        Result = new string[] { x }
      });

      List<AnalyzedText> orderedResults = new List<AnalyzedText>();
      ocrResultAsMany.ToList().ForEach(y => orderedResults.AddRange(analyzeTextStrategies.SelectMany(operation => operation.Analyze(y) ?? new AnalyzedText[] { })));

      return orderedResults;
    }
  }
}
