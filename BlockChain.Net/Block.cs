using System;
using System.Collections.Generic;
using System.Text;

namespace BlockChain.Net
{
    public struct Block
    {
        /// <summary>
        /// 区块位置
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 区块链生成时间戳
        /// </summary>
        public string TimeStamp { get; set; }

        /// <summary>
        /// 心率数值
        /// </summary>
        public int BPM { get; set; }
        /// <summary>
        /// 区块 SHA-256 散列值
        /// </summary>
        public string Hash { get; set; }
        /// <summary>
        /// 前一个区块 SHA-256 散列值
        /// </summary>
        public string PrevHash { get; set; }
    }
}
