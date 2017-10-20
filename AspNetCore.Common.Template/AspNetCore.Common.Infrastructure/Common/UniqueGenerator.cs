using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.Caching.Distributed;
using AspNetCore.Common.Infrastructure.Extension;

namespace AspNetCore.Common.Infrastructure.Common
{
    public static class UniqueGenerator
    {
        class BranchOrderSequence
        {
            internal int Min { get; set; } = 0;
            internal int Max { get; set; } = 9999999;

            internal int Current { get; set; } = 0;

            public DateTime Date { get; set; }
        }

        static Dictionary<string, BranchOrderSequence> orderSequenceCache = new Dictionary<string, BranchOrderSequence>();
        const string BRANCH_ORDER_Sequence_CACHEKEY = "BRANCH_ORDER_SEQ_{0}";

        /// <summary>
        /// 生成唯一编码
        /// </summary>
        /// <param name="prefix">业务编码</param>
        /// <returns>格式：业务编码+年月日时分秒+线程ID+随机数</returns>
        public static string GetUniqueCode(string prefix = null)
        {
            return string.Format("{0}{1}{2}{3}", prefix, DateTime.Now.ToString("yyyyMMddHHmmss"),
                Thread.CurrentThread.ManagedThreadId.ToString().PadLeft(3, '0'),
                new Random(Guid.NewGuid().GetHashCode()).Next(10, 99));
        }

        public static string GetUniqueOrderId(string branchId, IDistributedCache cacheProvider)
        {
            int seq = -1;
            int length = 0;
            lock (orderSequenceCache)
            {
                if (!orderSequenceCache.ContainsKey(branchId))
                {
                    orderSequenceCache.Add(branchId, new BranchOrderSequence()
                    {
                        Date = DateTime.Now
                    });
                }
                var seqEntity = orderSequenceCache[branchId];
                var cached = cacheProvider.GetInt32(string.Format(BRANCH_ORDER_Sequence_CACHEKEY, branchId));
                if (cached != null)
                {
                    seqEntity.Current = cached.Value;
                }
                if (seqEntity.Current >= seqEntity.Max)
                {
                    seqEntity.Current = 0;
                    seqEntity.Date = DateTime.Now;
                }
                seq = seqEntity.Current += 1;
                length = seqEntity.Max.ToString().Length;
                cacheProvider.SetInt32(string.Format(BRANCH_ORDER_Sequence_CACHEKEY, branchId), seq);
            }
            return string.Format("{0}{1}", branchId.Substring(2, 4), seq.ToString().PadLeft(length, '0'));
        }

        class FinanceLogSequence
        {
            internal int Min { get; set; } = 0;
            internal int Max { get; set; } = 9999999;

            internal int Current { get; set; } = 0;

            public DateTime Date { get; set; }
        }

        static Dictionary<string, FinanceLogSequence> financeLogSequenceCache = new Dictionary<string, FinanceLogSequence>();
        const string BRANCH_FINANCELOG_SEQUENCE_CACHEKEY = "BRANCH_FINANCELOG_SEQ_{0}_{1}";

        /// <summary>
        /// 为收款记录和付款记录添加序列编号
        /// </summary>
        /// <param name="type"></param>
        /// <param name="branchId"></param>
        /// <returns></returns>
        public static string GenerateFinanceLogSequenceNo(int type, string branchId,IDistributedCache cacheProvider)
        {
            int seq = -1;
            int length = 0;
            var key = $"{type}_{branchId}";
            lock (financeLogSequenceCache)
            {
                if (!financeLogSequenceCache.ContainsKey(key))
                {
                    financeLogSequenceCache.Add(key, new FinanceLogSequence()
                    {
                        Date = DateTime.Now
                    });
                }
                var seqEntity = financeLogSequenceCache[key];
                var cached = cacheProvider.GetInt32(string.Format(BRANCH_FINANCELOG_SEQUENCE_CACHEKEY, type, branchId));
                if (cached != null)
                {
                    seqEntity.Current = cached.Value;
                }
                if (seqEntity.Current >= seqEntity.Max)
                {
                    seqEntity.Current = 0;
                    seqEntity.Date = DateTime.Now;
                }
                seq = seqEntity.Current += 1;
                length = seqEntity.Max.ToString().Length;
                cacheProvider.SetInt32(string.Format(BRANCH_FINANCELOG_SEQUENCE_CACHEKEY,type,branchId), seq);
            }
            return seq.ToString().PadLeft(length, '0');
        }
    }
}