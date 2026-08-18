using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiTaller.Core.Helpers
{
    public static class TextNormalizationHelper
    {
        /// <summary>
        /// Normaliza el texto para comparación canónica:
        /// - Trim y colapso de espacios múltiples
        /// - Remueve diacríticos / tildes (á -> a, é -> e, etc.)
        /// - Convierte a minúsculas invariantes
        /// Ejemplo: "  Baterías  " -> "baterias", "BATERIA" -> "bateria"
        /// </summary>
        public static string NormalizeForComparison(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;

            // 1. Trim y colapsar espacios internos
            string clean = Regex.Replace(text.Trim(), @"\s+", " ");

            // 2. Remover diacríticos
            string normalized = clean.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            // 3. Minúsculas
            return sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant();
        }

        /// <summary>
        /// Formatea un texto limpio con capitalización tipo título para guardado legible.
        /// Ejemplo: "  baterias y acumuladores  " -> "Baterias Y Acumuladores"
        /// </summary>
        public static string ToCleanTitleCase(string? text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            string clean = Regex.Replace(text.Trim(), @"\s+", " ");
            return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(clean.ToLowerInvariant());
        }
    }
}
