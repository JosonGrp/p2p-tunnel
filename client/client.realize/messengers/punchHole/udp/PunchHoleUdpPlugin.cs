﻿using client.messengers.punchHole;
using client.messengers.punchHole.udp;
using common.libs;
using common.server.model;

namespace client.realize.messengers.punchHole.udp
{
    /// <summary>
    /// udp打洞消息
    /// </summary>
    public sealed class PunchHoleUdp : IPunchHole
    {
        private readonly IPunchHoleUdp punchHoleUdp;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="punchHoleUdp"></param>
        public PunchHoleUdp(IPunchHoleUdp punchHoleUdp)
        {
            this.punchHoleUdp = punchHoleUdp;
        }

        /// <summary>
        /// 
        /// </summary>
        public PunchHoleTypes Type => PunchHoleTypes.UDP;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        public void Execute(OnPunchHoleArg arg)
        {
            PunchHoleUdpSteps step = (PunchHoleUdpSteps)arg.Data.PunchStep;

            Logger.Instance.DebugDebug($"udp {step}");

            switch (step)
            {
                case PunchHoleUdpSteps.STEP_1:
                    Step1(arg);
                    break;
                case PunchHoleUdpSteps.STEP_2:
                    Step2(arg);
                    break;
                case PunchHoleUdpSteps.STEP_2_1:
                    Step21(arg);
                    break;
                case PunchHoleUdpSteps.STEP_2_Fail:
                    Step2Fail(arg);
                    break;
                case PunchHoleUdpSteps.STEP_3:
                    Step3(arg);
                    break;
                case PunchHoleUdpSteps.STEP_4:
                    Step4(arg);
                    break;
                default:
                    break;
            }
        }

        private void Step1(OnPunchHoleArg arg)
        {
            PunchHoleNotifyInfo model = new PunchHoleNotifyInfo();
            model.DeBytes(arg.Data.Data);
            punchHoleUdp.OnStep1(new OnStep1Params
            {
                Connection = arg.Connection,
                RawData = arg.Data,
                Data = model
            });
        }
        private void Step2(OnPunchHoleArg arg)
        {
            PunchHoleNotifyInfo model = new PunchHoleNotifyInfo();
            model.DeBytes(arg.Data.Data);
            punchHoleUdp.OnStep2(new OnStep2Params
            {
                Connection = arg.Connection,
                RawData = arg.Data,
                Data = model
            });
        }
        private void Step21(OnPunchHoleArg arg)
        {
            PunchHoleNotifyInfo model = new PunchHoleNotifyInfo();
            model.DeBytes(arg.Data.Data);
            punchHoleUdp.OnStep21(new OnStep21Params
            {
                Connection = arg.Connection,
                RawData = arg.Data,
                Data = model
            });
        }
        private void Step2Fail(OnPunchHoleArg arg)
        {
            punchHoleUdp.OnStep2Fail(new OnStep2FailParams
            {
                Connection = arg.Connection,
                RawData = arg.Data,
            });
        }

        private void Step3(OnPunchHoleArg arg)
        {
            PunchHoleStep3Info model = new PunchHoleStep3Info();
            model.DeBytes(arg.Data.Data);
            punchHoleUdp.OnStep3(new OnStep3Params
            {
                Connection = arg.Connection,
                RawData = arg.Data,
                Data = model
            });
        }
        private void Step4(OnPunchHoleArg arg)
        {
            PunchHoleStep4Info model = new PunchHoleStep4Info();
            model.DeBytes(arg.Data.Data);
            punchHoleUdp.OnStep4(new OnStep4Params
            {
                Connection = arg.Connection,
                RawData = arg.Data,
                Data = model
            });
        }
    }

}
