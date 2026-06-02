using HattieAI.Domain.Entities;

namespace HattieAI.Infrastructure.AI
{
    public static class TokenUsageGuard
    {
        private const int ApproxCharsPerToken = 4;

        public static int EstimateTokens(params string?[] values)
        {
            var totalChars = values
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Sum(v => v!.Length);

            return Math.Max(1, (int)Math.Ceiling(totalChars / (double)ApproxCharsPerToken));
        }

        public static bool IsLimitReached(Tenant tenant)
        {
            ResetMonthlyUsageIfNeeded(tenant);
            return tenant.MonthlyTokenLimit > 0 && tenant.MonthlyTokenUsage >= tenant.MonthlyTokenLimit;
        }

        public static bool WouldExceedLimit(Tenant tenant, int anticipatedTokens)
        {
            ResetMonthlyUsageIfNeeded(tenant);
            return tenant.MonthlyTokenLimit > 0 &&
                   tenant.MonthlyTokenUsage + Math.Max(0, anticipatedTokens) >= tenant.MonthlyTokenLimit;
        }

        public static void AddUsage(Tenant tenant, int tokenCount)
        {
            ResetMonthlyUsageIfNeeded(tenant);
            tenant.MonthlyTokenUsage += Math.Max(0, tokenCount);
        }

        public static string BuildUnavailableMessage(Tenant tenant)
        {
            return string.IsNullOrWhiteSpace(tenant.ContactPhone)
                ? "Our service is currently unavailable. Please contact us directly for support."
                : $"Our service is currently unavailable. Please contact us directly at {tenant.ContactPhone}.";
        }

        private static void ResetMonthlyUsageIfNeeded(Tenant tenant)
        {
            var now = DateTime.UtcNow;
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);

            if (tenant.TokenUsageMonthStartedAt < monthStart)
            {
                tenant.MonthlyTokenUsage = 0;
                tenant.TokenUsageMonthStartedAt = monthStart;
            }
        }
    }
}
