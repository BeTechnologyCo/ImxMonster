﻿using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using System.Threading.Tasks; using Cysharp.Threading.Tasks;

namespace Nethereum.RPC.DebugNode
{
    public interface IDebugGetRawBlock
    {
        RpcRequest BuildRequest(BlockParameter block, object id = null);
        UniTask<string> SendRequestAsync(object id = null);
        UniTask<string> SendRequestAsync(BlockParameter block, object id = null);
    }
}