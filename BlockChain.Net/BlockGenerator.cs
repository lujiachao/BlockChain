using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BlockChain.Net
{
    public static class BlockGenerator
    {
        public static List<Block> _blockChain = new List<Block>();

        /// <summary>
        /// 计算区块HASH的值
        /// </summary>
        /// <param name="block">区块实例</param>
        /// <returns>计算完成的区块的散列值</returns>
        public static string CalculateHash(Block block)
        {
            string calculationStr = $"{block.Index}{block.TimeStamp}{block.BPM}{block.PrevHash}";
            SHA256 sha256Generator = SHA256.Create();
            byte[] sha256HashBytes = sha256Generator.ComputeHash(Encoding.UTF8.GetBytes(calculationStr));
            StringBuilder sha256StrBuilder = new StringBuilder();
            foreach (byte @byte in sha256HashBytes)
            {
                sha256StrBuilder.Append(@byte.ToString("x2"));
            }
            return sha256StrBuilder.ToString();
        }

        ///<summary>
        ///生成新的区块
        /// </summary>
        public static Block GenerateBlock(Block oldBlock, int BPM)
        {
            Block newBlock = new Block()
            {
                Index = oldBlock.Index + 1,
                TimeStamp = CalculateCurrentTimeUTC(),
                BPM = BPM,
                PrevHash = oldBlock.Hash
            };
            newBlock.Hash = CalculateHash(newBlock);
            return newBlock;
        }

        /// <summary>
        /// 计算当前时间的 UTC 表示格式
        /// </summary>
        /// <returns>UTC 时间字符串</returns>
        public static string CalculateCurrentTimeUTC()
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime nowTime = DateTime.Now;
            long unixTime = (long)Math.Round((nowTime - startTime).TotalMilliseconds, MidpointRounding.AwayFromZero);
            return unixTime.ToString();
        }

        /// <summary>
        /// 检验区块是否有效
        /// </summary>
        /// <param name="newBlock">新生成的区块数据</param>
        /// <param name="oldBlock">旧的区块数据</param>
        /// <returns>有效返回 TRUE，无效返回 FALSE</returns>
        public static bool IsBlockValid(Block newBlock, Block oldBlock)
        {
            if (oldBlock.Index + 1 != newBlock.Index) return false;
            if (oldBlock.Hash != newBlock.PrevHash) return false;
            if (CalculateHash(newBlock) != newBlock.Hash) return false;
            return true;
        }

        /// <summary>
        /// 如果新的区块链比当前区块链更新，则切换当前区块链为最新区块链
        /// 这里始终以最长的链为主线
        /// </summary>
        /// <param name="newBlockChain">新的区块链</param>
        public static void SwitchChain(List<Block> newBlockChain)
        {
            if (newBlockChain.Count > _blockChain.Count)
            {
                _blockChain = newBlockChain;
            }
        }
    }
}
