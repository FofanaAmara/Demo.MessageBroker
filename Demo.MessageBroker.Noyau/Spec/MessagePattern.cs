﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sixeyed.MessageQueue.Messaging
{
    public enum MessagePattern
    {
        FireAndForget,
        RequestResponse,
        PublishSubscribe
    }
}
