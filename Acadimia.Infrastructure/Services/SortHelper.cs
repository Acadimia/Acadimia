using System.Linq.Dynamic.Core;

namespace Acadimia.Infrastructure.Services
{
    public static class SortHelper
    {
        private static readonly HashSet<string> AllowedDirections = new(StringComparer.OrdinalIgnoreCase)
        {
            "asc", "desc"
        };

        /// <summary>
        /// Applies OrderBy(sortColumn direction) only if sortColumn is present in
        /// allowedColumns (case-insensitive) and direction is asc/desc.
        /// Falls back to defaultSort (or leaves the query untouched) otherwise —
        /// it never throws and never forwards raw client input into Dynamic LINQ.
        /// </summary>
        public static IQueryable<T> ApplySort<T>(
            this IQueryable<T> query,
            string? sortColumn,
            string? sortColumnDirection,
            IReadOnlyCollection<string> allowedColumns,
            string? defaultSort = null)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
            {
                return string.IsNullOrWhiteSpace(defaultSort)
                    ? query
                    : query.OrderBy(defaultSort);
            }

            var matchedColumn = allowedColumns
                .FirstOrDefault(c => string.Equals(c, sortColumn, StringComparison.OrdinalIgnoreCase));

            if (matchedColumn == null)
            {
                return string.IsNullOrWhiteSpace(defaultSort)
                    ? query
                    : query.OrderBy(defaultSort);
            }

            var direction = AllowedDirections.Contains(sortColumnDirection ?? "")
                ? sortColumnDirection!.ToLowerInvariant()
                : "asc";

            return query.OrderBy($"{matchedColumn} {direction}");
        }
    }
}
