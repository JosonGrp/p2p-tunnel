﻿using client.messengers.register;
using client.service.ui.api.clientServer;
using common.libs.extends;
using common.server;
using common.socks5;
using System;

namespace client.service.socks5
{
    /// <summary>
    /// socks5
    /// </summary>
    public sealed class Socks5ClientService : IClientService
    {
        private readonly common.socks5.Config config;
        private readonly ISocks5ClientListener socks5ClientListener;
        private readonly Socks5Transfer socks5Transfer;
        private readonly ISocks5ClientHandler socks5ClientHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="socks5ClientListener"></param>
        /// <param name="socks5Transfer"></param>
        /// <param name="socks5ClientHandler"></param>
        public Socks5ClientService(common.socks5.Config config, ISocks5ClientListener socks5ClientListener, Socks5Transfer socks5Transfer, ISocks5ClientHandler socks5ClientHandler)
        {
            this.config = config;
            this.socks5ClientListener = socks5ClientListener;
            this.socks5Transfer = socks5Transfer;
            this.socks5ClientHandler = socks5ClientHandler;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public common.socks5.Config Get(ClientServiceParamsInfo arg)
        {
            return config;
        }

        /// <summary>
        /// 设置配置
        /// </summary>
        /// <param name="arg"></param>
        public void Set(ClientServiceParamsInfo arg)
        {
            var conf = arg.Content.DeJson<common.socks5.Config>();

            socks5ClientListener.Stop();
            if (conf.ListenEnable)
            {
                try
                {
                    socks5ClientListener.Start(conf.ListenPort, config.BufferSize);
                }
                catch (Exception ex)
                {
                    arg.SetCode(ClientServiceResponseCodes.Error, ex.Message);
                }
            }
            config.SaveConfig(arg.Content).Wait();

            socks5Transfer.ClearPac();
            socks5ClientHandler.Flush();
        }

        /// <summary>
        /// 获取pac
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public string GetPac(ClientServiceParamsInfo arg)
        {
            return socks5Transfer.GetPac();
        }

        /// <summary>
        /// 设置pac
        /// </summary>
        /// <param name="arg"></param>
        public void SetPac(ClientServiceParamsInfo arg)
        {
            PacSetParamsInfo model = arg.Content.DeJson<PacSetParamsInfo>();
            string msg = socks5Transfer.UpdatePac(model);
            if (!string.IsNullOrWhiteSpace(msg))
            {
                arg.SetCode(ClientServiceResponseCodes.Error, msg);
            }
        }


    }
}
