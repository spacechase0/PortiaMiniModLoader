using spacechase0.MiniModLoader.Api;
using spacechase0.MiniModLoader.Api.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace spacechase0.MiniModLoader.Unity
{
    public  class GlobalObjectComponent : MonoBehaviour
    {
        public void Update()
        {
            Util.InvokeEvent(nameof(Events.OnUpdate), Events.OnUpdate?.GetInvocationList(), null, new OnUpdateEventArgs(Time.deltaTime));
        }
    }
}
